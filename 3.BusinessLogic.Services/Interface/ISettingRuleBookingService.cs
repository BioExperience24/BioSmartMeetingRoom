using _4.Data.ViewModels;
using _7.Entities.Models;

namespace _3.BusinessLogic.Services.Interface
{
    public interface ISettingRuleBookingService : IBaseServiceId<SettingRuleBookingViewModel>
    {
        Task<SettingRuleBookingVMResponse> GetAllSettingRuleBookingsAsync();
        Task<SettingRuleBookingVMResponse> GetSettingRuleBookingByIdAsync(SettingRuleBookingDeleteViewModelFR request);
        Task<SettingRuleBooking?> CreateSettingRuleBookingAsync(SettingRuleBookingCreateViewModelFR request);
        //Task<SettingRuleBooking?> UpdateSettingRuleBookingAsync(SettingRuleBookingUpdateViewModelFR request);
        Task<int?> UpdateSettingRuleBookingAsync(SettingRuleBookingUpdateViewModelFR request);
        //Task<SettingRuleBooking?> DeleteSettingRuleBookingAsync(SettingRuleBookingDeleteViewModelFR request);
        Task<int?> DeleteSettingRuleBookingAsync(SettingRuleBookingDeleteViewModelFR request);
    }
}