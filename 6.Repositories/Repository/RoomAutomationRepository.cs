using System.Diagnostics.Metrics;
using System.Linq;
using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _6.Repositories.Repository;

public class RoomAutomationRepository : BaseLongRepository<RoomAutomation>
{

    private readonly MyDbContext _dbContext;
        
    public RoomAutomationRepository(MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}

