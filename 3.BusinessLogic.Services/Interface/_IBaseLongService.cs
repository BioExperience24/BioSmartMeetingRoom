namespace _3.BusinessLogic.Services.Interface;

public interface IBaseLongService<VM>
    where VM : BaseLongViewModel, new()
{
    Task<IEnumerable<VM>> GetAll();
    Task<VM?> GetById(long id);
    Task<VM?> Create(VM viewModel);
    Task<VM?> Update(VM viewModel);
    Task<VM?> SoftDelete(long id);
    Task<int?> PermanentDelete(long id);
    Task<IEnumerable<VM>> GetListByField(string field, string value);
    Task<VM?> GetOneByField(string field, string value);
    Task<IEnumerable<VM>?> CreateBulk(IEnumerable<VM> viewModels);
    Task<IEnumerable<VM>?> UpdateBulk(IEnumerable<VM> viewModels);
}
