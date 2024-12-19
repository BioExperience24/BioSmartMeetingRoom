using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _6.Repositories.Repository;

public class AccessChannelRepository : BaseLongRepository<AccessChannel>
{
    private readonly MyDbContext _dbContext;

    public AccessChannelRepository(MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AccessChannel?> GetAccessChannelById(Guid id)
    {
        return await _dbContext.AccessChannels.FindAsync(id);
    }

    public async Task AddAccessChannel(AccessChannel accessChannel)
    {
        await _dbContext.AccessChannels.AddAsync(accessChannel);
        await _dbContext.SaveChangesAsync();
    }

    // Tambah method lainnya sesuai kebutuhan
}

public class AccessChannelConfiguration : IEntityTypeConfiguration<AccessChannel>
{
    public void Configure(EntityTypeBuilder<AccessChannel> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK_access_channel_id");

        entity.ToTable("access_channel", "smart_meeting_room");

        entity.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");

        entity.Property(e => e.Channel)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("channel");

        entity.Property(e => e.IsDeleted)
            .HasDefaultValue(0)
            .HasColumnName("is_deleted");
    }
}