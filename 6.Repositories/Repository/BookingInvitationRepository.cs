

namespace _6.Repositories.Repository
{
    public class BookingInvitationRepository : BaseLongRepository<BookingInvitation>
    {
        private readonly MyDbContext _dbContext;

        public BookingInvitationRepository(MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetCountAttendeesByPicAsync(BookingInvitationFilter? filter = null)
        {
            if (filter?.Nik == null)
            {
                return 0;
            }

            var invitationBookingIds = from bi in _dbContext.BookingInvitations
                                    from b in _dbContext.Bookings.Where(b => bi.BookingId == b.BookingId)
                                    from r in _dbContext.Rooms.Where(r => b.RoomId == r.Radid)
                                    from bu in _dbContext.Buildings.Where(bu => r.BuildingId == bu.Id)
                                    where bi.IsPic == 1
                                    && bi.Internal == 1
                                    && b.IsAlive != 0
                                    select new { bi, b, r, bu };

            // NIK PIC
            invitationBookingIds = invitationBookingIds.Where(q => q.bi!.Nik == filter.Nik);

            if (filter?.DateStart != null && filter?.DateEnd != null)
            {
                invitationBookingIds = invitationBookingIds.Where(q => 
                    q.b!.Date >= filter.DateStart
                    && q.b!.Date <= filter.DateEnd
                );
            }

            if (filter?.BuildingId > 0)
            {
                invitationBookingIds = invitationBookingIds.Where(q => q.bu!.Id == filter.BuildingId);
            }

            if (filter?.RoomId != null)
            {
                invitationBookingIds = invitationBookingIds.Where(q => q.b!.RoomId == filter.RoomId);
            }
            

            var query = from bi in _dbContext.BookingInvitations
                        where (
                            from bii in invitationBookingIds
                            select bii.b.BookingId
                        ).Contains(bi.BookingId)
                        select bi;

            if (filter?.Checkin == 1)
            {
                query = query.Where(q => q.Checkin == filter.Checkin);
            }

            if (filter?.AttendanceStatus != 0)
            {
                if (filter!.AttendanceStatus == 1)
                {
                    query = query.Where(q => q.AttendanceStatus == filter.AttendanceStatus);
                }

                if (filter!.AttendanceStatus == 2)
                {
                    query = query.Where(q => q.AttendanceStatus == 2 || q.AttendanceStatus == 0);
                }
            }

            return await query.CountAsync();
        }

        public async Task<IEnumerable<BookingInvitationEmployee>> GetAllFilteredByBookingIds(string[] bookingIds)
        {
            var query = from bi in _dbContext.BookingInvitations
                        from e in _dbContext.Employees.Where(e => bi.Nik == e.Nik)
                            .Where(e => bi.Nik == e.Nik).DefaultIfEmpty()
                        where bi.IsDeleted == 0
                        && bookingIds.Contains(bi.BookingId)
                        select new BookingInvitationEmployee
                        {
                            Id = bi.Id,
                            BookingId = bi.BookingId, 
                            Nik = bi.Nik, 
                            Internal = bi.Internal, 
                            AttendanceStatus = bi.AttendanceStatus, 
                            AttendanceReason = bi.AttendanceReason, 
                            ExecuteAttendance = bi.ExecuteAttendance, 
                            ExecuteDoorAccess = bi.ExecuteDoorAccess, 
                            Email = bi.Email, 
                            Name = bi.Name, 
                            Company = bi.Company, 
                            Position = bi.Position, 
                            IsPic = bi.IsPic, 
                            IsVip = bi.IsVip, 
                            PinRoom = bi.PinRoom, 
                            CreatedAt = bi.CreatedAt, 
                            CreatedBy = bi.CreatedBy, 
                            UpdatedAt = bi.UpdatedAt, 
                            UpdatedBy = bi.UpdatedBy, 
                            LastUpdate365 = bi.LastUpdate365, 
                            Checkin = bi.Checkin, 
                            EndMeeting = bi.EndMeeting,
                            IsDeleted = bi.IsDeleted,
                            EmployeeName = e.Name,
                            EmployeeNoPhone = e.NoPhone,
                            EmployeeEmail = e.Email
                        };
                        // select bi;

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<BookingInvitation?> GetPicFilteredByBookingId(string bookingId)
        {
            var query = from bi in _dbContext.BookingInvitations
                        where bi.IsDeleted == 0
                        && bi.BookingId == bookingId
                        && bi.IsPic == 1
                        select bi;

            var result = await query.FirstOrDefaultAsync();

            return result;
        }

        public async Task<BookingInvitation?> CheckDoorOpenMeetingPin(
            DateOnly date, DateTime startTime, DateTime endTime, string pin, string radId)
        {
            var query = from bi in _dbContext.BookingInvitations
                        join b in _dbContext.Bookings on bi.BookingId equals b.BookingId
                        where b.Date == date
                            && b.IsAlive == 1
                            && b.IsCanceled == 0
                            && b.IsExpired == 0
                            && bi.PinRoom == pin
                            && b.EndEarlyMeeting == 0
                            && b.RoomId == radId
                            && b.Start <= startTime 
                            && b.End >= endTime   
                        select bi;

            return await query.FirstOrDefaultAsync();
        }

        public async Task<BookingInvitation?> GetItemFilteredByBookingIdAndNik(string bookingId, string nik)
        {
            if (string.IsNullOrEmpty(bookingId) || string.IsNullOrEmpty(nik))
            {
                return null;
            }

            var query = from bi in _dbContext.BookingInvitations
                        where bi.BookingId == bookingId
                        && bi.Nik == nik
                        && bi.IsDeleted == 0
                        && bi.AttendanceStatus == 0
                        select bi;

            return await query.FirstOrDefaultAsync();
        }

    }
}