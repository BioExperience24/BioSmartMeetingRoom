using _4.Data.ViewModels;
using _6.Repositories.Repository;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation;

public class AccessControlService(AccessControlRepository repo, IMapper mapper)
    : BaseService<AccessControlViewModel, AccessControl>(repo, mapper), IAccessControlService
{
}
