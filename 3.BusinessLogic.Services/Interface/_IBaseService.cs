using _4.Data.ViewModels;

namespace _3.BusinessLogic.Services.Interface;

public interface IBaseService<VM>
    where VM : BaseViewModel, new()
{
    Task<IEnumerable<VM>> GetAll();
    Task<VM?> GetById(string id);
    Task<int> Create(VM viewModel);
    Task<int> Update(VM viewModel);
    Task<int?> SoftDelete(string id);
    Task<int?> PermanentDelete(string id);
}
