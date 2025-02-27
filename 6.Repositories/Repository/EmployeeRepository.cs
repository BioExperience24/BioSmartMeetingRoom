using System.Diagnostics.Metrics;
using System.Text.Json;
using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _6.Repositories.Repository;

public class EmployeeRepository : BaseRepository<Employee>
{

    private readonly MyDbContext _dbContext;

    public EmployeeRepository(MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<object>> GetItemsAsync()
    {
        var query = from employee in _dbContext.Employees
                    from alocationType in _dbContext.AlocationTypes
                        .Where(at => employee.CompanyId == at.Id).DefaultIfEmpty()
                    from alocation in _dbContext.Alocations
                        .Where(a => employee.DepartmentId == a.Id).DefaultIfEmpty()
                    where employee.IsDeleted == 0 
                    orderby employee.Generate ascending
                    select new { employee, alocationType = new { CompanyName = alocationType.Name }, alocation = new { DepartmentName = alocation.Name } };

        return await query.ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        var query = from employee in _dbContext.Employees
                    from alocationType in _dbContext.AlocationTypes
                        .Where(at => employee.CompanyId == at.Id).DefaultIfEmpty()
                    from alocation in _dbContext.Alocations
                        .Where(a => employee.DepartmentId == a.Id).DefaultIfEmpty()
                    where employee.IsDeleted == 0 
                    orderby employee.Generate ascending
                    select new { employee, alocationType = new { CompanyName = alocationType.Name }, alocation = new { DepartmentName = alocation.Name } };

        return await query.CountAsync();
    }

    public async Task<IEnumerable<object>> GetItemsWithoutUserAsync()
    {
        var query = (from employee in _dbContext.Employees
                    from user in _dbContext.Users
                        .Where(u => u.EmployeeId == employee.Id).DefaultIfEmpty()
                    where employee.IsDeleted == 0 
                    && (
                        user == null
                        || (
                            user != null
                            && user.IsDeleted == 1
                            && !_dbContext.Users
                                .Any(u => u.EmployeeId == employee.Id && u.IsDeleted == 0)
                        )
                    )
                    select new { employee }).Distinct();

        return await query.ToListAsync();
    }

    public override async Task<int> UpdateAsync(Employee item)
    {
        // _dbContext.Entry(item).State = EntityState.Modified;
        _dbContext.Entry(item).Property(e => e.Generate).IsModified = false;

        return await _dbContext.SaveChangesAsync();
    }

    public async Task<Employee?> GetItemByIdAsync(string id)
    {
        var query = from employee in _dbContext.Employees
                    where employee.Id == id
                    select employee;

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<object>> GetAllItemByIdsAsync(string[] ids)
    {
        if (ids.Length == 0)
        {
            return new List<object>();
        }

        var query = from employee in _dbContext.Employees
                    from alocationType in _dbContext.AlocationTypes
                        .Where(at => employee.CompanyId == at.Id).DefaultIfEmpty()
                    from alocation in _dbContext.Alocations
                        .Where(a => employee.DepartmentId == a.Id).DefaultIfEmpty()
                    where ids.Contains(employee.Id)
                    select new { employee, alocationType = new { CompanyName = alocationType.Name }, alocation = new { DepartmentName = alocation.Name } };

        var result = await query.ToListAsync();

        return result;
    }

    public async Task<EmployeeReportOrganizerUsageDataTable> GetAllOrganizerUsageReportItemAsync(EmployeeFilter? filter = null, int limit = 0, int offset = 0)
    {
        var participants = getParticipantsQuery(filter);

        var query = from employee in _dbContext.Employees
                    from participant in participants
                        .Where(p => employee.Nik == p.Nik).DefaultIfEmpty()
                    from alocationType in _dbContext.AlocationTypes
                        .Where(at => employee.CompanyId == at.Id).DefaultIfEmpty()
                    from alocation in _dbContext.Alocations
                        .Where(a => employee.DepartmentId == a.Id).DefaultIfEmpty()
                    where employee.IsDeleted == 0
                    orderby participant.TotalMeeting descending
                    select new EmployeeReportOrganizerUsage {
                        Employee = employee,
                        CompanyName = alocationType.Name,
                        DepartmentName = alocation.Name,
                        TotalMeeting = participant.TotalMeeting ?? 0,
                        TotalReschedule = participant.TotalReschedule ?? 0,
                        TotalCancel = participant.TotalCancel ?? 0,
                        TotalApprove = participant.TotalApprove ?? 0,
                    };

        var recordsTotal = await query.CountAsync();

        if (filter?.Nik != null)
        {
            query = query.Where(q => q.Employee!.Nik == filter!.Nik);
        }

        var recordsFiltered = await query.CountAsync();

        if (limit > 0)
        {
            query = query
                    .Skip(offset)
                    .Take(limit);
        }

        var result = await query.ToListAsync();

        return new EmployeeReportOrganizerUsageDataTable {
            Collections = result,
            RecordsTotal = recordsTotal,
            RecordsFiltered = recordsFiltered
        };
    }

    // INFO: method ini merupakan method alternatif dari method GetAllOrganizerUsageReportItemAsync.
    public async Task<(IEnumerable<EmployeeReportOrganizerUsage>, int, int)> GetAllOrganizerUsageReportItemAltAsync(EmployeeFilter? filter = null, int limit = 0, int offset = 0)
    {
        var ttlParticipant = from bi in _dbContext.BookingInvitations
                            from b in _dbContext.Bookings.Where(b => bi.BookingId == b.BookingId)
                            from r in _dbContext.Rooms.Where(r => b.RoomId == r.Radid)
                            from bu in _dbContext.Buildings.Where(bu => r.BuildingId == bu.Id)
                            where bi.IsPic == 1
                            && bi.Internal == 1
                            && b.IsAlive != 0
                            select new { bi, b, bu };

        var ttlBook = from b in _dbContext.Bookings
                    from bi in _dbContext.BookingInvitations
                        .Where(bi => b.BookingId == bi.BookingId).DefaultIfEmpty()
                    from r in _dbContext.Rooms.Where(r => b.RoomId == r.Radid)
                    from bu in _dbContext.Buildings.Where(bu => r.BuildingId == bu.Id)
                    where bi.IsPic == 1
                    && bi.Internal == 1
                    && b.IsAlive != 0
                    select new { b, bi, bu };

        if (filter?.DateStart != null && filter?.DateEnd != null)
        {
            ttlParticipant = ttlParticipant.Where(q => 
                q.b!.Date >= filter.DateStart
                && q.b!.Date <= filter.DateEnd
            );

            ttlBook = ttlBook.Where(q => 
                q.b!.Date >= filter.DateStart
                && q.b!.Date <= filter.DateEnd
            );
        }

        if (filter?.BuildingId > 0)
        {
            ttlParticipant = ttlParticipant.Where(q => q.bu!.Id == filter.BuildingId);
            
            ttlBook = ttlBook.Where(q => q.bu!.Id == filter.BuildingId);
        }

        if (filter?.RoomId != null)
        {
            ttlParticipant = ttlParticipant.Where(q => q.b!.RoomId == filter.RoomId);
            
            ttlBook = ttlBook.Where(q => q.b!.RoomId == filter.RoomId);
        }

        var participants = (from bookInvitation in _dbContext.BookingInvitations
                            where bookInvitation.IsDeleted == 0
                            select new {
                                Nik = bookInvitation.Nik,
                                TotalMeeting = (int?) (
                                    from ttlMeeting in ttlParticipant
                                    where ttlMeeting.bi.Nik == bookInvitation.Nik
                                    select ttlMeeting
                                ).Count(),
                                TotalReschedule = (int?) (
                                    from ttlReSchedule in ttlBook
                                    where ttlReSchedule.bi.Nik == bookInvitation.Nik
                                    && ttlReSchedule.b.IsRescheduled == 1
                                    select ttlReSchedule
                                ).Count(),
                                TotalCancel = (int?) (
                                    from ttlCancel in ttlBook
                                    where ttlCancel.bi.Nik == bookInvitation.Nik
                                    && ttlCancel.b.IsCanceled == 1
                                    select ttlCancel
                                ).Count(),
                                TotalApprove = (int?) (
                                    from ttlApprove in ttlBook
                                    where ttlApprove.bi.Nik == bookInvitation.Nik
                                    && (ttlApprove.b.IsApprove == 1 || ttlApprove.b.IsApprove == 3)
                                    select ttlApprove
                                ).Count()
                            }).Distinct();

        var query = from employee in _dbContext.Employees
                    from participant in participants
                        .Where(p => employee.Nik == p.Nik).DefaultIfEmpty()
                    from alocationType in _dbContext.AlocationTypes
                        .Where(at => employee.CompanyId == at.Id).DefaultIfEmpty()
                    from alocation in _dbContext.Alocations
                        .Where(a => employee.DepartmentId == a.Id).DefaultIfEmpty()
                    where employee.IsDeleted == 0
                    orderby participant.TotalMeeting descending
                    select new EmployeeReportOrganizerUsage {
                        Employee = employee,
                        CompanyName = alocationType.Name,
                        DepartmentName = alocation.Name,
                        TotalMeeting = (participant.TotalMeeting != null) ? participant.TotalMeeting : 0,
                        TotalReschedule = (participant.TotalReschedule != null) ? participant.TotalReschedule : 0,
                        TotalCancel = (participant.TotalCancel != null) ? participant.TotalCancel : 0,
                        TotalApprove = (participant.TotalApprove != null) ? participant.TotalApprove : 0,
                    };

        var recordsTotal = await query.CountAsync();

        if (filter?.Nik != null)
        {
            query = query.Where(q => q.Employee!.Nik == filter!.Nik);
        }

        var recordsFiltered = await query.CountAsync();

        if (limit > 0)
        {
            query = query
                    .Skip(offset)
                    .Take(limit);
        }

        var result = await query.ToListAsync();

        return (result, recordsTotal, recordsFiltered);
    }

    public async Task<EmployeeReportAttendeesDataTable> GetAllAttendeesReportItemAsync(EmployeeFilter? filter = null, int limit = 0, int offset = 0)
    {
        var participants = getParticipantsQuery(filter);

        var query = from employee in _dbContext.Employees
                    from participant in participants
                        .Where(p => employee.Nik == p.Nik).DefaultIfEmpty()
                    from alocationType in _dbContext.AlocationTypes
                        .Where(at => employee.CompanyId == at.Id).DefaultIfEmpty()
                    from alocation in _dbContext.Alocations
                        .Where(a => employee.DepartmentId == a.Id).DefaultIfEmpty()
                    where employee.IsDeleted == 0
                    orderby participant.TotalMeeting descending
                    select new EmployeeReportAttendees {
                        Employee = employee,
                        CompanyName = alocationType.Name,
                        DepartmentName = alocation.Name,
                        TotalMeeting = participant.TotalMeeting ?? 0,
                    };

        var recordsTotal = await query.CountAsync();

        if (filter?.Nik != null)
        {
            query = query.Where(q => q.Employee!.Nik == filter!.Nik);
        }

        var recordsFiltered = await query.CountAsync();

        if (limit > 0)
        {
            query = query
                    .Skip(offset)
                    .Take(limit);
        }

        var result = await query.ToListAsync();

        return new EmployeeReportAttendeesDataTable {
            Collections = result,
            RecordsTotal = recordsTotal,
            RecordsFiltered = recordsFiltered
        };
    }
    
    private IQueryable<EmployeeTotalParticipant> getParticipantsQuery(EmployeeFilter? filter = null)
    {
        var ttlParticipant = from bi in _dbContext.BookingInvitations
                            from b in _dbContext.Bookings.Where(b => bi.BookingId == b.BookingId)
                            from r in _dbContext.Rooms.Where(r => b.RoomId == r.Radid)
                            from bu in _dbContext.Buildings.Where(bu => r.BuildingId == bu.Id)
                            where bi.IsPic == 1
                            && bi.Internal == 1
                            && b.IsAlive != 0
                            select new { bi, b, bu };

        if (filter?.DateStart != null && filter?.DateEnd != null)
        {
            ttlParticipant = ttlParticipant.Where(q => 
                q.b!.Date >= filter.DateStart
                && q.b!.Date <= filter.DateEnd
            );
        }

        if (filter?.BuildingId > 0)
        {
            ttlParticipant = ttlParticipant.Where(q => q.bu!.Id == filter.BuildingId);
        }

        if (filter?.RoomId != null)
        {
            ttlParticipant = ttlParticipant.Where(q => q.b!.RoomId == filter.RoomId);
        }

        var participants = (
            from bookInvitation in _dbContext.BookingInvitations
            join tpg in ttlParticipant on bookInvitation.Nik equals tpg.bi.Nik into ttlGroup
            where bookInvitation.IsDeleted == 0
            select new EmployeeTotalParticipant {
                Nik = bookInvitation.Nik,
                TotalMeeting = ttlGroup.Count(),
                TotalReschedule = ttlGroup.Count(g => g.b.IsRescheduled == 1),
                TotalCancel = ttlGroup.Count(g => g.b.IsCanceled == 1),
                TotalApprove = ttlGroup.Count(g => g.b.IsApprove == 1 || g.b.IsApprove == 3),
            }
        ).Distinct();

        return participants;
    }
}

