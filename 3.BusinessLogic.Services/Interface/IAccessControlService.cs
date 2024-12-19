

namespace _3.BusinessLogic.Services.Implementation;

public interface IAccessControlService : IBaseService<AccessControlViewModel>
{
    Task<IEnumerable<AccessControlViewModel>> GetAllItemAsync();
    Task<IEnumerable<AccessControlVMRoom>> GetAllItemRoomAsync();
    Task<IEnumerable<AccessControlVMRoom>> GetAllItemRoomRoomDisplayAsync();
    Task<IEnumerable<AccessControlVMRoom>> GetAllItemRoomWithRadidsAsycn(string[] radIds);
    Task<AccessControlViewModel?> CreateAsync(AccessControlVMCreateFR request);
    Task<AccessControlViewModel?> UpdateAsync(AccessControlVMUpdateFR request);
    Task<AccessControlViewModel?> DeleteAsync(AccessControlVMDeleteFR request);
}
