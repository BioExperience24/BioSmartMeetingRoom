namespace _6.Repositories.Repository;

public class PantryRepository(MyDbContext context)
    : BaseLongRepository<Pantry>(context)
{
}