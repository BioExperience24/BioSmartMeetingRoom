
namespace _6.Repositories.Repository
{
    public class RoomDisplayInformationRepository
    {
        private readonly MyDbContext _dbContext;
        public RoomDisplayInformationRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RoomDisplayInformation> CreateAsync(RoomDisplayInformation entity)
        {
            _dbContext.Set<RoomDisplayInformation>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task CreateBulkAsync(IEnumerable<RoomDisplayInformation> entities)
        {
            await _dbContext.BulkInsertAsync(entities.ToList());
        }

        public async Task<IEnumerable<RoomDisplayInformationSelect>> GetAllItemFilteredByDisplayId(long displayId)
        {
            var query = from rdi in _dbContext.RoomDisplayInformations
                        from r in _dbContext.Rooms.Where(r => r.Radid == rdi.RoomId)
                        where rdi.DisplayId == displayId
                        select new RoomDisplayInformationSelect
                        {
                            DisplayId = rdi.DisplayId,
                            RoomId = rdi.RoomId,
                            Icon = rdi.Icon,
                            Distance = rdi.Distance,
                            RoomName = r.Name
                        };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<RoomDisplayInformationSelect>> GetAllItemFilteredByDisplayIds(List<long> displayIds)
        {
            var query = from rdi in _dbContext.RoomDisplayInformations
                        from r in _dbContext.Rooms.Where(r => r.Radid == rdi.RoomId)
                        where rdi.DisplayId.HasValue && displayIds.Contains(rdi.DisplayId.Value)
                        select new RoomDisplayInformationSelect
                        {
                            DisplayId = rdi.DisplayId,
                            RoomId = rdi.RoomId,
                            Icon = rdi.Icon,
                            Distance = rdi.Distance,
                            RoomName = r.Name
                        };

            return await query.ToListAsync();
        }

        public async Task<int> DeleteFilteredByDisplayId(long displayId)
        {
            if (displayId < 1)
            {
                return 0;
            }

            var entities = await _dbContext.RoomDisplayInformations.Where(rdi => rdi.DisplayId == displayId).ToListAsync();

            _dbContext.RoomDisplayInformations.RemoveRange(entities);

            return await _dbContext.SaveChangesAsync();
        }
    }
}