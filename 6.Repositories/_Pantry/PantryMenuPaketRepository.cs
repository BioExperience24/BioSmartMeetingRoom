using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace _6.Repositories.Repository;

public class PantryMenuPaketRepository(MyDbContext context)
    : BaseRepository<PantryMenuPaket>(context)
{
    public async Task<PantryMenuPaket?> GetPackageWithPantry(string id)
    {
        return await _dbSet
            .Where(e => e.IsDeleted == 0) // Filter is_deleted
            .Include(d => d.Pantry) // Include untuk eager loading relasi ke Company
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id); // Cari berdasarkan id
    }

    public async Task<IEnumerable<PantryDetail?>> GetPantryDetailByPackageId(string id)
    {
        return await _dbSet
            .Where(e => e.IsDeleted == 0) // Filter is_deleted pada tabel utama
            .Where(p => p.Id == id)
            .SelectMany(p => p.PackageD.Where(d => d.IsDeleted == 0)) // Filter PackageD.IsDeleted == 0
            .Select(d => d.PantryDetail) // Ambil PantryDetail
            .ToListAsync();
    }

    public async Task<IEnumerable<PantryMenuPaketD?>> GetPantryPackageD(string id)
    {
        return await _dbSet
            .Where(e => e.IsDeleted == 0) // Filter is_deleted pada tabel utama
            .Where(p => p.Id == id)
            .SelectMany(p => p.PackageD.Where(d => d.IsDeleted == 0)) // Filter PackageD.IsDeleted == 0
            .ToListAsync();
    }


    public async Task<IEnumerable<PantryMenuPaket>> GetPackageWithPantry()
    {
        return await _dbSet
            .Where(e => e.IsDeleted == 0) // Filter is_deleted
            .Include(d => d.Pantry) // Include untuk eager loading relasi ke Company
            .AsNoTracking()
            .ToListAsync(); // Cari berdasarkan id
    }

    public virtual async Task CreateBulkPackageD(IEnumerable<PantryMenuPaketD> entities)
    {
        await _context.BulkInsertAsync(entities.ToList());
    }

    public virtual async Task UpdateBulkPackageD(IEnumerable<PantryMenuPaketD> entities)
    {
        await _context.BulkUpdateAsync(entities.ToList());
    }

    public virtual async Task SoftDeleteBulkByPackageId(string id)
    {
        var entityType = _context.Model.FindEntityType(typeof(PantryMenuPaketD));
        var tableName = entityType?.GetTableName();
        var schema = entityType?.GetSchema() ?? "dbo"; // Default ke 'dbo' kalau schema null
        var schemaDB = $"[{schema}].[{tableName}]";

        var sqlQuery = $@"
            UPDATE {schemaDB}
            SET is_deleted = 1
            WHERE package_id = @id";

        // Eksekusi query
        await _context.Database.ExecuteSqlRawAsync(sqlQuery, [new SqlParameter("@id", id)]);
    }


}

public class PantryMenuPaketConfiguration : IEntityTypeConfiguration<PantryMenuPaket>
{
    public void Configure(EntityTypeBuilder<PantryMenuPaket> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK_pantry_menu_paket_id");

        entity.ToTable("pantry_menu_paket", "smart_meeting_room");

        //entity.HasIndex(e => e.Generate, "pantry_menu_paket$_generate").IsUnique();

        entity.Property(e => e.Id)
            .HasMaxLength(50)
            .HasColumnName("id");
        entity.Property(e => e.CreatedAt)
            .HasPrecision(0)
            .HasColumnName("created_at");
        entity.Property(e => e.CreatedBy).HasColumnName("created_by");
        //entity.Property(e => e.Generate)
        //    .ValueGeneratedOnAdd()
        //    .HasColumnName("_generate");
        entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .HasColumnName("name");
        entity.Property(e => e.PantryId).HasColumnName("pantry_id");
        entity.Property(e => e.UpdatedAt)
            .HasPrecision(0)
            .HasColumnName("updated_at");
        entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

        // Relasi ke Pantry
        entity.HasOne(d => d.Pantry) // Navigation Property
            .WithMany() // Tidak ada koleksi Department di Company
            .HasForeignKey(d => d.PantryId) // Foreign key adalah id_perusahaan
            .HasConstraintName("FK_pantry_package");
    }
}

public class PackageDConfiguration : IEntityTypeConfiguration<PantryMenuPaketD>
{
    public void Configure(EntityTypeBuilder<PantryMenuPaketD> entity)
    {
        //entity.HasKey(e => e.Generate).HasName("PK_pantry_menu_paket_d__generate");
        entity.HasKey(d => new { d.MenuId, d.PackageId }); // Composite Key
        entity.ToTable("pantry_menu_paket_d", "smart_meeting_room");

        //entity.Property(e => e.Generate).HasColumnName("_generate");
        entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
        entity.Property(e => e.MenuId).HasColumnName("menu_id");
        entity.Property(e => e.PackageId).HasColumnName("package_id");

        entity.HasOne(d => d.Package) // Navigation Property
            .WithMany(f => f.PackageD)
            .HasForeignKey(ab => ab.PackageId) // Foreign key adalah id_perusahaan
            .HasConstraintName("FK_pantry_packageD");

        entity.HasOne(d => d.PantryDetail)
            .WithMany()
            .HasForeignKey(d => d.MenuId)
            .HasConstraintName("FK_pantry_menu_detail");
    }
}