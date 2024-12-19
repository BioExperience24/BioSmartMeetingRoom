

namespace _3.BusinessLogic.Services.Implementation
{
    public class AccessChannelService(AccessChannelRepository repo, IMapper mapper) : BaseLongService<AccessChannelViewModel, AccessChannel>(repo, mapper), IAccessChannelService
    {
        
    }
}