

namespace _6.Repositories.Repository
{
    public class SendingNotifRepository 
    {
        private readonly MyDbContext _dbContext;
        
        public SendingNotifRepository (MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SendingNotif> AddAsync(SendingNotif entity)
        {
            _dbContext.Set<SendingNotif>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}