using _4.Data.ViewModels;
using _7.Entities.Models;

namespace _3.BusinessLogic.Services.Interface
{
    public interface IVariableTimeExtendService : IBaseServiceId<VariableTimeExtendViewModel>
    {
        Task<VariableTimeExtendVMResponse> GetAllVariableTimeExtendsAsync();
        Task<VariableTimeExtendVMResponse> GetVariableTimeExtendByIdAsync(VariableTimeExtendUpdateViewModelFR request);
        Task<VariableTimeExtend?> CreateVariableTimeExtendAsync(VariableTimeExtendCreateViewModelFR request);
        Task<VariableTimeExtend?> UpdateVariableTimeExtendAsync(VariableTimeExtendUpdateViewModelFR request);
        Task<VariableTimeExtend?> DeleteVariableTimeExtendAsync(VariableTimeExtendDeleteViewModelFR request);
    }
}