

namespace _6.Repositories.Repository
{
    public class BookingInvitationRepository : BaseLongRepository<BookingInvitation>
    {
        private readonly MyDbContext _dbContext;

        public BookingInvitationRepository(MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}