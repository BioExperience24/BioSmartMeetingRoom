using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _6.Repositories.Repository;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation;

public class CompanyService(CompanyRepository repo, IMapper mapper)
    : BaseService<CompanyViewModel, Company>(repo, mapper), ICompanyService
{
}
