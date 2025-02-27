namespace _6.Repositories.Repository;

public class PantryDetailMenuVariantDetailRepository(MyDbContext context)
    : BaseLongRepository<PantryDetailMenuVariantDetail>(context)
{
    public async Task<IEnumerable<PantryDetailMenuVariantDetail>> GetByMenuId(long menuId)
    {
        var query = from pd in context.PantryDetailMenuVariants
                    from pdt in context.PantryDetailMenuVariantDetails
                        .Where(ptd => pd.Id == ptd.VariantId)
                    where pd.MenuId == menuId
                    select new PantryDetailMenuVariantDetail
                    {
                        Id = pdt.Id,
                        Name = pdt.Name,
                        VariantId = pdt.VariantId,
                        Price = pdt.Price
                    };

        return await query.ToListAsync();
    }
}