

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
    }
}