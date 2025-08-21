

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

        public async Task<RoomDisplaySelect?> GetDisplaySerialBySerialNumber(string displaySerial)
        {
            var query = from roomDisplay in _dbContext.RoomDisplays
                        join r in _dbContext.Rooms on roomDisplay.RoomId equals r.Radid
                        join b in _dbContext.Buildings on roomDisplay.BuildingId equals b.Id
                        join bf in _dbContext.BuildingFloors on roomDisplay.FloorId equals bf.Id
                        where roomDisplay.IsDeleted == 0 && roomDisplay.DisplaySerial == displaySerial
                        select new RoomDisplaySelect
                        {
                            Id = roomDisplay.Id,
                            RoomId = roomDisplay.RoomId,
                            DisplaySerial = roomDisplay.DisplaySerial,
                            Type = roomDisplay.Type,
                            Background = roomDisplay.Background,
                            BackgroundUpdate = roomDisplay.BackgroundUpdate,
                            ColorOccupied = roomDisplay.ColorOccupied,
                            ColorAvailable = roomDisplay.ColorAvailable,
                            EnableSignage = roomDisplay.EnableSignage,
                            SignageType = roomDisplay.SignageType,
                            SignageMedia = roomDisplay.SignageMedia,
                            SignageUpdate = roomDisplay.SignageUpdate,
                            CreatedBy = roomDisplay.CreatedBy,
                            UpdatedBy = roomDisplay.UpdatedBy,
                            CreatedAt = roomDisplay.CreatedAt,
                            UpdatedAt = roomDisplay.UpdatedAt,
                            StatusSync = roomDisplay.StatusSync,
                            Enabled = roomDisplay.Enabled,
                            HardwareUuid = roomDisplay.HardwareUuid,
                            HardwareInfo = roomDisplay.HardwareInfo,
                            HardwareLastsync = roomDisplay.HardwareLastsync,
                            RoomSelect = roomDisplay.RoomSelect,
                            DisableMsg = roomDisplay.DisableMsg,
                            Name = roomDisplay.Name,
                            Description = roomDisplay.Description,
                            BuildingId = roomDisplay.BuildingId,
                            FloorId = roomDisplay.FloorId,

                            RoomName = r.Name,
                            BuildingName = b.Name,
                            FloorName = bf.Name
                        };

            return await query.FirstOrDefaultAsync();
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
                            RoomName = r.Name,
                            WorkDay = r.WorkDay ?? new List<string>(),
                            WorkTime = r.WorkTime,
                            WorkStart = r.WorkStart,
                            WorkEnd = r.WorkEnd
                        };

            return await query.ToListAsync();
        }

        public async Task<List<RoomDisplayInformationMeetingDTO>> GetMeetingDisplayInformation(DateTime date, string serial)
        {
            DateTime parsedDate = date.Date;

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
                        where b.ServerDate!.Value.Date == parsedDate &&
                              rd.DisplaySerial == serial &&
                              r.IsDeleted == 0 &&
                              rd.IsDeleted == 0 &&
                              b.IsCanceled == 0 &&
                              b.IsExpired == 0 &&
                              bi.IsPic == 1
                        orderby b.Start ascending

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
                            FloorName = bf.Name,
                            WorkDay = r.WorkDay ?? new List<string>(),
                            WorkTime = r.WorkTime,
                            WorkStart = r.WorkStart,
                            WorkEnd = r.WorkEnd
                        };

            return await query.ToListAsync();
        }

        public async Task<RoomDisplayAvailableInformationMeetingDTO> GetMeetingRoomAvailableDisplayInformation(DateTime day, DateTime current, string serial)
        {
            var currentPlus30 = current.AddMinutes(30);

            var roomIds = await (
                from rd in _dbContext.RoomDisplays
                join rdi in _dbContext.RoomDisplayInformations on rd.Id equals rdi.DisplayId
                where rd.DisplaySerial == serial && rd.IsDeleted == 0
                select rdi.RoomId
            ).Distinct().ToListAsync();

            if (roomIds.Count == 0)
            {
                return new RoomDisplayAvailableInformationMeetingDTO
                {
                    Serial = serial,
                    TotalRoom = 0,
                    TotalAvailable = 0,
                    RoomNames = new List<string>()
                };
            }

            var activeRooms = await _dbContext.Rooms
                .Where(r => r.IsDeleted == 0 && roomIds.Contains(r.Radid))
                .Select(r => new { r.Radid, r.Name })
                .ToListAsync();

            var totalRoom = activeRooms.Count;

            var busyRoomIds = await _dbContext.Bookings
                .Where(b =>
                    roomIds.Contains(b.RoomId) &&
                    b.IsCanceled == 0 &&
                    b.IsExpired == 0 &&
                    b.EndEarlyMeeting == 0 &&
                    b.Start != null && b.End != null &&
                    b.Start.Date == day &&
                    b.Start <= currentPlus30 &&
                    b.End.AddMinutes((double)(b.ExtendedDuration ?? 0)) > current
                )
                .Select(b => b.RoomId)
                .Distinct()
                .ToListAsync();

            var availableRoomNames = activeRooms
                .Where(r => !busyRoomIds.Contains(r.Radid))
                .Select(r => r.Name)
                .ToList();

            return new RoomDisplayAvailableInformationMeetingDTO
            {
                Serial = serial,
                TotalRoom = totalRoom,
                TotalAvailable = availableRoomNames.Count,
                RoomNames = availableRoomNames
            };
        }
    }
}