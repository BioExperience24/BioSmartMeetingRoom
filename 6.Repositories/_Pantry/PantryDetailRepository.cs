namespace _6.Repositories.Repository;

public class PantryDetailRepository(MyDbContext context)
    : BaseLongRepository<PantryDetail>(context)
{
    private readonly MyDbContext _dbContext = context;

    public async Task<IEnumerable<PantryDetailSelect>> GetAllFilteredByBookingIds(string[] bookingIds)
    {
        var query = from pd in _dbContext.PantryDetails
                    from ptd in _dbContext.PantryTransaksiDs
                        .Where(ptd => pd.Id == ptd.MenuId)
                    from pt in _dbContext.PantryTransaksis
                        .Where(pt => ptd.TransaksiId == pt.Id)
                    where bookingIds.Contains(pt.BookingId)
                    select new PantryDetailSelect
                    {
                        Id = pd.Id,
                        Name = pd.Name,
                        Description = pd.Description,
                        // Note = pd.Note,
                        // Note = ptd.NoteOrder ?? string.Empty,
                        Note = ptd.NoteOrder ?? pd.Note,
                        Price = pd.Price,
                        Qty = ptd.Qty,
                        BookingId = pt.BookingId,
                        PantryId = pd.PantryId,
                        PackageId = pt.PackageId,
                        TransaksiId = pt.Id,
                    };

        

        return await query.ToListAsync();
    }
}