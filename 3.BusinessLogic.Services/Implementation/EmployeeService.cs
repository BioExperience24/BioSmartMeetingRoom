using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _6.Repositories.Repository;
using _7.Entities.Models;
using AutoMapper;


namespace _3.BusinessLogic.Services.Implementation;

public class EmployeeService : BaseService<EmployeeViewModel, Employee>, IEmployeeService
{
    public EmployeeService(EmployeeRepository repo, IMapper mapper) : base(repo, mapper)
    {

    }
}

