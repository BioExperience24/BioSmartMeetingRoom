

namespace _3.BusinessLogic.Services.Interface
{
    public interface IAccessIntegratedService : IBaseLongService<AccessIntegratedViewModel>
    {
        Task<IEnumerable<AccessIntegratedViewModel>> GetAllItemByAccessIdAsync(string accessId);
        Task<bool> AssignAsync(AccessIntegratedVMAssignFR request);
    }
}