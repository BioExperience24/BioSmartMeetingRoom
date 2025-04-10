using System.Globalization;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _6.Repositories.Repository;

public class PantryTransaksiRepository(
    MyDbContext context, 
    ModuleBackendRepository _repoModuleBackend,
    SettingPantryConfigRepository _settingPantryConfigRepository,
    PantryMenuPaketRepository _pantryMenuPaketRepository,
    PantryTransaksiStatusRepository _pantryTransaksiStatusRepository
    )
    : BaseRepository<PantryTransaksi>(context)
{
    public async Task<IEnumerable<object>> GetPantryTransactionsAsync(DateTime? start = null, DateTime? end = null, long? pantryId = null, long? orderSt = null)
    {
        if (start.HasValue)
        {
            start = start.Value.Date; // Set ke jam 00:00:00
        }

        if (end.HasValue)
        {
            end = end.Value.Date.AddDays(1).AddSeconds(-1); // Set ke jam 23:59:59
        }

        var query = from pt in context.PantryTransaksis
                    from booking in context.Bookings
                        .Where(b => b.BookingId == pt.BookingId).DefaultIfEmpty()
                    from room in context.Rooms
                        .Where(r => r.Radid == booking.RoomId).DefaultIfEmpty()
                    from employee in context.Employees
                        .Where(e => e.Nik == pt.EmployeeId).DefaultIfEmpty()
                    from am in context.AlocationMatrices
                        .Where(a => a.Nik == employee.DepartmentId).DefaultIfEmpty()
                    from alocation in context.Alocations
                        .Where(a => a.Id == am.AlocationId).DefaultIfEmpty()
                    from pts in context.PantryTransaksiStatuses
                        .Where(ps => ps.Id == pt.OrderSt).DefaultIfEmpty()
                    where pt.IsBlive == 0 && booking.IsApprove == 1
                        && (pantryId == null || pt.PantryId == pantryId)
                        && (start == null || pt.OrderDatetime >= start)
                        && (end == null || pt.OrderDatetime <= end)
                        && (orderSt == null || pts.Id == orderSt)
                    orderby pt.Id descending
                    select new
                    {
                        Id = pt.Id,
                        PantryId = pt.PantryId,
                        OrderNo = pt.OrderNo,
                        Title = booking.Title,
                        DateBooking = booking.Date,
                        RoomName = room.Name,
                        RoomLocation = room.Location,
                        BookingStart = booking.Start,
                        BookingEnd = booking.End,
                        EmployeeName = employee.Name,
                        DepartmentName = alocation.Name,
                        OrderStatus = pts.Name,
                        OrderDatetime = pt.OrderDatetime,
                        ExpiredAt = pt.ExpiredAt,
                        UpdatedAt = pt.UpdatedAt,
                        OrderSt = pt.OrderSt
                    };

        var result = await query.ToListAsync();

        return result;
    }

    public async Task<IEnumerable<PantryTransaksiStatus>?> GetAllPantryTransaksiStatus()
    {
        return await context.PantryTransaksiStatuses
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<PantryEntryResponse> GetPantryEntriesAsync(DateTime date, int pantryId, int? orderSt = null)
    {
        try
        {
            var query = from p in _context.PantryTransaksis
                        join e in _context.Employees on p.EmployeeId equals e.Nik into emp
                        from Employee in emp.DefaultIfEmpty()
                        join b in _context.Bookings on p.BookingId equals b.BookingId into book
                        from Booking in book.DefaultIfEmpty()
                        join r in _context.Rooms on Booking.RoomId equals r.Radid into room
                        from Room in room.DefaultIfEmpty()
                        where p.IsDeleted == 0 && p.PantryId == pantryId
                            && p.OrderDatetime.Date == date && p.IsTrashpantry == 0
                        orderby p.Id descending
                        select new PantryEntryDto
                        {
                            OrderSt = p.OrderSt,
                            Process = p.Process,
                            Complete = p.Complete,
                            Failed = p.Failed,
                            IsRejectedPantry = p.IsRejectedPantry,
                            NoteReject = p.NoteReject,
                            PantryId = p.PantryId,
                            TransaksiId = p.Id,
                            OrderNo = p.OrderNo,
                            EmployeeName = Employee.Name,
                            EmployeeNik = Employee.Nik,
                            Title = Booking.Title,
                            RoomName = Room.Name,
                            StartBooking = Booking.Start,
                            EndBooking = Booking.End,
                            OrderDatetime = p.OrderDatetime,
                            OrderDatetimeBefore = p.OrderDatetimeBefore,
                            OrderStName = p.OrderStName,
                            CompletedAt = p.CompletedAt,
                            CompletedBy = p.CompletedBy,
                            ProcessAt = p.ProcessAt,
                            ProcessBy = p.ProcessBy,
                            RejectedAt = p.RejectedAt,
                            RejectedBy = p.RejectedBy
                        };

            // Apply order status filter if provided
            if (orderSt.HasValue)
            {
                query = query.Where(p => p.OrderSt == orderSt.Value);
            }
            else
            {
                query = query.Where(p => p.OrderSt == 0 || p.OrderSt == 4 || p.OrderSt == 5);
            }

            var data = await query.ToListAsync();
            return new PantryEntryResponse
            {
                Status = "success",
                Data = data,
                Message = "Success get data from pantry"
            };
        }
        catch (Exception)
        {
            return new PantryEntryResponse
            {
                Status = "fail",
                Data = new List<PantryEntryDto>(),
                Message = "Failed to retrieve pantry data"
            };
        }
    }

    public async Task<List<DetailPantryDto>> GetBatchDetailPantryAsync(List<string> transaksiIds)
    {
        return await _context.PantryTransaksiDs
            .Where(pt => transaksiIds.Contains(pt.TransaksiId) && pt.IsDeleted == 0)
            .Select(pt => new DetailPantryDto
            {
                TransaksiId = pt.TransaksiId,
                MenuId = pt.MenuId,
                Qty = pt.Qty,
                NoteOrder = pt.NoteOrder,
                NoteReject = pt.NoteReject,
                Detailorder = pt.Detailorder,
                Status = pt.Status,
                IsRejected = pt.IsRejected,
                RejectedBy = pt.RejectedBy,
                RejectedAt = pt.RejectedAt
            }).ToListAsync();
    }

    public async Task<IEnumerable<PantryTransaksi>> GetAllItemFilteredByEntity(PantryTransaksi? filter = null)
    {
        var query = (from pt in context.PantryTransaksis
                    select pt).AsQueryable();

        if (!string.IsNullOrEmpty(filter!.BookingId))
        {
            query = query.Where(pt => pt.BookingId == filter.BookingId);
        }

        if (filter!.BookingIds.Any())
        {
            query = query.Where(pt => filter.BookingIds.Contains(pt.BookingId));
        }

        if (!string.IsNullOrEmpty(filter!.Via))
        {
            query = query.Where(pt => pt.Via == filter.Via);
        }

        if (filter!.OrderSt != default(int))
        {
            query = query.Where(pt => pt.OrderSt == filter.OrderSt);
        }

        var result = await query.ToListAsync();

        return result;
    }

    public async Task<string> GetPantryOrderNumberAsync(long pantryId, DateOnly qDate)
    {
        var maxOrderNo = await context.PantryTransaksis
                                    .Where(pt => pt.OrderDatetime.Date == qDate.ToDateTime(TimeOnly.MinValue).Date && pt.PantryId == pantryId)
                                    .MaxAsync(pt => (int?)int.Parse(pt.OrderNo)) ?? 0;
        if (maxOrderNo == 0)
        {
            return "0001";
        }
        else
        {
            var newOrderNo = maxOrderNo + 1;
            return newOrderNo.ToString("D4");
        }
    }

    public async Task<IEnumerable<object>> GetAllTrsPantry(string nik)
    {
        var data = await (from pt in context.PantryTransaksis
                        where pt.IsDeleted == 0 && pt.EmployeeId == nik
                        join p in context.Pantries on pt.PantryId equals p.Id into pantryJoin
                        from p in pantryJoin.DefaultIfEmpty()
                        join b in context.Bookings on pt.BookingId equals b.BookingId into bookingJoin
                        from b in bookingJoin.DefaultIfEmpty()
                        join r in context.Rooms on b.RoomId equals r.Radid into roomJoin
                        from r in roomJoin.DefaultIfEmpty()
                        join pts in context.PantryTransaksiStatuses on pt.OrderSt equals pts.Id into statusJoin
                        from pts in statusJoin.DefaultIfEmpty()
                        select new
                        {
                            pt.Id,
                            pt.EmployeeId,
                            OrderUser = pt.EmployeeId,
                            OrderNo = pt.OrderNo,
                            pt.PantryId,
                            pt.BookingId,
                            Order = pt.OrderSt,
                            pt.Process,
                            pt.Complete,
                            pt.Failed,
                            pt.Done,
                            pt.Note,
                            PantryName = p.Name,
                            BookingTitle = b.Title,
                            BookingDate = b.Date,
                            BookingStart = b.Start,
                            BookingEnd = b.End,
                            RoomName = r.Name,
                            StatusOrder = pts.Name,
                            pt.OrderDatetime,
                            pt.OrderDatetimeBefore,
                            pt.Datetime,
                            CountItem = context.PantryTransaksiDs.Count(ptd => ptd.TransaksiId == pt.Id),
                            OrderUserName = context.Employees.Where(e => e.Id == pt.EmployeeId).Select(e => e.Name).FirstOrDefault(),
                            RejectedPantryByName = context.Employees.Where(e => e.Id == pt.RejectedPantryBy).Select(e => e.Name).FirstOrDefault(),
                            CompletedPantryByName = context.Employees.Where(e => e.Id == pt.CompletedPantryBy).Select(e => e.Name).FirstOrDefault(),
                            ProcessPantryByName = context.Employees.Where(e => e.Id == pt.ProcessPantryBy).Select(e => e.Name).FirstOrDefault()
                        })
                        .OrderBy(pt => pt.Datetime)
                        .ToListAsync();

        return data;
    }

    public string GetMaxOrderNo(DateTime tanggalOrderPantry, long pantryId)
    {
        var maxOrderNo = _context.PantryTransaksis
            .Where(p => p.OrderDatetime.Date == tanggalOrderPantry.Date && p.PantryId == pantryId)
            .Max(p => (string?)p.OrderNo) ?? "";

        return maxOrderNo;
    }    
    
    public async Task<PantryTransaksi?> GetMaxOrderNumberAsync(DateTime meetingDate, long pantryId)
    {
        return await _context.PantryTransaksis
            .Where(p => p.OrderDatetime.Date == meetingDate && p.PantryId == pantryId)
            .OrderByDescending(p => p.OrderNo)
            .FirstOrDefaultAsync();
    }
    
    public async Task<Booking?> GetMaxInvoiceOrderAsync(string year)
    {
        var result = await _context.Bookings
            .Where(b => b.Date.Year == int.Parse(year))
            .OrderByDescending(b => b.NoOrder)
            .FirstOrDefaultAsync();
        return result;
    }

    public async Task<PantryTransaksi?> GetItemFilteredByBookingId(string bookingId)
    {
        return await context.PantryTransaksis
            .Where(pt => pt.BookingId == bookingId)
            .FirstOrDefaultAsync();
    }

    public async Task<DataTableEntity<PantryTransaksiSelect>> GetAllApprovalItemWithEntityAsync(PantryTransaksiFilter? entity = null, int limit = 0, int offset = 0)
    {
        var query = (from pt in context.PantryTransaksis
                    from b in context.Bookings
                        .Where(b => b.BookingId == pt.BookingId).DefaultIfEmpty()
                    where pt.IsDeleted == 0 
                        && b.IsDeleted == 0 
                        // && b.IsCanceled == 0
                    orderby pt.Id descending
                    select new PantryTransaksiSelect
                    {
                        Id = pt.Id,
                        PantryId = pt.PantryId,
                        OrderNo = pt.OrderNo,
                        EmployeeId = pt.EmployeeId,
                        BookingId = pt.BookingId,
                        IsBlive = pt.IsBlive,
                        RoomId = pt.RoomId,
                        Via = pt.Via,
                        Datetime = pt.Datetime,
                        OrderDatetime = pt.OrderDatetime,
                        OrderDatetimeBefore = pt.OrderDatetimeBefore,
                        OrderSt = pt.OrderSt,
                        OrderStName = pt.OrderStName,
                        Process = pt.Process,
                        Complete = pt.Complete,
                        Failed = pt.Failed,
                        Done = pt.Done,
                        Note = pt.Note,
                        NoteReject = pt.NoteReject,
                        NoteCanceled = pt.NoteCanceled,
                        IsRejectedPantry = pt.IsRejectedPantry,
                        RejectedBy = pt.RejectedBy,
                        RejectedAt = pt.RejectedAt,
                        IsTrashpantry = pt.IsTrashpantry,
                        IsCanceled = pt.IsCanceled,
                        IsExpired = pt.IsExpired,
                        ExpiredAt = pt.ExpiredAt,
                        CanceledBy = pt.CanceledBy,
                        CanceledAt = pt.CanceledAt,
                        CompletedAt = pt.CompletedAt,
                        CompletedBy = pt.CompletedBy,
                        ProcessAt = pt.ProcessAt,
                        ProcessBy = pt.ProcessBy,
                        CreatedAt = pt.CreatedAt,
                        UpdatedAt = pt.UpdatedAt,
                        UpdatedBy = pt.UpdatedBy,
                        CanceledPantryBy = pt.CanceledPantryBy,
                        RejectedPantryBy = pt.RejectedPantryBy,
                        CompletedPantryBy = pt.CompletedPantryBy,
                        ProcessPantryBy = pt.ProcessPantryBy,
                        Timezone = pt.Timezone,
                        FromPantry = pt.FromPantry,
                        ToPantry = pt.ToPantry,
                        Pending = pt.Pending,
                        PendingAt = pt.PendingAt,
                        PackageId = pt.PackageId,
                        ApprovedBy = pt.ApprovedBy,
                        ApprovedAt = pt.ApprovedAt,
                        BookingRoomName = b.RoomName,
                        BookingTitle = b.Title,
                        BookingIsApprove = b.IsApprove,
                        BookingIsCanceled = b.IsCanceled,
                        BookingDate = b.Date,
                        BookingStart = b.Start,
                        BookingEnd = b.End,
                    }).AsQueryable();

        if (entity?.StartDate != null && entity?.EndDate != null)
        {
            query = query.Where(q =>
                q.OrderDatetime >= entity.StartDate.ToDateTime(TimeOnly.MinValue)
                && q.OrderDatetime <= entity.EndDate.ToDateTime(TimeOnly.MaxValue)
            );
        }

        var recordsTotal = await query.CountAsync();

        if (entity?.PackageId != null)
        {
            query = query.Where(q => q.PackageId == entity.PackageId);
        }

        var recordsFiltered = await query.CountAsync();

        if (limit > 0)
        {
            query = query
                    .Skip(offset)
                    .Take(limit);
        }

        var result = await query.ToListAsync();

        return new DataTableEntity<PantryTransaksiSelect>
        {
            Collections = result,
            RecordsTotal = recordsTotal,
            RecordsFiltered = recordsFiltered
        };
    }

    public async Task<PantryTransaksiOrderApproval?> PrintOrderApprovalAsync(string pantryTransaksiId)
    {
        if (string.IsNullOrEmpty(pantryTransaksiId))
        {
            return null;
        }

        var query = from pt in context.PantryTransaksis
                    from b in context.Bookings
                        .Where(b => b.BookingId == pt.BookingId).DefaultIfEmpty()
                    from r in context.Rooms
                        .Where(r => r.Radid == b.RoomId).DefaultIfEmpty()
                    from bu in context.Buildings
                        .Where(bu => bu.Id == r.BuildingId).DefaultIfEmpty()
                    from bfl in context.BuildingFloors
                        .Where(bfl => bfl.Id == r.FloorId).DefaultIfEmpty()
                    from pmp in context.PantryMenuPakets
                        .Where(pmp => pmp.Id == pt.PackageId).DefaultIfEmpty()
                    where pt.Id == pantryTransaksiId 
                        // && pt.ApprovedBy != null 
                        && pt.IsDeleted == 0
                    select new PantryTransaksiOrderApproval
                    {
                        PantryPackageName = pmp.Name,
                        PantryTransaksiId = pt.Id,
                        BookingId = b.BookingId,
                        RoomId = r.Radid,
                        EmployeeId = pt.EmployeeId,
                        ApprovedBy = pt.ApprovedBy,
                        BookingTitle = b.Title,
                        RoomName = r.Name,
                        BuildingName = bu.Name,
                        BuildingFloorName = bfl.Name,
                        RoomImage = r.Image,
                        OrderStatus = pt.OrderSt,
                        OrderStatusName = pt.OrderStName,
                        BookingDate = b.Date,
                        BookingStart = b.Start,
                        BookingEnd = b.End,
                        ExpiredAt = pt.ExpiredAt,
                        UpdatedAt = pt.UpdatedAt
                    };

        return await query.FirstOrDefaultAsync();
    }
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
        entity.Property(e => e.PackageId)
            .HasMaxLength(50)
            .HasColumnName("package_id");
        entity.Property(e => e.ApprovedBy)
            .HasColumnName("approved_by");
        entity.Property(e => e.ApprovedAt)
            .HasPrecision(0)
            .HasDefaultValueSql("(getdate())")
            .HasColumnName("approved_at");
    }
}
