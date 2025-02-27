using System.Net;
using _5.Helpers.Consumer.Custom;
using _5.Helpers.Consumer.EnumType;

namespace _3.BusinessLogic.Services.Implementation;

public class PantryTransaksiService(PantryTransaksiRepository repo, IMapper mapper, PantryTransaksiDetailRepository repoD, ModuleBackendRepository _moduleBackendRepository, SettingPantryConfigRepository settingPantryConfigRepository, PantryMenuPaketRepository pantryMenuPaketRepository, PantryTransaksiStatusRepository pantryTransaksiStatusRepository, PantryTransaksiRepository pantryTransaksiRepository, PantryTransaskiDRepository pantryTransaskiDRepository)
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

}