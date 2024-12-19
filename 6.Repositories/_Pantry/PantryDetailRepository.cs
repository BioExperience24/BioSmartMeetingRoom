namespace _6.Repositories.Repository;

public class PantryDetailRepository(MyDbContext context)
    : BaseLongRepository<PantryDetail>(context)
{
}