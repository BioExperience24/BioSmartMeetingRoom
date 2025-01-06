

namespace _6.Repositories.Repository
{
    public class RoomDisplayRepository : BaseLongRepository<RoomDisplay>
    {
        private readonly MyDbContext _dbContext;
        public RoomDisplayRepository(MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<object>> GetAllItemAsync()
        {
            var query = from roomDisplay in _dbContext.RoomDisplays
                        from room in _dbContext.Rooms
                                .Where(q => q.Radid == roomDisplay.RoomId)
                        where roomDisplay.IsDeleted == 0 && room.IsDeleted == 0
                        orderby room.Id ascending
                        select new {
                            roomDisplay,
                            Room = new {
                                RoomName = room != null ? room.Name : ""
                            }
                        };

            var list = await query.ToListAsync();
            
            return list;
        }
    }
}