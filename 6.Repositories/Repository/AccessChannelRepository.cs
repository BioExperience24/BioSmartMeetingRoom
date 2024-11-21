using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _6.Repositories.Repository;

public class AccessChannelRepository
{
    private readonly MyDbContext _context;

    public AccessChannelRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<AccessChannel> GetAccessChannelById(Guid id)
    {
        return await _context.AccessChannels.FindAsync(id);
    }

    public async Task AddAccessChannel(AccessChannel accessChannel)
    {
        await _context.AccessChannels.AddAsync(accessChannel);
        await _context.SaveChangesAsync();
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
            .HasDefaultValue((short)0)
            .HasColumnName("is_deleted");
    }
}