
using _4.Data.ViewModels;

namespace _3.BusinessLogic.Services.Interface;

public interface IBaseServiceId<VM>
    where VM : BaseViewModelId, new()
{
    Task<IEnumerable<VM>> GetAll();
    Task<VM?> GetById(long id);
    Task<int> Create(VM viewModel);
    Task<bool> Update(VM viewModel);
    Task<bool> SoftDelete(long id);
    Task<bool> PermanentDelete(long id);
}
