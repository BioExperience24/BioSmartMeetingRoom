namespace _3.BusinessLogic.Services.Implementation;

public class DepartementService(DepartmentRepository repo, IMapper mapper)
    : BaseService<DepartmentViewModel, Department>(repo, mapper), IDepartmentService
{

    public readonly DepartmentRepository _repo = repo;

    public async Task<DepartmentViewModel> GetDepartmentAndCompany(string id)
    {
        var entity = await _repo.GetDepartmentWithCompany(id);
        if (entity == null)
        {
            return null;
        }

        var vm = _mapper.Map<DepartmentViewModel>(entity);
        vm.CompanyName = entity.Company.Name;
        return vm;
    }
}
