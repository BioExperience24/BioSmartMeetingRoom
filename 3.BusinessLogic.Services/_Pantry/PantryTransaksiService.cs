using System.Net;
using System.Security.Claims;
using System.Transactions;
using _5.Helpers.Consumer.Custom;
using _5.Helpers.Consumer.EnumType;

namespace _3.BusinessLogic.Services.Implementation;

public class PantryTransaksiService(PantryTransaksiRepository repo, IMapper mapper, PantryTransaksiDetailRepository repoD, ModuleBackendRepository _moduleBackendRepository, SettingPantryConfigRepository settingPantryConfigRepository, PantryMenuPaketRepository pantryMenuPaketRepository, PantryTransaksiStatusRepository pantryTransaksiStatusRepository, PantryTransaksiRepository pantryTransaksiRepository, PantryTransaskiDRepository pantryTransaskiDRepository, IHttpContextAccessor httpCtx, EmployeeRepository employeeRepo, PantryTransaksiDRepository pantryTransaksiDRepository, PantryDetailRepository pantryDetailRepository)
    : BaseService<PantryTransaksiViewModel, PantryTransaksi>(repo, mapper), IPantryTransaksiService
{
    public async Task<List<PantryTransactionDetail>> GetPantryTransaction(DateTime? start = null, DateTime? end = null, long? pantryId = null, long? orderSt = null)
    {
        var entities = await repo.GetPantryTransactionsAsync(start, end, pantryId, orderSt);
        return _mapper.Map<List<PantryTransactionDetail>>(entities); ;
    }

    public async Task<IEnumerable<PantryTransaksiStatusViewModel>> GetAllPantryTransaksiStatus()
    {
        var entities = await repo.GetAllPantryTransaksiStatus();
        var result = _mapper.Map<List<PantryTransaksiStatusViewModel>>(entities);
        return result;
    }

    public async Task<IEnumerable<PantryTransaksiAndMenuViewModel>> GetPantryTransaksiDetailByTransaksiId(string transaksiId)
    {
        IEnumerable<PantryTransaksiAndMenu> entities = await repoD.GetPantryTransaksiDetailByTransaksiId(transaksiId);
        var result = _mapper.Map<List<PantryTransaksiAndMenuViewModel>>(entities);
        return result;
    }

    public async Task<string> GetNextOrderNumber(long pantryId, DateTime? dateTime = null)
    {
        var orderNo = "ORD-" + _Random.AlphabetNumeric(4) + _Random.Numeric(3);
        dateTime ??= DateTime.Now;
        try
        {
            var maxOrderNo = repo.GetMaxOrderNo(dateTime.Value, pantryId);
            if (string.IsNullOrEmpty(maxOrderNo))
            {
                return "0001";
            }
            else
            {
                int oldNoOrderPantry = int.Parse(maxOrderNo);
                int noSortOrderPantry = oldNoOrderPantry + 1;
                return noSortOrderPantry.ToString("D4");
            }
        }
        catch (Exception)
        {
            return orderNo;
        }

    }

    public async Task CreatePantryOrderAsync(FastBookBookingViewModel databook, string id = "")
    {
        string timezone = TimeZoneInfo.Local.Id;

        // Get pantry settings and modules
        var modules = await _moduleBackendRepository.GetModuleByTextAsync(ModuleBackendTextModule.Pantry);
        var setPantryConfig = await settingPantryConfigRepository.GetSettingPantryConfigTopOne();

        if (setPantryConfig == null || modules == null || setPantryConfig.Status != 1 || modules.IsEnabled != 1) return;

        string pantryPackage = databook.PantryPackage ?? "";
        var pantryTransaksiD = databook.PantryDetail ?? new();

        if (pantryTransaksiD.Any(item => item.Qty > setPantryConfig.MaxOrderQty)) return;

        if (string.IsNullOrEmpty(pantryPackage)) return;

        DateTime meetingDateTime = DateTime.ParseExact(
            $"{databook.Date} {databook.Start}",
            "yyyy-MM-dd HH:mm",
            CultureInfo.InvariantCulture
        );

        DateTime orderDatetimeBefore = meetingDateTime.AddMinutes(-setPantryConfig.BeforeOrderMeeting);
        DateTime currentDatetime = DateTime.Now;

        // Get pantry package data
        var pantryData = await pantryMenuPaketRepository.GetDataPantryPackage(pantryPackage) ?? new PantryPackageDTO();
        var pantryTrsStatus = await pantryTransaksiStatusRepository.GetAllPantryTransaksiStatus(0);
        var rowOrderPantry = await pantryTransaksiRepository.GetMaxOrderNumberAsync(meetingDateTime.Date, pantryData.PantryId!);

        int orderNumber = string.IsNullOrEmpty(rowOrderPantry.OrderNo) ? 1 : int.Parse(rowOrderPantry.OrderNo) + 1;
        string formattedOrderNumber = orderNumber.ToString("D4");

        string transactionId = $"MEETING-{currentDatetime:yyyyMMddHHmmss}{new Random().Next(100, 999)}";

        // Create transaction object
        var pantryTransaction = new PantryTransaksi
        {
            Id = transactionId,
            PantryId = pantryData.PantryId,
            OrderNo = formattedOrderNumber,
            EmployeeId = databook.Pic.ToString(),
            BookingId = id,
            Via = "booking",
            Datetime = currentDatetime,
            OrderDatetime = currentDatetime,
            OrderDatetimeBefore = orderDatetimeBefore,
            OrderSt = 0,
            OrderStName = pantryTrsStatus.Name,
            Process = 0,
            Complete = 0,
            Failed = 0,
            Done = 0,
            Note = "",
            CreatedAt = currentDatetime,
            IsDeleted = 0,
            Timezone = timezone
        };

        var collectedPantryDetails = pantryTransaksiD.Select(item => new PantryTransaksiD
        {
            TransaksiId = transactionId,
            MenuId = item.Id,
            Qty = item.Qty,
            NoteOrder = item.NoteOrder,
            NoteReject = "",
            IsRejected = 0,
            IsDeleted = 0,
            Status = item.Status
        }).ToList();

        // Insert transactions if valid
        if (collectedPantryDetails.Count > 0)
        {
            await pantryTransaksiRepository.AddAsync(pantryTransaction);
            await pantryTransaskiDRepository.CreateBulk(collectedPantryDetails);
        }
    }

    public async Task<DataTableResponse> GetAllItemWithApprovalDataTablesAsync(PantryTransaksiVMNeedApprovalDataTableFR request)
    {
        var authUserNIK = httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
        var authUserLevel = httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;

        var filter = new PantryTransaksiFilter();

        filter.StartDate = request.StartDate;
        filter.EndDate = request.EndDate;

        if (request.PackageId != null)
        {
            filter.PackageId = request.PackageId;
        }

        var item = await repo.GetAllApprovalItemWithEntityAsync(filter, request.Length, request.Start);

        var collections = _mapper.Map<List<PantryTransaksiVMApporval>>(item.Collections);

        var userNiks = collections
            .SelectMany(q => new[] { q.UpdatedBy, q.ApprovedBy, q.RejectedBy, q.CanceledBy })
            .Where(s => !string.IsNullOrEmpty(s))
            .Distinct()
            .ToList();

        var employees = await employeeRepo.GetNikEmployeeByPic(userNiks!);

        var no = request.Start + 1;
        foreach (var collection in collections)
        {
            collection.No = no++;
            collection.UpdatedBy = employees.Where(q => q?.Nik == collection.UpdatedBy).Select(q => q?.Name).FirstOrDefault() ?? string.Empty;
            collection.ApprovedBy = employees.Where(q => q?.Nik == collection.ApprovedBy).Select(q => q?.Name).FirstOrDefault() ?? string.Empty;
            collection.RejectedBy = employees.Where(q => q?.Nik == collection.RejectedBy).Select(q => q?.Name).FirstOrDefault() ?? string.Empty;
            collection.CanceledBy = employees.Where(q => q?.Nik == collection.CanceledBy).Select(q => q?.Name).FirstOrDefault() ?? string.Empty;
        }
        
        return new DataTableResponse
        {
            Draw = request.Draw,
            RecordsTotal = 0,
            RecordsFiltered = 0,
            Data = collections
        };
    }

    public async Task<ReturnalModel> ProcessOrderApprovalAsync(PantryTransaksiVMProcessApproval request)
    {
        ReturnalModel ret = new();

        var authUserNIK = httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
        var now = DateTime.Now;

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                var pantryTransaksi = await repo.GetByIdAsync(request.Id);
                if (pantryTransaksi == null)
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = "Data not found";
                    return ret;
                }

                var pantryTransaksiDs = await pantryTransaksiDRepository.GetAllItemFilteredByTransaksiIdAsync(pantryTransaksi.Id!);

                PantryTransaksiStatus? pantryTransaksiStatus = null;
                switch (request.Approval)
                {
                    case 1:
                        pantryTransaksiStatus = await pantryTransaksiStatusRepository.GetAllPantryTransaksiStatus(1);

                        pantryTransaksi.OrderSt = (int)(pantryTransaksiStatus?.Id ?? 0);
                        pantryTransaksi.OrderStName = pantryTransaksiStatus?.Name ?? "";
                        pantryTransaksi.Process = 1;
                        pantryTransaksi.Note = request.Note ?? "";
                        pantryTransaksi.ApprovedAt = now;
                        pantryTransaksi.ApprovedBy = authUserNIK ?? "";

                        /* if (pantryTransaksiDs.Any())
                        {
                            foreach (var item in pantryTransaksiDs)
                            {
                                item.NoteOrder = request.Note ?? "";
                            }
                        } */
                        
                        break;
                    case 2:
                        pantryTransaksiStatus = await pantryTransaksiStatusRepository.GetAllPantryTransaksiStatus(5);

                        pantryTransaksi.OrderSt = (int)(pantryTransaksiStatus?.Id ?? 0);
                        pantryTransaksi.OrderStName = pantryTransaksiStatus?.Name ?? "";
                        pantryTransaksi.IsRejectedPantry = 1;
                        pantryTransaksi.RejectedAt = now;
                        pantryTransaksi.RejectedBy = authUserNIK ?? "";
                        pantryTransaksi.RejectedPantryBy = authUserNIK ?? "";
                        pantryTransaksi.NoteReject = request.Note;

                        if (pantryTransaksiDs.Any())
                        {
                            foreach (var item in pantryTransaksiDs)
                            {
                                item.IsRejected = 1;
                                item.RejectedAt = now;
                                item.RejectedBy = authUserNIK ?? "";
                                item.NoteReject = request.Note;
                            }
                        }
                        break;
                }

                if (pantryTransaksiStatus == null)
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = "The approval process failed due to an invalid status.";
                    return ret;
                }

                await repo.UpdateAsync(pantryTransaksi);

                if (pantryTransaksiStatus.Id == 5)
                {
                    await pantryTransaksiDRepository.UpdateBulk(pantryTransaksiDs);
                }

                scope.Complete();

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public async Task<ReturnalModel> PrintOrderApprovakAsycn(string pantryTransaksiId)
    {
        ReturnalModel ret = new();

        var pantryTransaksi = await repo.PrintOrderApprovalAsync(pantryTransaksiId);
        
        if (pantryTransaksi == null)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = "Data not found";
            return ret;
        }

        var userNiks = new List<string> { 
            pantryTransaksi.EmployeeId ?? string.Empty, 
            pantryTransaksi.ApprovedBy ?? string.Empty 
        }
        .Where(nik => !string.IsNullOrEmpty(nik))
        .ToList();

        var employees = userNiks.Any() ? await employeeRepo.GetNikEmployeeByPic(userNiks) : new List<Employee?>();
        
        pantryTransaksi.EmployeeOrganize = employees.Where(q => q != null && q.Nik == pantryTransaksi.EmployeeId).FirstOrDefault()?.Name ?? string.Empty;
        pantryTransaksi.EmployeeApprove = employees.Where(q => q != null && q.Nik == pantryTransaksi.ApprovedBy).FirstOrDefault()?.Name ?? string.Empty;

        var pantryDetails = await pantryDetailRepository.GetAllFilteredByBookingIds(new string[] { pantryTransaksi.BookingId! });

        var pantryDetailMaps = _mapper.Map<List<PantryDetailVMMenus>>(pantryDetails.Where(q => q.TransaksiId == pantryTransaksi.PantryTransaksiId).ToList());

        var pantryTransaksiMap = _mapper.Map<PantryTransaksiVMOrderApproval>(pantryTransaksi);
        pantryTransaksiMap.OrderDetail = pantryDetailMaps;

        ret.Collection = pantryTransaksiMap;

        return ret;
    }

    public async Task<ReturnalModel> ProcessCancelOrderAsync(PantryTransaksiVMProcessCancel request)
    {
        ReturnalModel ret = new();
        
        var authUserNIK = httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
        var now = DateTime.Now;

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                var pantryTransaksi = await repo.GetByIdAsync(request.Id);
                if (pantryTransaksi == null)
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = "Data not found";
                    return ret;
                }

                var pantryTransaksiDs = await pantryTransaksiDRepository.GetAllItemFilteredByTransaksiIdAsync(pantryTransaksi.Id!);
                
                var pantryTransaksiStatus = await pantryTransaksiStatusRepository.GetAllPantryTransaksiStatus(4);

                if (pantryTransaksiStatus == null)
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = "The approval process failed due to an invalid status.";
                    return ret;
                }

                pantryTransaksi.OrderSt = (int)(pantryTransaksiStatus?.Id ?? 0);
                pantryTransaksi.OrderStName = pantryTransaksiStatus?.Name ?? "";
                pantryTransaksi.IsCanceled = 1;
                pantryTransaksi.CanceledAt = now;
                pantryTransaksi.CanceledBy = authUserNIK ?? "";
                // pantryTransaksi.CanceledPantryBy = authUserNIK ?? "";
                pantryTransaksi.NoteCanceled = request.Note;

                await repo.UpdateAsync(pantryTransaksi);

                /* if (pantryTransaksiDs.Any())
                {
                    foreach (var item in pantryTransaksiDs)
                    {
                        item.IsRejected = 1;
                        item.RejectedAt = now;
                        item.RejectedBy = authUserNIK ?? "";
                        item.NoteReject = request.Note;
                    }
                    
                    await pantryTransaksiDRepository.UpdateBulk(pantryTransaksiDs);
                } */

                scope.Complete();

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}