using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _6.Repositories.Repository;

public class PantryTransaksiRepository(MyDbContext context)
    : BaseRepository<PantryTransaksi>(context)
{

}

public class PantryTransaksiConfiguration : IEntityTypeConfiguration<PantryTransaksi>
{
    public void Configure(EntityTypeBuilder<PantryTransaksi> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK_pantry_transaksi_id");

        entity.ToTable("pantry_transaksi", "smart_meeting_room");

        //entity.HasIndex(e => e.Generate, "pantry_transaksi$_generate").IsUnique();

        entity.Property(e => e.Id)
            .HasMaxLength(100)
            .HasColumnName("id");
        entity.Property(e => e.BookingId)
            .HasMaxLength(100)
            .HasColumnName("booking_id");
        entity.Property(e => e.CanceledAt)
            .HasPrecision(0)
            .HasDefaultValueSql("(getdate())")
            .HasColumnName("canceled_at");
        entity.Property(e => e.CanceledBy)
            .HasMaxLength(100)
            .HasColumnName("canceled_by");
        entity.Property(e => e.CanceledPantryBy)
            .HasMaxLength(100)
            .HasDefaultValue("")
            .HasColumnName("canceled_pantry_by");
        entity.Property(e => e.Complete).HasColumnName("complete");
        entity.Property(e => e.CompletedAt)
            .HasPrecision(0)
            .HasDefaultValueSql("(getdate())")
            .HasColumnName("completed_at");
        entity.Property(e => e.CompletedBy)
            .HasMaxLength(100)
            .HasColumnName("completed_by");
        entity.Property(e => e.CompletedPantryBy)
            .HasMaxLength(100)
            .HasDefaultValue("")
            .HasColumnName("completed_pantry_by");
        entity.Property(e => e.CreatedAt)
            .HasPrecision(0)
            .HasDefaultValueSql("(getdate())")
            .HasColumnName("created_at");
        entity.Property(e => e.Datetime)
            .HasPrecision(0)
            .HasDefaultValueSql("(getdate())")
            .HasColumnName("datetime");
        entity.Property(e => e.Done).HasColumnName("done");
        entity.Property(e => e.EmployeeId)
            .HasMaxLength(50)
            .HasColumnName("employee_id");
        entity.Property(e => e.ExpiredAt)
            .HasPrecision(0)
            .HasDefaultValueSql("(getdate())")
            .HasColumnName("expired_at");
        entity.Property(e => e.Failed).HasColumnName("failed");
        entity.Property(e => e.FromPantry)
            .HasMaxLength(255)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("from_pantry");
        //entity.Property(e => e.Generate)
        //    .ValueGeneratedOnAdd()
        //    .HasColumnName("_generate");
        entity.Property(e => e.IsBlive).HasColumnName("is_blive");
        entity.Property(e => e.IsCanceled).HasColumnName("is_canceled");
        entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
        entity.Property(e => e.IsExpired).HasColumnName("is_expired");
        entity.Property(e => e.IsRejectedPantry).HasColumnName("is_rejected_pantry");
        entity.Property(e => e.IsTrashpantry).HasColumnName("is_trashpantry");
        entity.Property(e => e.Note).HasColumnName("note");
        entity.Property(e => e.NoteCanceled)
            .HasMaxLength(255)
            .HasDefaultValue("")
            .HasColumnName("note_canceled");
        entity.Property(e => e.NoteReject).HasColumnName("note_reject");
        entity.Property(e => e.OrderDatetime)
            .HasPrecision(0)
            .HasDefaultValueSql("(getdate())")
            .HasColumnName("order_datetime");
        entity.Property(e => e.OrderDatetimeBefore)
            .HasPrecision(0)
            .HasDefaultValueSql("(getdate())")
            .HasColumnName("order_datetime_before");
        entity.Property(e => e.OrderNo)
            .HasMaxLength(11)
            .HasColumnName("order_no");
        entity.Property(e => e.OrderSt).HasColumnName("order_st");
        entity.Property(e => e.OrderStName)
            .HasMaxLength(100)
            .HasColumnName("order_st_name");
        entity.Property(e => e.PantryId).HasColumnName("pantry_id");
        entity.Property(e => e.Pending)
            .HasDefaultValue(0)
            .HasColumnName("pending");
        entity.Property(e => e.PendingAt)
            .HasPrecision(0)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("pending_at");
        entity.Property(e => e.Process).HasColumnName("process");
        entity.Property(e => e.ProcessAt)
            .HasPrecision(0)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("process_at");
        entity.Property(e => e.ProcessBy)
            .HasMaxLength(255)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("process_by");
        entity.Property(e => e.ProcessPantryBy)
            .HasMaxLength(100)
            .HasDefaultValue("")
            .HasColumnName("process_pantry_by");
        entity.Property(e => e.RejectedAt)
            .HasPrecision(0)
            .HasDefaultValueSql("(getdate())")
            .HasColumnName("rejected_at");
        entity.Property(e => e.RejectedBy)
            .HasMaxLength(100)
            .HasColumnName("rejected_by");
        entity.Property(e => e.RejectedPantryBy)
            .HasMaxLength(100)
            .HasDefaultValue("")
            .HasColumnName("rejected_pantry_by");
        entity.Property(e => e.RoomId)
            .HasMaxLength(32)
            .HasColumnName("room_id");
        entity.Property(e => e.Timezone)
            .HasMaxLength(255)
            .HasDefaultValue("")
            .HasColumnName("timezone");
        entity.Property(e => e.ToPantry)
            .HasMaxLength(255)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("to_pantry");
        entity.Property(e => e.UpdatedAt)
            .HasPrecision(0)
            .HasDefaultValueSql("(getdate())")
            .HasColumnName("updated_at");
        entity.Property(e => e.UpdatedBy)
            .HasMaxLength(100)
            .HasColumnName("updated_by");
        entity.Property(e => e.Via)
            .HasMaxLength(50)
            .HasColumnName("via");
    }
}
