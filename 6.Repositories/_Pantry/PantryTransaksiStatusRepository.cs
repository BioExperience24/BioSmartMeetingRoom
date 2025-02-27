
namespace _6.Repositories.Repository
{
    public class PantryTransaksiStatusRepository 
    {
        private readonly MyDbContext _dbContext;

        public PantryTransaksiStatusRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PantryTransaksiStatus?> GetAllPantryTransaksiStatus(int id)
        {
            var query = from p in _dbContext.PantryTransaksiStatuses
                        where p.Id == id
                        select p;

            return await query.FirstOrDefaultAsync();
        }

        public async Task<PantryTransaksiStatus?> GetPantryTransaksiStatus(int id)
        {
            var query = from p in _dbContext.PantryTransaksiStatuses
                        where p.Id == id
                        select p;

            return await query.FirstOrDefaultAsync();
        }

    }
}