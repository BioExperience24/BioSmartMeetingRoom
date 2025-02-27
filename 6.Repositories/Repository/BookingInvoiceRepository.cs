

namespace _6.Repositories.Repository
{
    public class BookingInvoiceRepository : BaseLongRepository<BookingInvoice>
    {
        private readonly MyDbContext _dbContext;

        public BookingInvoiceRepository(MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BookingInvitation>> GetAllFilteredByBookingIds(string[] bookingIds)
        {
            var query = from bi in _dbContext.BookingInvitations
                        where bi.IsDeleted == 0
                        && bookingIds.Contains(bi.BookingId)
                        select bi;

            var result = await query.ToListAsync();

            return result;
        }
    }
}