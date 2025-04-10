
namespace _6.Repositories.Repository
{
    public class HelpItGaRepository : BaseRepository<HelpItGa>
    {
        private readonly MyDbContext _dbContext;

        public HelpItGaRepository(MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DataTableEntity<object>> GetAllItemFilterdByEntityAsync(HelpItGaFilter? filter = null, int limit = 0, int offset = 0)
        {
            var query = from h in _dbContext.HelpItGas
                        from b in _dbContext.Bookings
                            .Where(b => h.BookingId == b.BookingId).DefaultIfEmpty()
                        select new {
                            h,
                            RoomName = b.RoomName,
                            BookingName = b.Title
                        };

            if (filter != null && !string.IsNullOrEmpty(filter.Type))
            {
                query = query.Where(q => q.h.Type == filter.Type);
            }
            
            var recordsTotal = await query.CountAsync();

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Search))
                {
                    query = query.Where(q =>
                        q.h.Id!.Contains(filter.Search) 
                        || q.h.Subject!.Contains(filter.Search)
                        || q.h.Description!.Contains(filter.Search)
                        || q.h.CreatedBy!.Contains(filter.Search)
                        || q.h.UpdatedBy!.Contains(filter.Search)
                        || q.h.ProcessBy!.Contains(filter.Search)
                        || q.h.DoneBy!.Contains(filter.Search)
                        || q.h.RejectBy!.Contains(filter.Search)
                        || q.h.Status!.Contains(filter.Search)
                        || q.RoomName!.Contains(filter.Search)
                        || q.BookingName!.Contains(filter.Search)
                    );
                }

                if (!string.IsNullOrEmpty(filter.RoomId))
                {
                    query = query.Where(q => q.h.RoomId == filter.RoomId);
                }

                if (filter.Start != null)
                {
                    query = query.Where(q => q.h.Datetime >= filter.Start);
                }

                if (filter.End != null)
                {
                    query = query.Where(q => q.h.Datetime <= filter.End);
                }
            }

            var recordsFiltered = await query.CountAsync();

            if (limit > 0)
            {
                query = query
                        .Skip(offset)
                        .Take(limit);
            }

            var result = await query.ToListAsync();
            
            return new DataTableEntity<object> {
                Collections = result,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsFiltered
            };
        }
    }
}