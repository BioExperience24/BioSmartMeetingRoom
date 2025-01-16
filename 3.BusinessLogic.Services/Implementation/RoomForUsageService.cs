

namespace _3.BusinessLogic.Services.Implementation
{
    public class RoomForUsageService(RoomForUsageRepository repo, IMapper mapper) : BaseLongService<RoomForUsageViewModel, RoomForUsage>(repo, mapper), IRoomForUsageService
    {
        // public RoomForUsageService(RoomForUsageRepository repo, IMapper mapper) 
        //     : base(repo, mapper)
        // { }
    }
}