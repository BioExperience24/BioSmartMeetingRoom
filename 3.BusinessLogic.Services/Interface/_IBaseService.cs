namespace _3.BusinessLogic.Services.Interface;

public interface IBaseService<VM>
    where VM : BaseViewModel, new()
{
    Task<IEnumerable<VM>> GetAll();
    Task<VM?> GetById(string id);
    Task<VM?> Create(VM viewModel, bool isAInc = true);
    Task<VM?> Update(VM viewModel);
    Task<VM?> SoftDelete(string id);
    Task<int?> PermanentDelete(string id);
    Task<IEnumerable<VM>> GetListByField(string field, string value);
    Task<VM?> GetOneByField(string field, string value);
    Task<IEnumerable<VM>?> CreateBulk(IEnumerable<VM> viewModels);
    Task<IEnumerable<VM>?> UpdateBulk(IEnumerable<VM> viewModels);
}