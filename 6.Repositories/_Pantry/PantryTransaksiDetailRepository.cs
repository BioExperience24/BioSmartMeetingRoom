
namespace _6.Repositories.Repository;

public class PantryTransaksiDetailRepository(MyDbContext dbContext)
{
    public async Task<IEnumerable<PantryTransaksiAndMenu>> GetPantryTransaksiDetailByTransaksiId(string idtransaksi)
    {
        var query = from p in dbContext.PantryTransaksiDs
                    join m in dbContext.PantryDetails on p.MenuId equals m.Id
                    where p.TransaksiId == idtransaksi
                    orderby p.Id
                    select new PantryTransaksiAndMenu
                    {
                        ItemId = p.Id,
                        Qty = p.Qty,
                        NoteOrder = p.NoteOrder,
                        NoteReject = p.NoteReject,
                        IsRejected = p.IsRejected,
                        Name = m.Name,
                        Detailorder = p.Detailorder
                    };

        return await query.ToListAsync();
    }

}