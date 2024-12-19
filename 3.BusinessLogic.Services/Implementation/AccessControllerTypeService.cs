

namespace _3.BusinessLogic.Services.Implementation
{
    public class AccessControllerTypeService : BaseService<AccessControllerTypeViewModel, AccessControllerType>, IAccessControllerTypeService
    {
        public AccessControllerTypeService(AccessControllerTypeRepository repo, IMapper mapper) 
            : base(repo, mapper)
        { }
    }
}