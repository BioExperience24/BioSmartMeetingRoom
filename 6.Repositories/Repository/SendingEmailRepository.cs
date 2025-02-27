

namespace _6.Repositories.Repository
{
    public class SendingEmailRepository 
    {
        private readonly MyDbContext _dbContext;
        
        public SendingEmailRepository (MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SendingEmail> AddAsync(SendingEmail entity)
        {
            _dbContext.Set<SendingEmail>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}