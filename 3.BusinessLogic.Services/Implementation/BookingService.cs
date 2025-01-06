
using System.Drawing;
using System.Transactions;
using _5.Helpers.Consumer._Encryption;
using _5.Helpers.Consumer.EnumType;
using _7.Entities.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _3.BusinessLogic.Services.Implementation;

public class BookingService : BaseLongService<BookingViewModel, Booking>, IBookingService
{
    private readonly IMapper __mapper;
    
    private readonly BookingRepository _repo;

    private readonly UserConfigRepository _userConfigRepo;

    private readonly AlocationMatrixRepository _alocationMatrixRepo;

    private readonly UserRepository _userRepo;
    private readonly ModuleBackendRepository _repoModuleBackend;

    public BookingService(
        IMapper mapper,
        BookingRepository repo, 
        UserConfigRepository userConfigRepo,
        AlocationMatrixRepository alocationMatrixRepo,
        ModuleBackendRepository repoModuleBackend,
        UserRepository userRepo) : base(repo, mapper)
    { 
        _repo = repo;
        __mapper = mapper;
        _repoModuleBackend = repoModuleBackend;
        _userConfigRepo = userConfigRepo;
        _alocationMatrixRepo = alocationMatrixRepo;
        _userRepo = userRepo;
    }

    public async Task<IEnumerable<BookingVMChart>> GetItemChartsAsync(int year)
    {
        var items = await _repo.GetAllItemChartAsync(year);

        var result = __mapper.Map<List<BookingVMChart>>(items);

        return result;
    }

    public async Task<IEnumerable<BookingViewModel>> GetItemOngoingAsync(DateOnly startDate, DateOnly endDate, string? nik = null)
    {
        var items = await _repo.GetAllItemOngoingAsync(startDate, endDate, nik);

        var result = __mapper.Map<List<BookingViewModel>>(items);

        if (result.Any())
        {
            foreach (var item in result)
            {
                if (item.Start != DateTime.MinValue && item.End != DateTime.MinValue)
                {
                    // Menghitung selisih waktu
                    TimeSpan difference = item.End.Subtract(item.Start);
                    // Mendapatkan total menit
                    item.Duration = difference.TotalMinutes;
                }
            }
        }

        return result;
    }

    public async Task<int> GetCountAsync()
    {
        return await _repo.GetCountAsync();
    }

    public async Task<IEnumerable<BookingViewModel>> GetDataBookingAsync(DateTime start, DateTime end)
    {
        var bookings = await _repo.GetDataBookingAsync(start, end);
        return _mapper.Map<IEnumerable<BookingViewModel>>(bookings);
    }

    public async Task<IEnumerable<BookingViewModel>> GetDataBookingByNikAsync(DateTime start, DateTime end, string nik)
    {
        var bookings = await _repo.GetDataBookingByNikAsync(start, end, nik);
        return _mapper.Map<IEnumerable<BookingViewModel>>(bookings);
    }

    public async Task<BookingMenuDetailViewModel> GetDataBooking()
    {

        // note:
        //this function need level id based on authentication:
        var pagename = "Booking";
        var building = await _repoModuleBackend.GetBuildingData();
        var room = await _repoModuleBackend.GetDataRoom();
        var pantry_module = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Pantry);
        var facilities = await _repoModuleBackend.SelectAllFacilityAsync();
        var inovice_status_name = await _repoModuleBackend.GetInvoiceStatusName();
        var booking_menu = await _repoModuleBackend.GetBookingMenuByConditions();
        var setting_rule_booking = await _repoModuleBackend.GetSettingRuleBookingAsync();

        
        // Fetch room usage and check-in data
        var room_for_usage = await _repoModuleBackend.SelectAllRoomForUsageAsync();
        _mapper.Map<List<RoomForUsageViewModel>>(room_for_usage);

        var module_automation = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Automation);
        var vip = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.UserVIP);
        var module_price = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Price);
        var module_int_365 = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Int365);
        var module_int_google = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.IntGoogle);
        var module_room_adv = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.RoomAdvance);


        // Map modules to the response view model with detailed properties
        var modules = new BookingModulesViewModel
        {
            Automation = module_automation != null ? new BookingModuleDetailsViewModel
            {
                ModuleId = module_automation.ModuleId.ToString(),
                ModuleText = module_automation.ModuleText,
                Name = module_automation.Name,
                ModuleSerial = module_automation.ModuleSerial ?? string.Empty,
                IsEnabled = module_automation.IsEnabled
            } : new BookingModuleDetailsViewModel(),

            Vip = vip != null ? new BookingModuleDetailsViewModel
            {
                ModuleId = vip.ModuleId.ToString(),
                ModuleText = vip.ModuleText,
                Name = vip.Name,
                ModuleSerial = vip.ModuleSerial ?? string.Empty,
                IsEnabled = vip.IsEnabled
            } : new BookingModuleDetailsViewModel(),

            Price = module_price != null ? new BookingModuleDetailsViewModel
            {
                ModuleId = module_price.ModuleId.ToString(),
                ModuleText = module_price.ModuleText,
                Name = module_price.Name,
                ModuleSerial = module_price.ModuleSerial ?? string.Empty,
                IsEnabled = module_price.IsEnabled
            } : new BookingModuleDetailsViewModel(),

            Int365 = module_int_365 != null ? new BookingModuleDetailsViewModel
            {
                ModuleId = module_int_365.ModuleId.ToString(),
                ModuleText = module_int_365.ModuleText,
                Name = module_int_365.Name,
                ModuleSerial = module_int_365.ModuleSerial ?? string.Empty,
                IsEnabled = module_int_365.IsEnabled
            } : new BookingModuleDetailsViewModel(),

            IntGoogle = module_int_google != null ? new BookingModuleDetailsViewModel
            {
                ModuleId = module_int_google.ModuleId.ToString(),
                ModuleText = module_int_google.ModuleText,
                Name = module_int_google.Name,
                ModuleSerial = module_int_google.ModuleSerial ?? string.Empty,
                IsEnabled = module_int_google.IsEnabled
            } : new BookingModuleDetailsViewModel(),

            RoomAdv = module_room_adv != null ? new BookingModuleDetailsViewModel
            {
                ModuleId = module_room_adv.ModuleId.ToString(),
                ModuleText = module_room_adv.ModuleText,
                Name = module_room_adv.Name,
                ModuleSerial = module_room_adv.ModuleSerial ?? string.Empty,
                IsEnabled = module_room_adv.IsEnabled
            } : new BookingModuleDetailsViewModel(),
        };

        // Returning response data in a structured format
        return new BookingMenuDetailViewModel
        {
            Pagename = pagename,
            Menu = booking_menu,
            Building = building,
            Invoice = inovice_status_name,
            Category = room_for_usage,
            SettingGeneral = setting_rule_booking,
            Room = room,
            Organizer = room,
            Modules = modules,
            Facility = facilities
        };
    }
}

