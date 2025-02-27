namespace _3.BusinessLogic.Services.Interface;

public interface IUserService : IBaseLongService<UserViewModel>
{
    Task<IEnumerable<UserViewModel>> GetItemsAsync();
    Task<UserViewModel?> CreateAsync(UserVMCreateFR request);
    Task<UserViewModel?> UpdateAsync(UserVMUpdateFR request);
    Task<ReturnalModel> UpdateUsernameAsync(UserVMUpdateUsernameFR request, long id);
    Task<ReturnalModel> UpdatePassword(UserVMUpdatePasswordFR request, long id);
    Task<UserViewModel?> DisableAsync(UserVMDisableFR request);
    Task<UserViewModel?> DeleteAsync(UserVMDeleteFR request);
    Task<ReturnalModel> CheckLogin(LoginModel request);
    Task<ReturnalModel> RequestToken(LoginModel request);
    Task<ReturnalModel> GetAuthUser();
    // Task<ReturnalModel> GetProfileByNik(long id);
    Dictionary<string, string>? GetAllClaims();
    Task<ReturnalModel> PantryLogin(LoginModel request);
    Task<ReturnalModel> DisplayLogin(LoginModel request);
}