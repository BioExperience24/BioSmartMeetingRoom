using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _6.Repositories.Repository;

public class AccessControlRepository : BaseRepository<AccessControl>
{
    public AccessControlRepository(MyDbContext context) : base(context)
    {
    }
}

public class AccessControlConfiguration : IEntityTypeConfiguration<AccessControl>
{
    public void Configure(EntityTypeBuilder<AccessControl> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK_access_control_id");

        entity.ToTable("access_control", "smart_meeting_room");

        entity.Property(e => e.Id)
            .HasMaxLength(15)
            .HasDefaultValue("")
            .HasColumnName("id");
        entity.Property(e => e.AccessId)
            .HasMaxLength(255)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("access_id");
        entity.Property(e => e.Channel)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("channel");
        entity.Property(e => e.ControllerList)
            .HasMaxLength(100)
            .HasColumnName("controller_list");
        entity.Property(e => e.CreatedAt)
            .HasPrecision(0)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("created_at");
        entity.Property(e => e.CreatedBy)
            .HasMaxLength(100)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("created_by");
        entity.Property(e => e.Delay)
            .HasDefaultValue(3)
            .HasColumnName("delay");
        entity.Property(e => e.IpController)
            .HasMaxLength(100)
            .HasDefaultValue("0.0.0.0")
            .HasColumnName("ip_controller");
        entity.Property(e => e.IsDeleted)
            .HasDefaultValue(0)
            .HasColumnName("is_deleted");
        entity.Property(e => e.ModelController)
            .HasMaxLength(255)
            .HasDefaultValue("reader")
            .HasColumnName("model_controller");
        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .HasDefaultValue("")
            .HasColumnName("name");
        entity.Property(e => e.RoomControllerFalco)
            .HasMaxLength(100)
            .HasColumnName("room_controller_falco");
        entity.Property(e => e.Type)
            .HasMaxLength(100)
            .HasColumnName("type");
        entity.Property(e => e.UpdatedAt)
            .HasPrecision(0)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("updated_at");
        entity.Property(e => e.UpdatedBy)
            .HasMaxLength(100)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("updated_by");
    }
}