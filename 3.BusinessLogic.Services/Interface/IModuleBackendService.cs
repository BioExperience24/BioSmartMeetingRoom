

namespace _3.BusinessLogic.Services.Interface
{
    public interface IModuleBackendService
    {
        Task<ModuleBackendViewModel?> GetItemAsync(ModuleBackendViewModel? vm = null);
    }
}