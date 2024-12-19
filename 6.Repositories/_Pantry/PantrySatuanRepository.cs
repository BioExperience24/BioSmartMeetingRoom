namespace _6.Repositories.Repository;

public class PantrySatuanRepository(MyDbContext context)
    : BaseLongRepository<PantrySatuan>(context)
{
}