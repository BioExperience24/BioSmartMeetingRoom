using _4.Data.ViewModels;
using _7.Entities.Models;

namespace _3.BusinessLogic.Services.Interface
{
    public interface IVariableTimeDurationService : IBaseServiceId<VariableTimeDurationViewModel>
    {
        Task<VariableSettingVMResponse> GetAllVariablesAsync();
        Task<VariableTimeDurationVMResponse> GetAllVariableTimeDurationsAsync();
        Task<VariableTimeDurationVMResponse> GetVariableTimeDurationByIdAsync(VariableTimeDurationUpdateViewModelFR request);
        Task<VariableTimeDuration?> CreateVariableTimeDurationAsync(VariableTimeDurationCreateViewModelFR request);
        Task<VariableTimeDuration?> UpdateVariableTimeDurationAsync(VariableTimeDurationUpdateViewModelFR request);
        Task<VariableTimeDuration?> DeleteVariableTimeDurationAsync(VariableTimeDurationDeleteViewModelFR request);
    }
}