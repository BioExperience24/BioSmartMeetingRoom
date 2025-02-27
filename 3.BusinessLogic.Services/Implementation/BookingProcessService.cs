

using System.Transactions;
using _5.Helpers.Consumer.EnumType;

namespace _3.BusinessLogic.Services.Implementation
{
    public class BookingProcessService : IBookingProcessService
    {
        private readonly IMapper _mapper;

        private readonly BookingRepository _bookingRepo;
    
        private readonly ModuleBackendRepository _moduleBackendRepo;

        private readonly EmployeeRepository _employeeRepo;

        private readonly AlocationRepository _alocationRepo;

        private readonly RoomRepository _roomRepo;

        private readonly SettingRuleBookingRepository _settingRuleBookingRepo;

        private readonly PantryMenuPaketRepository _pantryMenuPaketRepo;

        private readonly BookingInvitationRepository _bookingInvitationRepo;

        private readonly PantryTransaksiRepository _pantryTransaksiRepo;

        private readonly PantryTransaksiDRepository _pantryTransaksiDetailRepo;

        public BookingProcessService(
            IMapper mapper,
            BookingRepository bookingRepo,
            ModuleBackendRepository moduleBackendRepo,
            EmployeeRepository employeeRepo,
            AlocationRepository alocationRepo,
            RoomRepository roomRepo,
            SettingRuleBookingRepository settingRuleBookingRepo,
            PantryMenuPaketRepository pantryMenuPaketRepo,
            BookingInvitationRepository bookingInvitationRepo,
            PantryTransaksiRepository pantryTransaksiRepo,
            PantryTransaksiDRepository pantryTransaksiDetailRepo
        )
        {
            _mapper = mapper;
            _bookingRepo = bookingRepo;
            _moduleBackendRepo = moduleBackendRepo;
            _employeeRepo = employeeRepo;
            _alocationRepo = alocationRepo;
            _roomRepo = roomRepo;
            _settingRuleBookingRepo = settingRuleBookingRepo;
            _pantryMenuPaketRepo = pantryMenuPaketRepo;
            _bookingInvitationRepo = bookingInvitationRepo;
            _pantryTransaksiRepo = pantryTransaksiRepo;
            _pantryTransaksiDetailRepo = pantryTransaksiDetailRepo;
        }

        public async Task<(BookingViewModel?, string?)> CreateBookingAsync(BookingVMCreateReserveFR request)
        {
            DateTime now = DateTime.Now;
            var bookingId = new DateTimeOffset(now).ToUnixTimeMilliseconds() + _Random.Numeric(3);
            var pakeTransaksiId = new DateTimeOffset(now).ToUnixTimeMilliseconds() + _Random.Numeric(3);
            TimeZoneInfo localZone = TimeZoneInfo.Local;  

            var bookDate = _String.ToDateOnly(_String.RemoveAllSpace(request.Date));
            var bookStart = _String.ToDateTime($"{_String.RemoveAllSpace(request.Date)} {_String.RemoveAllSpace(request.Start)}:00", "yyyy-MM-dd HH:mm:ss");
            var bookEnd = _String.ToDateTime($"{_String.RemoveAllSpace(request.Date)} {_String.RemoveAllSpace(request.End)}:00", "yyyy-MM-dd HH:mm:ss");
            var bookDuration = bookEnd - bookStart;

            var generalSetting = await _settingRuleBookingRepo.GetSettingRuleBookingTopOne();

            var pic = await _employeeRepo.GetItemByIdAsync(request.Pic);

            var pantryMenu = await _pantryMenuPaketRepo.GetPackageWithPantry(request.MeetingCategory);

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    // get modules
                    // var modules = await getModules();
                    
                    // get alocation
                    // var alocation = await getBookingAlocation(request.AlocationId);

                    // check room with the selected date and time
                    Room cRoom = new Room {
                        Radid = request.RoomId,
                        WorkDay = new List<string>{ _String.ToDayName(request.Date) },
                        WorkStart = (request.Start != "") 
                                    ? $"{_String.RemoveAllSpace(request.Start)}:00"
                                    : "00:00:00",
                        WorkEnd = (request.End != "") 
                                    ? $"{_String.RemoveAllSpace(request.End)}:00"
                                    : "23:59:00",
                    };
                    var roomExist = await _roomRepo.GetAllRoomAvailableAsync(cRoom);

                    if (!roomExist.Any())
                    {
                        return (null, "Room is not available.");
                    }   

                    var room = _mapper.Map<RoomViewModelAlt>(roomExist.First());

                    // check room is available with the selected date and time
                    Booking cBook = new Booking {
                        RoomId = request.RoomId,
                        Date = bookDate,
                        Start = bookStart,
                        End = bookEnd,
                    };
                    var bookingExist = await _bookingRepo.GetAllAvailableItemAsync(cBook);

                    if (bookingExist.Any())
                    {
                        return (null, "Room is not available");
                    }   

                    // booking stage
                    // storing booking data
                    Booking dBooking = new Booking {
                        BookingId = bookingId.ToString(),
                        BookingDevices = request.Device,
                        NoOrder = "TRX-" + bookingId.ToString(),
                        Title = request.Title,
                        RoomId = request.RoomId,
                        Date = bookDate,
                        Start = bookStart,
                        End = bookEnd,
                        TotalDuration = (int)bookDuration.TotalMinutes,
                        // DurationPerMeeting = (int)bookDuration.TotalHours,
                        DurationPerMeeting = generalSetting?.Duration,
                        CostTotalBooking = 0,
                        AlocationId = request.AlocationId,
                        AlocationName = request.AlocationName,
                        Pic = pic!.Name ?? "",
                        IsMeal = 0,
                        IsAlive = 1,
                        IsDeleted = 0,
                        IsRescheduled = 0,
                        IsCanceled = 0,
                        IsExpired = 0, 
                        IsDevice = 0,
                        ExternalLink = request.ExternalLink,
                        Note = request.Note ?? "",
                        RoomName = room.Name,
                        IsMerge = 0,
                        MergeRoomName = "",
                        MergeRoomId = "",
                        MergeRoom = "",
                        CreatedAt = now,
                        // CreatedBy = "", // uncomment jika sudah ada data auth
                        UpdatedAt = now,
                        // UpdatedBy = "", // uncomment jika sudah ada data auth
                        IsVip = 0,
                        VipUser = "",
                        IsApprove = 0,
                        UserApproval = "",
                        Category = (pantryMenu != null) ? (int)pantryMenu.PantryId : 0,
                        Timezone = localZone.Id,
                        IsPrivate = request.IsPrivate == "on" ? 1 : 0,

                        ServerDate = bookDate.ToDateTime(TimeOnly.MinValue),
                        ServerStart = bookStart,
                        ServerEnd = bookEnd,

                        CanceledNote = "",
                        EarlyEndedBy = "",
                        ExpiredBy = "",
                        RescheduledBy = "",
                        CanceledBy = "",

                        Participants = ""
                    };

                    var booking = await _bookingRepo.Create(dBooking);
                    // BookingViewModel? booking = null;
                    
                    // attendess stage
                    // storing attendees data
                    var attendees = new List<BookingInvitation>();
                    // generate internal attendees
                    var internalAttendees = await generateInternalAttendees(request.InternalAttendees);

                    if (internalAttendees.Any())
                    {
                        foreach (var item in internalAttendees)
                        {
                            var isPic = (item.Id == request.Pic) ? 1 : 0;

                            var attendance = new BookingInvitation {
                                BookingId = booking!.BookingId,
                                // BookingId = bookingId.ToString(),
                                // BookingId = "1234",
                                Nik = item.Nik,
                                Internal = 1,
                                Email = item.Email,
                                Name = item.Name,
                                Company = item.CompanyName ?? "", // alocationtype name
                                Position = item.DepartmentName, // alocation name
                                IsPic = (short)isPic,
                                IsVip = item.IsVip,
                                IsDeleted = 0,
                                CreatedAt = now,
                                // CreatedBy = "", // uncomment jika sudah ada data auth
                                UpdatedAt = now,
                                // UpdatedBy = "", // uncomment jika sudah ada data auth
                            };
                            
                            attendees.Add(attendance);
                        }
                    }

                    // generate external attendees
                    var externalAttendess = generateExternalAttendees(request.ExternalAttendees);
                    if (externalAttendess.Any())
                    {
                        foreach (var item in externalAttendess)
                        {
                            var attendance = new BookingInvitation {
                                BookingId = booking!.BookingId,
                                // BookingId = bookingId.ToString(),
                                // BookingId = "1234",
                                Nik = item.Nik,
                                Internal = 0,
                                Email = item.Email,
                                Name = item.Name,
                                Company = item.Company, // alocationtype id
                                Position = item.Position, // alocation id
                                IsPic = 0,
                                IsVip = item.IsVip,
                                IsDeleted = 0,
                                CreatedAt = now,
                                // CreatedBy = "", // uncomment jika sudah ada data auth
                                UpdatedAt = now,
                                // UpdatedBy = "", // uncomment jika sudah ada data auth
                            };

                            attendees.Add(attendance);
                        }
                    }

                    if (attendees.Any())
                    {
                        await _bookingInvitationRepo.CreateBulk(attendees);
                    }
                    
                    // pantry stage
                    if (pantryMenu != null)
                    {
                        var orderNo = "ORD-" + _Random.AlphabetNumeric(4) + _Random.Numeric(3);
                        // storing transaction data
                        var dPantryTransaksi = new PantryTransaksi {
                            Id = pakeTransaksiId.ToString(),
                            PantryId = pantryMenu.PantryId,
                            OrderNo = orderNo,
                            EmployeeId = pantryMenu.Pantry?.EmployeeId ?? "",
                            BookingId = bookingId.ToString(),
                            IsBlive = 0,
                            RoomId = request.RoomId,
                            Via = request.Device,
                            Datetime = now,
                            OrderDatetime = bookStart,
                            OrderDatetimeBefore = bookEnd,
                            OrderSt = PantryTransaksiOrderStatus.Processing,
                            OrderStName = _Dictionary.PantryTransaksiOrderStatusDictionary[PantryTransaksiOrderStatus.Processing],
                            Process = 0,
                            Complete = 0,
                            Failed = 0,
                            Done = 0,
                            Note = "",
                            NoteReject = "",
                            NoteCanceled = "",
                            IsRejectedPantry = 0,
                            ProcessAt = now,
                            CreatedAt = now,
                            UpdatedAt = now,
                            IsDeleted = 0,
                            Timezone = localZone.Id,
                            
                            UpdatedBy = "",
                            ProcessBy = "",
                            CanceledBy = "",
                            RejectedBy = "",
                            CompletedBy = "",
                        };

                        await _pantryTransaksiRepo.AddAsync(dPantryTransaksi);

                        // storing transaction detail data
                        if (request.MenuItems.Any())
                        {
                            var menuItems = generatePantryTransaksiD(request.MenuItems);
                            if (menuItems.Any())
                            {
                                var dPantryTransaksiDs = new List<PantryTransaksiD>();
                                foreach (var item in menuItems)
                                {
                                    var dPantryTransaksiD = new PantryTransaksiD {
                                        TransaksiId = pakeTransaksiId.ToString(),
                                        MenuId = item.MenuId,
                                        Qty = item.Qty,
                                        NoteOrder = "Order Number: " + orderNo,
                                        NoteReject = "",
                                        Detailorder = "",
                                        Status = PantryTransaksiOrderStatus.Processing,
                                        IsRejected = 0,
                                        IsDeleted = 0,

                                        RejectedBy = "",
                                    };

                                    dPantryTransaksiDs.Add(dPantryTransaksiD);
                                }

                                if (dPantryTransaksiDs.Any())
                                {
                                    await _pantryTransaksiDetailRepo.CreateBulk(dPantryTransaksiDs);
                                }
                            }
                        }
                    }

                    scope.Complete();

                    return ((booking != null) ? _mapper.Map<BookingViewModel>(booking) : null, "Get success");
                }
                catch (Exception)
                {
                    throw;
                }
            }

            // return (null, "Failed to create booking");
        }

        private async Task<IEnumerable<EmployeeVMResp>> generateInternalAttendees(string[] request)
        {
            IEnumerable<EmployeeVMResp> internalAttendees = new List<EmployeeVMResp>();

            if (request.Any())
            {
                List<string> employeeIds = new List<string>();

                foreach (var item in request)
                {
                    var d = _String.DecodeUnicode(item);
                    var vmIAttendance = JsonSerializer.Deserialize<BookingVMAttendance>(d);
                    
                    employeeIds.Add(vmIAttendance!.Id);
                }

                var result = await _employeeRepo.GetAllItemByIdsAsync(employeeIds.ToArray());

                internalAttendees = _mapper.Map<List<EmployeeVMResp>>(result);
            }

            return internalAttendees;
        }

        private IEnumerable<BookingVMAttendance> generateExternalAttendees(string[] request)
        {
            List<BookingVMAttendance> externalAttendees = new List<BookingVMAttendance>();

            if (request.Any())
            {
                foreach (var item in request)
                {
                    var d = _String.DecodeUnicode(item);
                    var vmIAttendance = JsonSerializer.Deserialize<BookingVMAttendance>(d);
                    
                    externalAttendees.Add(vmIAttendance!);
                }
            }

            return externalAttendees;
        }
        
        private IEnumerable<BookingVMMenuItem> generatePantryTransaksiD(string[] request)
        {
            List<BookingVMMenuItem> menuItems = new List<BookingVMMenuItem>();

            if (request.Any())
            {
                foreach (var item in request)
                {
                    var d = _String.DecodeUnicode(item);
                    var vmMenuItem = JsonSerializer.Deserialize<BookingVMMenuItem>(d);
                    
                    menuItems.Add(vmMenuItem!);
                }
            }

            return menuItems;
        }
        
        private async Task<AlocationViewModel?> getBookingAlocation(string alocationId)
        {
            var item = await _alocationRepo.GetItemByIdAsync(alocationId);
            
            return (item == null) ? null : _mapper.Map<AlocationViewModel>(item);
        }

        private async Task<Dictionary<string, ModuleBackend?>> getModules()
        {
            var modules = await _moduleBackendRepo.GetItemsByModuleTextAsync(
                new string[]{
                    "module_pantry",
                    "module_price",
                    "module_user_vip",
                    "module_room_advance",
                    "module_loker",
                    "module_invoice",
                    "module_email",
                }
            );

            var dictModules = new Dictionary<string, ModuleBackend?>();

            foreach (var item in modules)
            {
                dictModules.Add(_Dictionary.ModuleAliasDictionary[item.ModuleText], item);
            }

            return dictModules;
        }   
    }
}