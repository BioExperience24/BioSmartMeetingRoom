

using System.Reflection.Metadata.Ecma335;

namespace _6.Repositories.Repository
{
    public class RoomDisplayRepository : BaseLongRepository<RoomDisplay>
    {
        private readonly MyDbContext _dbContext;
        public RoomDisplayRepository(MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountAsync()
        {
            var query = from roomDisplay in _dbContext.RoomDisplays
                        from room in _dbContext.Rooms
                                .Where(q => q.Radid == roomDisplay.RoomId)
                        where roomDisplay.IsDeleted == 0 && room.IsDeleted == 0
                        orderby room.Id ascending
                        select roomDisplay;
            
            return await query.CountAsync();
        }

        public async Task<IEnumerable<RoomDisplaySelect>> GetAllItemAsync()
        {
            var query = from rd in _dbContext.RoomDisplays
                        from r in _dbContext.Rooms.Where(r => r.Radid == rd.RoomId)
                        from b in _dbContext.Buildings.Where(b => b.Id == r.BuildingId)
                        from bf in _dbContext.BuildingFloors.Where(bf => bf.Id == rd.FloorId)
                        where rd.IsDeleted == 0 && r.IsDeleted == 0
                        orderby r.Id ascending
                        select new RoomDisplaySelect
                        {
                            Id = rd.Id, 
                            RoomId = rd.RoomId, 
                            DisplaySerial = rd.DisplaySerial, 
                            Type = rd.Type, 
                            Background = rd.Background, 
                            BackgroundUpdate = rd.BackgroundUpdate, 
                            ColorOccupied = rd.ColorOccupied, 
                            ColorAvailable = rd.ColorAvailable, 
                            EnableSignage = rd.EnableSignage, 
                            SignageType = rd.SignageType, 
                            SignageMedia = rd.SignageMedia, 
                            SignageUpdate = rd.SignageUpdate, 
                            CreatedBy = rd.CreatedBy, 
                            UpdatedBy = rd.UpdatedBy, 
                            CreatedAt = rd.CreatedAt, 
                            UpdatedAt = rd.UpdatedAt, 
                            IsDeleted = rd.IsDeleted, 
                            StatusSync = rd.StatusSync, 
                            Enabled = rd.Enabled, 
                            HardwareUuid = rd.HardwareUuid, 
                            HardwareInfo = rd.HardwareInfo, 
                            HardwareLastsync = rd.HardwareLastsync, 
                            RoomSelect = rd.RoomSelect, 
                            DisableMsg = rd.DisableMsg, 
                            Name = rd.Name, 
                            Description = rd.Description, 
                            BuildingId = rd.BuildingId, 
                            FloorId = rd.FloorId, 
                            RoomName = r.Name,
                            BuildingName = b.Name,
                            FloorName = bf.Name
                        };

            var list = await query.ToListAsync();

            return list;
        }

        public async Task<RoomDisplay> GetDisplaySerialBySerialNumber(string displaySerial)
        {
            var query = from roomDisplay in _dbContext.RoomDisplays
                        where roomDisplay.IsDeleted == 0 && roomDisplay.DisplaySerial == displaySerial
                        select roomDisplay;

            return await query.FirstOrDefaultAsync()!;
        }

        public async Task<List<RoomDisplayInformationSelect>> GetDataRoomDisplayByListID(long? displayId)
        {
            var query = from d in _dbContext.RoomDisplayInformations
                        join r in _dbContext.Rooms on d.RoomId equals r.Radid
                        where r.IsDeleted == 0 && d.DisplayId == displayId
                        orderby r.Name ascending
                        select new RoomDisplayInformationSelect
                        {
                            RoomId = d.RoomId,
                            Icon = d.Icon,
                            Distance = d.Distance,
                            RoomName = r.Name
                        };

            return await query.ToListAsync();
        }

        public async Task<List<RoomDisplayInformationMeetingDTO>> GetMeetingDisplayInformation(DateTime date, string serial)
        {
            var query = from rd in _dbContext.RoomDisplays
                        join rdi in _dbContext.RoomDisplayInformations on rd.Id equals rdi.DisplayId into rdiGroup
                        from rdi in rdiGroup.DefaultIfEmpty()
                        join r in _dbContext.Rooms on rdi.RoomId equals r.Radid
                        join b in _dbContext.Bookings on rdi.RoomId equals b.RoomId
                        join bi in _dbContext.BookingInvitations on b.BookingId equals bi.BookingId
                        join bf in _dbContext.BuildingFloors on r.FloorId equals bf.Id into bfGroup
                        from bf in bfGroup.DefaultIfEmpty()
                        join bu in _dbContext.Buildings on bf.BuildingId equals bu.Id into buGroup
                        from bu in buGroup.DefaultIfEmpty()
                        where b.ServerDate == date &&
                              rd.DisplaySerial == serial &&
                              r.IsDeleted == 0 &&
                              rd.IsDeleted == 0 &&
                              bi.IsPic == 1
                        orderby r.Name ascending
                        select new RoomDisplayInformationMeetingDTO
                        {
                            DisplayId = rdi.DisplayId,
                            RoomId = rdi.RoomId,
                            Icon = rdi.Icon,
                            Distance = rdi.Distance,
                            RoomName = r.Name,
                            Capacity = r.Capacity,
                            BookingId = b.BookingId,
                            Title = b.Title,
                            Date = b.Date,
                            Start = b.Start,
                            End = b.End,
                            EndMeeting = b.End.AddMinutes(b.ExtendedDuration.GetValueOrDefault(0)).TimeOfDay, // FIXED: Ensure it works even if ExtendedDuration is null
                            IsAlive = b.IsAlive,
                            IsApprove = b.IsApprove,
                            IsExpired = b.IsExpired,
                            IsCanceled = b.IsCanceled,
                            IsPrivate = b.IsPrivate,
                            OrganizerName = bi.Name,
                            PinRoom = bi.PinRoom,
                            BuildingName = bu.Name,
                            FloorName = bf.Name
                        };

            return await query.ToListAsync();
        }


    }
}