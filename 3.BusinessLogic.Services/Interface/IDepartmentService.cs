using _4.Data.ViewModels;

namespace _3.BusinessLogic.Services.Interface;

public interface IDepartmentService : IBaseService<DepartmentViewModel>
{
    Task<DepartmentViewModel> GetDepartmentAndCompany(string id);
}
