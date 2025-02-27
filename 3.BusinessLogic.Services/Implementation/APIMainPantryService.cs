using _5.Helpers.Consumer.EnumType;
using System.Security.Claims;
using System.Transactions;

namespace _3.BusinessLogic.Services.Implementation;

public class APIMainPantryService(
        UserRepository _userRepository,
        PantryTransaksiRepository _pantryTransaksiRepository,
        PantryTransaksiStatusRepository _pantryTransaksiStatusRepository,
        PantryTransaksiDRepository _pantryTransaksiDetailRepo,
        IMapper mapper,
        IConfiguration config,
        IHttpContextAccessor context,
        IVariantService variant,
        IPantryDetailMenuVariantDetailService variantDetail,
        IPantryTransaksiService transaksiService,
        ISettingPantryConfigService pantryConfig
    )
    : IAPIMainPantryService
{


    public async Task<ReturnalModel> DisplayPantryOrder(OrderByDateRequest request, int? orderSt = null)
    {
        var ret = new ReturnalModel();
        var username = context.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
        var accessPantryBooking = await GetAccessPantryBooking(username!);

        if (accessPantryBooking.Status == ReturnalType.Failed)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Failed, your access is restricted"
            };
        }

        DateTime date = DateTime.TryParseExact(request.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate)
                        ? parsedDate
                        : DateTime.Now; // Default to current date if parsing fails

        try
        {
            var orderEntry = await _pantryTransaksiRepository.GetPantryEntriesAsync(date, request.PantryId, orderSt);

            if (orderEntry.Status == "success")
            {
                var transaksiIds = orderEntry.Data.Select(x => x.TransaksiId).ToList();
                var allDetails = await _pantryTransaksiRepository.GetBatchDetailPantryAsync(transaksiIds!);
                var detailsDict = allDetails.GroupBy(d => d.TransaksiId)
                                            .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var value in orderEntry.Data)
                {
                    if (detailsDict.TryGetValue(value.TransaksiId!, out var details))
                    {
                        value.Detail = details;
                    }
                }

                ret.Status = ReturnalType.Success;
                ret.Collection = orderEntry.Data;
                ret.Message = orderSt switch
                {
                    EnumPantryTransaksiOrderStatus.Entry => "Success get data from pantry entry",
                    EnumPantryTransaksiOrderStatus.Process => "Success get data from pantry process",
                    _ => "Success get data from pantry"
                };
            }
            else
            {
                ret.Status = ReturnalType.Failed;
                ret.StatusCode = StatusCodes.Status400BadRequest;
                ret.Message = orderEntry.Message;
            }
        }
        catch (Exception)
        {
            throw;
        }

        return ret;
    }

    public async Task<ReturnalModel> DisplayPantryPush(PushOrderRequest request, int status)
    {
        var ret = new ReturnalModel();
        var username = context.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
        var accessPantryBooking = await GetAccessPantryBooking(username!);

        if (accessPantryBooking.Status == ReturnalType.Failed)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                StatusCode = StatusCodes.Status403Forbidden,
                Message = "Failed, your access is restricted"
            };
        }

        DateTime date = DateTime.TryParseExact(request.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate)
                        ? parsedDate
                        : DateTime.Now; // Default to current date if parsing fails

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                var orderEntry = await _pantryTransaksiStatusRepository.GetAllPantryTransaksiStatus(status);
                if (orderEntry == null)
                {
                    return new ReturnalModel
                    {
                        Status = ReturnalType.Failed,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Pantry transaction status not found"
                    };
                }

                var pantryTransaksi = await _pantryTransaksiRepository.GetByIdAsync(request.TransaksiId);
                if (pantryTransaksi == null)
                {
                    return new ReturnalModel
                    {
                        Status = ReturnalType.Failed,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Transaction not found"
                    };
                }

                // Update the existing transaction based on status
                pantryTransaksi.OrderSt = status;
                pantryTransaksi.OrderStName = orderEntry.Name;
                pantryTransaksi.Process = status == EnumPantryTransaksiPushStatus.Process ? 1 : 0;
                pantryTransaksi.Complete = status == EnumPantryTransaksiPushStatus.Complete ? 1 : 0;
                pantryTransaksi.Failed = 0;
                pantryTransaksi.Done = 0;
                pantryTransaksi.IsRejectedPantry = status == EnumPantryTransaksiPushStatus.Reject ? 1 : 0;

                if (status == EnumPantryTransaksiPushStatus.Process)
                {
                    pantryTransaksi.ProcessAt = date;
                    pantryTransaksi.ProcessBy = username;
                }
                else if (status == EnumPantryTransaksiPushStatus.Complete)
                {
                    pantryTransaksi.CompletedAt = date;
                    pantryTransaksi.CompletedBy = username;
                    pantryTransaksi.CompletedPantryBy = username;
                    pantryTransaksi.IsTrashpantry = 0;
                }
                else if (status == EnumPantryTransaksiPushStatus.Reject)
                {
                    pantryTransaksi.RejectedAt = date;
                    pantryTransaksi.RejectedBy = username;
                    pantryTransaksi.RejectedPantryBy = username;
                    pantryTransaksi.NoteReject = request.NoteReject;
                }
                else if (status == EnumPantryTransaksiPushStatus.Canceled)
                {
                    pantryTransaksi.Failed = 1;
                    pantryTransaksi.Note = request.NoteReject;
                    pantryTransaksi.IsCanceled = 1;
                }

                // Save the changes
                await _pantryTransaksiRepository.UpdateAsync(pantryTransaksi);

                // Commit transaction
                scope.Complete();

                ret.Status = ReturnalType.Success;
                ret.Message = "Transaction successfully pushed to process state";
            }
            catch (Exception)
            {
                throw;
            }
        }
        return ret;
    }

    public async Task<ReturnalModel> GetPantryTransaksiDetailByTransaksiId(MobileDetailOrderRequest request)
    {
        var ret = new ReturnalModel();
        var collection = await transaksiService.GetPantryTransaksiDetailByTransaksiId(request.Id);
        foreach (var item in collection)
        {
            if (!string.IsNullOrEmpty(item.Detailorder))
            {
                try
                {
                    item.DetailorderObject = JsonSerializer.Deserialize<Dictionary<string, object>>(item.Detailorder);
                }
                catch (Exception)
                {
                    //gagal diconvert, skip aja
                }
            }
        }
        ret.Collection = collection;
        return ret;
    }

    public async Task<ReturnalModel> GetPantryVariantAndVariantDetail(MobileMenuDetailRequest request)
    {
        var ret = new ReturnalModel();
        if (long.TryParse(request.MenuId, out long res))
        {
            var getVariant = await variant.GetVariantByMenuId(res);
            var getVariantDetail = await variant.GetVariantDetailByMenuId(res);
            foreach (var item in getVariant)
            {
                item.variant = getVariantDetail.Where(x => x.variant_id == item.Id).ToList(); ;
            }
            ret.Collection = getVariant;
            ret.Message = "Success get data to menu";
        }
        else
        {
            ret.Status = ReturnalType.BadRequest;
            ret.Message = "Invalid ID format.";
            ret.StatusCode = StatusCodes.Status400BadRequest;
        }

        return ret;
    }

    public async Task<ReturnalModel> SetIsTrashPantry(PushOrderRequest request)
    {
        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                var pantryTransaksi = await _pantryTransaksiRepository.GetByIdAsync(request.TransaksiId);
                if (pantryTransaksi == null)
                {
                    return new ReturnalModel
                    {
                        Status = ReturnalType.Failed,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Transaction not found"
                    };
                }
                var username = context?.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
                pantryTransaksi.Id = request.TransaksiId;
                pantryTransaksi.IsTrashpantry = 1;//status delete
                pantryTransaksi.UpdatedAt = DateTime.Now;
                pantryTransaksi.UpdatedBy = username ?? "UnAuthorize";
                await _pantryTransaksiRepository.UpdateAsync(pantryTransaksi);

                scope.Complete();

                ReturnalModel ret = new()
                {
                    Collection = mapper.Map<PantryTransaksiViewModel>(pantryTransaksi),
                    Message = "Success remove data to pantry List",
                };
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private async Task<ReturnalModel> GetAccessPantryBooking(string username)
    {
        var ret = new ReturnalModel();
        var passed = false;
        var access_desk_id = 2;
        var existingUser = await _userRepository.GetUserByUsername(username);

        if (existingUser == null)
        {
            ret.Status = ReturnalType.Failed;
            ret.StatusCode = StatusCodes.Status400BadRequest;
            return ret;
        }

        var spAccess = existingUser.AccessId?.Split('#') ?? [];
        foreach (var value in spAccess)
        {
            if (value == access_desk_id.ToString())
            {
                passed = true;
                break;
            }
        }

        ret.Status = ReturnalType.Success;
        ret.Collection = passed;
        return ret;
    }

    public async Task<ReturnalModel> GetAllTrsPantry(MobileHistoryRequest request)
    {
        IEnumerable<object> res = await _pantryTransaksiRepository.GetAllTrsPantry(request.Nik);

        var result = mapper.Map<IEnumerable<PantryTransactionMobileHistory>>(res);
        ReturnalModel ret = new()
        {
            Collection = result,
        };

        return ret;
    }

    public async Task<ReturnalModel> PostSubmitOrder(MobileSubmitOrderRequest request)
    {
        ReturnalModel ret = new();

        var username = context.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(request.BookingId) || string.IsNullOrEmpty(request.PantryId))
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Pantry or schedule is empty"
            };
        }

        long pantryId;
        if (long.TryParse(request.PantryId, out long res))
        {
            pantryId = res;
        }
        else
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Invalid pantry ID format."
            };
        }

        string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        foreach (var order in request.PantryOrder)
        {
            if (TryDeserialize(order.detailorder, out List<DetailOrder>? detailOrderObj))
            {
                if (detailOrderObj == null)
                {
                    continue;
                }

                foreach (var detail in detailOrderObj)
                {
                    //detail.VariantDetail = detail.VariantDetail?.Where(v => v.OnChange == 1).ToList();
                }
            }
        }

        var allPantryConfig = (await pantryConfig.GetAllSettingPantryConfigsAsync()).Data;
        string idtrspantry = $"PANTRY-{DateTime.Now:yyyyMMddHHmmss}{_Random.Numeric(3)}";

        string orderNo = await transaksiService.GetNextOrderNumber(pantryId);

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            var pantryStatus = await _pantryTransaksiStatusRepository.GetAllPantryTransaksiStatus(EnumPantryTransaksiPushStatus.NotYetProcess);
            var pantryTransaction = new PantryTransaksi
            {
                Id = idtrspantry,
                PantryId = pantryId,
                OrderNo = orderNo,
                EmployeeId = request.Nik,
                BookingId = request.BookingId,
                Via = "mobile",
                Datetime = DateTime.Now,
                OrderDatetime = DateTime.Now,
                OrderDatetimeBefore = DateTime.Now,
                OrderSt = 0,
                OrderStName = pantryStatus?.Name ?? string.Empty,
                Process = 0,
                Complete = 0,
                Failed = 0,
                Done = 0,
                Note = "",
                CreatedAt = DateTime.Now,
                IsDeleted = 0,
                RoomId = "",
                NoteReject = "",
                NoteCanceled = "",
                IsRejectedPantry = 0,
                ProcessAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UpdatedBy = request.Nik ?? "",
                Timezone = TimeZoneInfo.Local.Id,
                PackageId = request.PackageId,
                ProcessBy = "",
                CanceledBy = "",
                RejectedBy = "",
                CompletedBy = ""
            };
            await _pantryTransaksiRepository.AddAsync(pantryTransaction);

            var dPantryTransaksiDs = request.PantryOrder.Select(order => new PantryTransaksiD
            {
                TransaksiId = idtrspantry,
                MenuId = order.id,
                Qty = order.qty,
                NoteOrder = order.note,
                NoteReject = "",
                Detailorder = order.detailorder,
                Status = order.status,
                IsRejected = 0,
                IsDeleted = 0,
                RejectedBy = "",
                RejectedAt = DateTime.MinValue
            }).ToList();

            if (dPantryTransaksiDs.Count != 0)
            {
                await _pantryTransaksiDetailRepo.CreateBulk(dPantryTransaksiDs);
            }

            scope.Complete();
            ret.Message = "Success create a order pantry";
        }//transaction scope

        return ret;
    }

    public bool TryDeserialize<T>(string jsonString, out T? result)
    {
        try
        {
            result = JsonSerializer.Deserialize<T>(jsonString);
            return true;
        }
        catch (JsonException)
        {
            result = default;
            return false;
        }
    }
}