

namespace _6.Repositories.Repository
{
    public class AccessIntegratedRepository(MyDbContext dbContext) : BaseLongRepository<AccessIntegrated>(dbContext)
    {
        private readonly MyDbContext _dbContext = dbContext;

        public async Task<IEnumerable<object>> GetAllItemAsync(AccessIntegrated? entity = null)
        {
            var query = _dbContext.AccessIntegrateds.AsQueryable();

            query = query.Where(q => q.IsDeleted == 0);

            if (entity != null)
            {
                if (entity.AccessId != null)
                {
                    query = query.Where(q => q.AccessId == entity.AccessId);
                }

                if (entity.RoomId != null)
                {
                    query = query.Where(q => q.RoomId == entity.RoomId);
                }
            }

            var list = await query.ToListAsync();

            return list;
        }

        public async Task<IEnumerable<AccessIntegrated>> GetAllItemWithEntity(AccessIntegrated? entity = null)
        {
            var query = _dbContext.AccessIntegrateds.AsQueryable();

            query = query.Where(q => q.IsDeleted == 0);

            if (entity != null)
            {
                if (entity.Id != null)
                {
                    query = query.Where(q => q.Id == entity.Id);
                }
                
                if (entity.AccessId != null)
                {
                    query = query.Where(q => q.AccessId == entity.AccessId);
                }

                if (entity.RoomId != null)
                {
                    query = query.Where(q => q.RoomId == entity.RoomId);
                }
            }

            var list = await query.ToListAsync();

            return list;
        }
    }
}