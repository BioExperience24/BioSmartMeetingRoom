namespace _3.BusinessLogic.Services.Interface;

public interface IUserService : IBaseLongService<UserViewModel>
{
    Task<IEnumerable<UserViewModel>> GetItemsAsync();
    Task<UserViewModel?> CreateAsync(UserVMCreateFR request);
    Task<UserViewModel?> UpdateAsync(UserVMUpdateFR request);
    Task<UserViewModel?> DisableAsync(UserVMDisableFR request);
    Task<UserViewModel?> DeleteAsync(UserVMDeleteFR request);
    Task<ReturnalModel> CheckLogin(LoginModel request);
}