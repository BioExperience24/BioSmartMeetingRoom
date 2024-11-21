using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _6.Repositories.Repository;

public class FacilityRepository : BaseRepository<Facility>
{
    public FacilityRepository(MyDbContext context) : base(context)
    {
    }
}

