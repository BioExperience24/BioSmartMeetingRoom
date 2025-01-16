
namespace _6.Repositories.Repository
{
    public class PantryTransaksiDRepository : BaseLongRepository<PantryTransaksiD>
    {
        private readonly MyDbContext _dbContext;

        public PantryTransaksiDRepository(MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}