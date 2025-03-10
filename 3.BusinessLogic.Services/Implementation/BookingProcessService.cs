using System.Security.Claims;
using System.Transactions;
using _5.Helpers.Consumer.EnumType;
using _6.Repositories.DB;

namespace _3.BusinessLogic.Services.Implementation
{
    public class BookingProcessService : IBookingProcessService
    {
        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpCtx;

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

        private readonly PantryDetailRepository _pantryDetailRepo;

        private readonly SettingRuleBookingRepository _settingRuleBookingRepository;

        private readonly TimeScheduleRepository _timeScheduleRepo;

        private readonly SettingPantryConfigRepository _settingPantryConfigRepository;

        private readonly PantryTransaksiStatusRepository _pantryTransaksiStatusRepository;

        private readonly SettingInvoiceTextRepository _settingInvoiceTextRepository;

        private readonly BookingInvoiceRepository _bookingInvoiceRepo;

        public BookingProcessService(
            IMapper mapper,
            IHttpContextAccessor httpCtx,
            BookingRepository bookingRepo,
            ModuleBackendRepository moduleBackendRepo,
            EmployeeRepository employeeRepo,
            AlocationRepository alocationRepo,
            RoomRepository roomRepo,
            SettingRuleBookingRepository settingRuleBookingRepo,
            PantryMenuPaketRepository pantryMenuPaketRepo,
            BookingInvitationRepository bookingInvitationRepo,
            PantryTransaksiRepository pantryTransaksiRepo,
            PantryTransaksiDRepository pantryTransaksiDetailRepo,
            PantryDetailRepository pantryDetailRepo,
            SettingRuleBookingRepository settingRuleBookingRepository,
            TimeScheduleRepository timeScheduleRepo,
            SettingPantryConfigRepository settingPantryConfigRepository,
            PantryTransaksiStatusRepository pantryTransaksiStatusRepository,
            SettingInvoiceTextRepository settingInvoiceTextRepository,
            BookingInvoiceRepository bookingInvoiceRepo
        )
        {
            _mapper = mapper;
            _httpCtx = httpCtx;
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
            _pantryDetailRepo = pantryDetailRepo;
            _settingRuleBookingRepository = settingRuleBookingRepository;
            _timeScheduleRepo = timeScheduleRepo;
            _settingPantryConfigRepository = settingPantryConfigRepository;
            _pantryTransaksiStatusRepository = pantryTransaksiStatusRepository;
            _settingInvoiceTextRepository = settingInvoiceTextRepository;
            _bookingInvoiceRepo = bookingInvoiceRepo;
        }

        public async Task<(BookingViewModel?, string?)> CreateBookingAsync(BookingVMCreateReserveFR request)
        {
            // from token
            var authUserNIK = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
            // .from token

            // id
            string randomId = _Random.Numeric(10, true).ToString();
            string id = randomId; // booking id
            string invoiceId = randomId;
            var roomId = request.RoomId;
            // .id

            // time
            DateTime now = DateTime.Now;
            var timeStart = TimeOnly.FromTimeSpan(request.Start);
            var timeEnd = TimeOnly.FromTimeSpan(request.End);

            var bookDate = request.Date;
            var bookStart = bookDate.ToDateTime(timeStart);
            var bookEnd = bookDate.ToDateTime(timeEnd);
            var bookDuration = bookEnd - bookStart;
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            // .time


            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    // check available room
                    var room = await CheckAvailableRoom(roomId, bookDate, bookStart, bookEnd);

                    if (room == null)
                    {
                        return (null, "Room is not available");
                    }
                    // .check available room

                    // config & settings
                    var modules = await getModules();

                    int modulesRoomAdvEnabled = modules.ContainsKey("room_adv") ? (modules["room_adv"]?.IsEnabled ?? 0) - 0 : 0;
                    int modulesVipEnabled = modules.ContainsKey("vip") ? (modules["vip"]?.IsEnabled ?? 0) - 0 : 0;

                    var statusInvoice = await getStatusInvoiceName();

                    var alocation = await getBookingAlocation(request.AlocationId);

                    var generalSetting = await _settingRuleBookingRepo.GetSettingRuleBookingTopOne();

                    var setPantryConfig = await _settingPantryConfigRepository.GetSettingPantryConfigTopOne();
                    var pantryExpired = setPantryConfig?.PantryExpired ?? 0;
                    var pantryMaxOrderQty = setPantryConfig?.MaxOrderQty ?? 0;
                    var pantryBeforeOrderMeeting = setPantryConfig?.BeforeOrderMeeting ?? 0;
                    // .config & settings

                    // module price setting
                    var fHour = generalSetting?.Duration ?? 0;
                    var duration = bookDuration.TotalMinutes;
                    int reservationCost = 0;
                    long cost = 0;
                    double getHoursMeeting = 0;
                    double checkHours = 0;
                    if (modules.ContainsKey("price") && modules["price"]?.IsEnabled == 1)
                    {
                        cost = room.Price;
                        getHoursMeeting = Math.Floor(duration / fHour);
                        checkHours = duration % fHour;
                        if (checkHours > 0)
                        {
                            getHoursMeeting += 1;
                        }
                        reservationCost = (int)(cost * getHoursMeeting);
                    }
                    // .module price setting

                    // pic
                    var pic = await _employeeRepo.GetItemByIdAsync(request.Pic);
                    // .pic

                    // pantry package (pantry menu)
                    var pantryPackage = await _pantryMenuPaketRepo.GetPackageWithPantry(request.MeetingCategory);
                    // .pantry package (pantry menu)

                    // generate invoice
                    string formatInvoice = string.Empty;
                    BookingInvoice? bookingInvoiceCollection = null;
                    if (modules.ContainsKey("invoice") && modules["invoice"]?.IsEnabled == 1)
                    {
                        if (modules.ContainsKey("price") && modules["price"]?.IsEnabled == 1)
                        {
                            var bookingDate = request.Date.ToDateTime(TimeOnly.MinValue); // parse date string to DateTime
                            var years = bookingDate.Year; // get year from date
                            var y_years = bookingDate.ToString("yy"); // get year in two digits from date
                            var months = bookingDate.Month; // get month from date
                            var days = bookingDate.Day; // get day from date

                            string alocationOrderId = alocation != null ? alocation.Id + "-E-Meeting" : string.Empty;

                            var bookingOrderNo = await generateBookingOrderNumber(years.ToString());
                            formatInvoice = $"{bookingOrderNo}/{alocationOrderId}/{months}/{y_years}";
                        }

                        bookingInvoiceCollection = new BookingInvoice
                        {
                            InvoiceNo = invoiceId,
                            InvoiceFormat = formatInvoice,
                            BookingId = id,
                            RentCost = reservationCost,
                            Alocation = alocation?.Id ?? "",
                            TimeBefore = now,
                            CreatedAt = now,
                            CreatedBy = authUserNIK ?? "",
                            InvoiceStatus = "0",
                            IsDeleted = 0,
                        };
                    }
                    // generate invoice

                    // generate booking
                    Booking bookingCollection = new Booking
                    {
                        BookingId = id,
                        BookingDevices = request.Device ?? "web",
                        NoOrder = formatInvoice,
                        Title = request.Title,
                        RoomId = roomId,
                        Date = bookDate,
                        Start = bookStart,
                        End = bookEnd,
                        TotalDuration = (int)bookDuration.TotalMinutes,
                        DurationPerMeeting = fHour,
                        CostTotalBooking = reservationCost,
                        AlocationId = request.AlocationId,
                        AlocationName = request.AlocationName,
                        Pic = pic!.Name ?? "",
                        IsMeal = 0,
                        IsAlive = request.BookingType != "specialroom" ? 1 : 0,
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
                        CreatedBy = authUserNIK ?? "",
                        UpdatedAt = now,
                        UpdatedBy = authUserNIK ?? "",
                        IsVip = 0,
                        VipUser = "",
                        IsApprove = 0,
                        UserApproval = "",
                        Category = (pantryPackage != null) ? (int)pantryPackage.PantryId : 0,
                        Timezone = localZone.Id,
                        IsPrivate = request.IsPrivate == "on" ? 1 : 0,
                        ServerDate = bookStart,
                        ServerStart = bookStart,
                        ServerEnd = bookEnd,
                        CanceledNote = "",
                        EarlyEndedBy = "",
                        ExpiredBy = "",
                        RescheduledBy = "",
                        CanceledBy = "",
                        Participants = "",
                        BookingType = request.BookingType,

                        // bookingAdjustAdvanceMeeting
                        IsConfigSettingEnable = room.IsConfigSettingEnable ?? 0,
                        IsEnableApproval = room.IsEnableApproval ?? 0,
                        IsEnablePermission = room.IsEnablePermission ?? 0,
                        IsEnableRecurring = room.IsEnableRecurring ?? 0,
                        IsEnableCheckin = room.IsEnableCheckin ?? 0,
                        IsRealeaseCheckinTimeout = room.IsRealeaseCheckinTimeout ?? 0,
                        IsEnableCheckinCount = room.IsEnableCheckinCount ?? 0, 
                    };
                    
                    bookingCollection = await checkMeetingVipAndApprovalAccess(bookingCollection, room, modules);
                    // .generate booking

                    // generate pantry transaction & pantry transaction detail
                    PantryTransaksi? pantryTransaksiCollection = null;
                    List<PantryTransaksiD> pantryTransaksiDCollections = new List<PantryTransaksiD>();
                    if (pantryPackage != null && setPantryConfig?.Status == 1 && modules.ContainsKey("pantry") && modules["pantry"]?.IsEnabled == 1)
                    {
                        var pantryTransaksiStatus = await _pantryTransaksiStatusRepository.GetAllPantryTransaksiStatus(0);

                        // pantry detail (menu item)
                        var pantryDetail = generatePantryTransaksiD(request.MenuItems);
                        // .pantry detail (menu item)
                        
                        var orderPantryDateTime = DateTime.Parse($"{request.Date} {request.Start}");
                        var orderPantryDateTimeBefore = orderPantryDateTime.AddMinutes(-pantryBeforeOrderMeeting);
                        var orderPantryDate = request.Date.ToDateTime(TimeOnly.MinValue);

                        var pantrTrxId = $"MEETING-{DateTime.Now:yyyyMMddHHmmss}{_Random.Numeric(3)}";
                        var orderNo = generatePantryOrderNumber(pantryPackage.PantryId, orderPantryDate);

                        pantryTransaksiCollection = new PantryTransaksi
                        {
                            Id = pantrTrxId,
                            PantryId = pantryPackage.PantryId,
                            RoomId = roomId,
                            PackageId = request.MeetingCategory,
                            OrderNo = orderNo,
                            EmployeeId = pic?.Nik ?? "",
                            BookingId = id,
                            Via = "booking",
                            Datetime = now,
                            OrderDatetime = orderPantryDateTime,
                            OrderDatetimeBefore = orderPantryDateTimeBefore,
                            OrderSt = (int)(pantryTransaksiStatus?.Id ?? 0),
                            OrderStName = pantryTransaksiStatus?.Name ?? "",
                            Process = 0,
                            Complete = 0,
                            Failed = 0,
                            Done = 0,
                            Note = "",
                            CreatedAt = now,
                            IsDeleted = 0,

                            UpdatedAt = now,
                            UpdatedBy = authUserNIK ?? "",
                            Timezone = localZone.Id,
                            IsBlive = 0,
                            
                            ProcessBy = string.Empty,
                            CompletedBy = string.Empty,

                            IsRejectedPantry = 0,
                            RejectedBy = string.Empty,
                            NoteReject = string.Empty,
                            
                            CanceledBy = string.Empty,
                            NoteCanceled = string.Empty,
                        };

                        if (pantryDetail.Any())
                        {
                            foreach (var item in pantryDetail)
                            {
                                if (pantryMaxOrderQty < item.Qty)
                                {
                                    return (null, $"Orders per item exceed. Maximum quantity of {pantryMaxOrderQty}");
                                }

                                PantryTransaksiD pantryTransaksiD = new PantryTransaksiD
                                {
                                    TransaksiId = pantrTrxId,
                                    MenuId = item.MenuId,
                                    Qty = item.Qty,
                                    NoteOrder = "Order Number: " + orderNo,
                                    NoteReject = string.Empty,
                                    IsRejected = 0,
                                    IsDeleted = 0,
                                    Status = 0,

                                    Detailorder = string.Empty,
                                    RejectedBy = string.Empty,
                                };

                                pantryTransaksiDCollections.Add(pantryTransaksiD);
                            }
                        }
                    }
                    // .generate pantry transaction & pantry transaction detail

                    // generate attendees
                    var attendanceCollections = new List<BookingInvitation>();

                    // internal attendees
                    var internalAttendees = await generateInternalAttendees(request.InternalAttendees);
                    if (internalAttendees.Any())
                    {
                        foreach (var item in internalAttendees)
                        {
                            // var isPic = (item.Id == request.Pic) ? 1 : 0;
                            var isPic = (item.Id == pic!.Id) ? 1 : 0;
                            var pinRoom =_Random.Numeric(6).ToString();

                            var attendance = new BookingInvitation
                            {
                                BookingId = bookingCollection.BookingId,
                                Nik = item.Nik,
                                Name = item.Name,
                                IsVip = pic.Nik == bookingCollection.VipUser ? 0 : item.IsVip,
                                Internal = 1,
                                AttendanceStatus = 0,
                                Email = item.Email,
                                IsPic = (short)isPic,
                                PinRoom = pinRoom,
                                Company = item.CompanyName ?? "", // alocationtype name
                                Position = item.DepartmentName, // alocation name
                                CreatedAt = now,
                                CreatedBy = authUserNIK ?? "",
                                UpdatedAt = now,
                                UpdatedBy = authUserNIK ?? "",
                                IsDeleted = 0,
                            };

                            attendanceCollections.Add(attendance);
                        }

                        // buat data pic jika tidak ada pic pada internal attendees
                        if (attendanceCollections.Where(q => q.IsPic == 1).FirstOrDefault() == null && pic != null)
                        {
                            attendanceCollections.Add(new BookingInvitation
                            {
                                BookingId = bookingCollection.BookingId,
                                Nik = pic.Nik,
                                Name = pic.Name ?? string.Empty,
                                IsVip = pic.Nik == bookingCollection.VipUser ? 0 : pic.IsVip,
                                Internal = 1,
                                AttendanceStatus = 0,
                                Email = pic.Email,
                                IsPic = 1,
                                PinRoom = _Random.Numeric(6).ToString(),
                                Company = alocation?.TypeName ?? "", // alocationtype name
                                Position = alocation?.Name, // alocation name
                                CreatedAt = now,
                                CreatedBy = authUserNIK ?? "",
                                UpdatedAt = now,
                                UpdatedBy = authUserNIK ?? "",
                                IsDeleted = 0,
                            });
                        }
                    }
                    // .internal attendees

                    // external attendees
                    var externalAttendess = generateExternalAttendees(request.ExternalAttendees);
                    if (externalAttendess.Any())
                    {
                        foreach (var item in externalAttendess)
                        {
                            var pinRoom =_Random.Numeric(6).ToString();
                            
                            var attendance = new BookingInvitation
                            {
                                BookingId = bookingCollection.BookingId,
                                Nik = item.Nik,
                                Internal = 0,
                                Email = item.Email,
                                Name = item.Name,
                                Company = item.Company, // alocationtype id
                                Position = item.Position, // alocation id
                                IsPic = 0,
                                IsVip = 0,
                                PinRoom = pinRoom,
                                CreatedAt = now,
                                CreatedBy = authUserNIK ?? "",
                                UpdatedAt = now,
                                UpdatedBy = authUserNIK ?? "",
                                IsDeleted = 0,
                            };

                            attendanceCollections.Add(attendance);
                        }
                    }
                    // .external attendees
                    
                    // .generate attendees

                    // booking invoice stage
                    // storing booking invoice data
                    if (bookingInvoiceCollection != null)
                    {
                        await _bookingInvoiceRepo.Create(bookingInvoiceCollection);
                    }

                    // booking stage
                    // storing booking data
                    var booking = await _bookingRepo.Create(bookingCollection);

                    // pantry stage
                    if (pantryTransaksiCollection != null)
                    {
                        // storing transaction data
                        await _pantryTransaksiRepo.AddAsync(pantryTransaksiCollection);

                        if (pantryTransaksiDCollections.Any())
                        {
                            // storing transaction detail data
                            await _pantryTransaksiDetailRepo.CreateBulk(pantryTransaksiDCollections);
                        }
                    }

                    // attendess stage
                    // storing attendees data
                    if (attendanceCollections.Any())
                    {
                        await _bookingInvitationRepo.CreateBulk(attendanceCollections);
                    }

                    // TODO: send mail & notif stage

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

        public async Task<(IEnumerable<BookingViewModel>, int, int)> GetAllItemDataTablesAsync(BookingVMDataTableFR request)
        {
            var authUserLevel = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
            var authUserNIK = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;

            var entity = new Booking { };

            if (request.BookingDate != null)
            {
                string[] dates = request.BookingDate.Split(" - ");
                entity.DateStart = _String.ToDateOnly(_String.RemoveAllSpace(dates[0]), "MM/dd/yyyy");
                entity.DateEnd = _String.ToDateOnly(_String.RemoveAllSpace(dates[1]), "MM/dd/yyyy");
            }

            if (request.BookingOrganizer != null)
            {
                entity.Pic = request.BookingOrganizer;
            }

            if (request.BookingRoom != null)
            {
                entity.RoomId = request.BookingRoom;
            }

            if (request.BookingBuilding > 0)
            {
                entity.BuildingId = request.BookingBuilding;
            }
            
            else if (authUserLevel == "2")
            {
                entity.AuthUserNIK = authUserNIK;
            }

            var (items, recordsTotal, recordsFiltered) = await _bookingRepo.GetAllItemWithEntityAsync(entity, request.Length, request.Start);

            var result = _mapper.Map<List<BookingViewModel>>(items);

            var bookingIds = result.Select(q => q.BookingId).ToArray();

            var bookingInvitations = await _bookingInvitationRepo.GetAllFilteredByBookingIds(bookingIds);

            // var BookingMenus = await _pantryDetailRepo.GetAllFilteredByBookingIds(bookingIds);

            var no = request.Start + 1;

            foreach (var item in result)
            {
                item.No = no;
                item.BookingDate = item.Date.ToString("dd MMM yyyy");

                var timeStart = item.Start.ToString("HH:mm");
                var timeEnd = item.End.ToString("HH:mm");
                if (item.ExtendedDuration > 0)
                {
                    timeEnd = item.End.AddMinutes((double)item.ExtendedDuration).ToString("HH:mm");
                }
                item.Time = $"{timeStart} - {timeEnd}";

                var externalAttendees = bookingInvitations
                                                .Where(q => q.BookingId == item.BookingId && q.Internal == 0)
                                                .ToList();
                List<BookingInvitationVMCategory> MapExternalAttendees = new();
                _mapper.Map(externalAttendees, MapExternalAttendees);

                var internalAttendees = bookingInvitations
                                                .Where(q => q.BookingId == item.BookingId && q.Internal == 1)
                                                .ToList();

                var pic = internalAttendees.Where(q => q.IsPic == 1).FirstOrDefault();
                if (pic != null)
                {
                    item.PicNIK = pic.Nik;
                }

                List<BookingInvitationVMCategory> MapInternalAttendees = new();
                _mapper.Map(internalAttendees, MapInternalAttendees);

                item.AttendeesList = new BookingInvitationVMList
                {
                    ExternalAttendess = MapExternalAttendees,
                    InternalAttendess = MapInternalAttendees
                };

                // var menus = BookingMenus.Where(q => item.BookingId == q.BookingId).ToList();

                // item.PackageId = menus.Select(q => q.PackageId).FirstOrDefault() ?? string.Empty;

                // item.PackageMenus = _mapper.Map<List<PantryDetailVMMenus>>(menus);

                no++;
            }

            return (result, recordsTotal, recordsFiltered);
        }

        public async Task<EmployeeViewModel?> GetPicFilteredByBookingIdAsync(string bookingId)
        {
            var picBI = await _bookingInvitationRepo.GetPicFilteredByBookingId(bookingId);

            if (picBI == null)
            {
                return null;
            }

            var picEmp = await _employeeRepo.GetItemByNikAsync(picBI.Nik);

            if (picEmp == null)
            {
                return null;
            }

            var result = _mapper.Map<EmployeeViewModel>(picEmp);

            return result;
        }

        public async Task<ReturnalModel> CheckRescheduleDateAsync(string bookingId, DateOnly date, string roomId)
        {
            ReturnalModel ret = new();

            var dataBooking = await _bookingRepo.GetItemFilteredByBookingIdAsync(bookingId);

            if (string.IsNullOrEmpty(dataBooking?.Title))
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Data booking not exist";
                return ret;
            }

            var dataRoom = await _roomRepo.GetAllRoomWithRadidsItemAsycn(new string[] { roomId });
            var dataRoomMap = _mapper.Map<List<RoomVMCheckReschedule>>(dataRoom);

            var dataSettingGeneral = await _settingRuleBookingRepo.GetSettingRuleBookingTopOne();
            var dataSettingGeneralMap = _mapper.Map<SettingRuleBookingViewModel>(dataSettingGeneral);

            // var setDuration = dataSettingGeneralMap?.Duration ?? 30;
            var setDuration = 15;

            var timeArray = await _timeScheduleRepo.GetAllTimeScheduleFilteredByDurationAsync(setDuration);

            // Console.WriteLine("-------------timeDatas-------------");
            // var timeDatas = timeArray.Select(q => date.ToDateTime(TimeOnly.Parse(q.Time))).ToArray();
            // Console.WriteLine("timeDatas: " + JsonSerializer.Serialize(timeDatas));

            var roomIds = dataRoomMap.Select(q => q.Radid).ToArray();

            var dataBookingByDateRooms = await _bookingRepo.GetBookingsByDateRoomAsync(roomIds, date);

            foreach (var room in dataRoomMap)
            {
                var rId = room.Radid;
                var roomTimeData = new List<RoomVMTimeData>();

                foreach (var time in timeArray)
                {
                    var parsedTime = TimeOnly.Parse(time.Time);
                    var timeData = date.ToDateTime(parsedTime);

                    var startTime = timeData.TimeOfDay;
                    var endTime = timeData.AddMinutes(-1).TimeOfDay;

                    var dataBookingByTime = dataBookingByDateRooms
                                                .Where(q =>
                                                    q.RoomId == rId
                                                    && q.BookingId != bookingId
                                                    && startTime >= q.Start.TimeOfDay
                                                    && endTime <= q.End.AddMinutes((double)(q.ExtendedDuration ?? 0)).TimeOfDay
                                                ).ToList();
                    var roomTime = new RoomVMTimeData
                    {
                        Book = dataBookingByTime.Count(),
                        TimeArray = timeData.TimeOfDay,
                        RoomId = roomId,
                        Canceled = dataBookingByTime.Sum(b => b.IsCanceled),
                        Expired = dataBookingByTime.Sum(b => b.IsExpired),
                        EndEarly = dataBookingByTime.Sum(b => b.EndEarlyMeeting)
                    };

                    roomTimeData.Add(roomTime);
                }

                foreach (var timeData in roomTimeData)
                {
                    if (timeData.Canceled >= 1 || timeData.Expired >= 1 || timeData.EndEarly >= 1)
                    {
                        timeData.Book = 0;
                    }
                    if (timeData.Book >= 1)
                    {
                        timeData.Book = 1;
                    }
                }

                room.DataTime = roomTimeData;
                room.Setting = dataSettingGeneralMap;
            }

            ret.Collection = dataRoomMap;

            return ret;
        }

        public async Task<ReturnalModel> RescheduleBookingAsync(BookingVMRescheduleFR request)
        {
            ReturnalModel ret = new();

            var authUserNIK = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;

            var modules = await getModules();

            var dataBooking = await _bookingRepo.GetItemFilteredByBookingIdAsync(request.BookingId);

            if (dataBooking == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Data booking not exist";
                return ret;
            }

            // var dataInvitation = await _bookingInvitationRepo.GetAllFilteredByBookingIds(new string[] { request.BookingId });

            // var dataPic = await _bookingInvitationRepo.GetPicFilteredByBookingId(request.BookingId);

            var dataSettingGeneral = await _settingRuleBookingRepo.GetSettingRuleBookingTopOne();

            var now = DateTime.Now;

            if (dataSettingGeneral?.LimitTimeBooking != null && dataSettingGeneral.LimitTimeBooking > 0)
            {
                var limitDuration = dataSettingGeneral.LimitTimeBooking;
                var startTime = new DateTimeOffset(request.Start.GetValueOrDefault()).ToUnixTimeSeconds();
                var endTime = new DateTimeOffset(request.End.GetValueOrDefault()).ToUnixTimeSeconds();
                var extendedDuration = dataBooking?.ExtendedDuration ?? 0;
                var duration = TimeSpan.FromSeconds(endTime - startTime).TotalMinutes + extendedDuration;

                if (duration > limitDuration)
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = $"Maximum meeting duration is {limitDuration} minutes";
                    return ret;
                }
            }

            // TODO: Is Merge Condition
            // gak tau flownya gimana? karna saay buat reserve room tidak ada merge room

            if (string.IsNullOrEmpty(dataBooking?.RoomId))
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Room ID not found";
                return ret;
            }

            // var getRoom = await _roomRepo.GetAllRoomWithRadidsItemAsycn(new string[] { roomId });

            // if(getRoom == null) 
            // {
            //     ret.Status = ReturnalType.Failed;
            //     ret.Message = "Room not found";
            //     return ret;
            // }

            // Console.WriteLine("---------dataRoom---------");
            // var dataRoom = getRoom.GroupBy(q => q.Radid).Select(q => q.First()).FirstOrDefault();
            // Console.WriteLine(JsonSerializer.Serialize(dataRoom));

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    if (modules.ContainsKey("pantry") && modules["pantry"]?.IsEnabled == 1)
                    {
                        var setPantryConfig = await _settingPantryConfigRepository.GetSettingPantryConfigTopOne();

                        var pantryBeforeOrderMeeting = setPantryConfig?.BeforeOrderMeeting ?? 0;

                        var pantryTransaksiList = await _pantryTransaksiRepo.GetAllItemFilteredByEntity(new PantryTransaksi { BookingId = dataBooking!.BookingId, Via = "booking", OrderSt = 0 });

                        if (pantryTransaksiList.Count() > 0)
                        {
                            var pantryTransaksi = pantryTransaksiList.First();

                            if (dataBooking.Date != request.Date)
                            {
                                var qDate = dataBooking.Date;
                                var pantryId = pantryTransaksi.PantryId;
                                // var noOrderPantry = await _pantryTransaksiRepo.GetPantryOrderNumberAsync(pantryId, qDate);
                                // pantryOrder.OrderNo = noOrderPantry;
                                pantryTransaksi.OrderNo = "ORD-" + _Random.AlphabetNumeric(4) + _Random.Numeric(3);
                            }
                            var pantryTransaksiStatus = await _pantryTransaksiStatusRepository.GetAllPantryTransaksiStatus(0);

                            DateTime startBooking = request.Start == null ? dataBooking.Start : request.Start.GetValueOrDefault();
                            pantryTransaksi.OrderDatetime = startBooking;
                            int bTime = -pantryBeforeOrderMeeting;
                            // DateTime tanggaltimeOrderPantryBefore = DateTime.ParseExact(startBooking.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).AddMinutes(bTime);
                            pantryTransaksi.OrderDatetimeBefore = startBooking.AddMinutes(bTime);
                            pantryTransaksi.Via = "booking";
                            pantryTransaksi.OrderSt = (int)(pantryTransaksiStatus?.Id ?? 0);
                            pantryTransaksi.OrderStName = pantryTransaksiStatus?.Name ?? "";
                            pantryTransaksi.UpdatedAt = now;
                            pantryTransaksi.UpdatedBy = authUserNIK ?? "";

                            await _pantryTransaksiRepo.UpdateAsync(pantryTransaksi);
                        }
                    }

                    dataBooking!.Date = request.Date ?? dataBooking.Date;
                    dataBooking!.Start = request.Start ?? dataBooking.Start;
                    dataBooking!.End = request.End ?? dataBooking.End;

                    var extendedDuration = dataBooking.ExtendedDuration ?? 0;
                    var duration = (dataBooking.End - dataBooking.Start).TotalMinutes + extendedDuration;
                    var fHour = dataSettingGeneral?.Duration ?? 0;
                    long? cost = 0;
                    double allDuration = 0;
                    double getHoursMeeting = 0;
                    double checkHours = 0;
                    double reservationCost = 0;
                    if (modules.ContainsKey("price") && modules["price"]?.IsEnabled == 1)
                    {
                        cost = dataBooking.RoomPrice; // per hour
                        allDuration = extendedDuration + duration;
                        getHoursMeeting = Math.Floor(allDuration / fHour);
                        checkHours = allDuration % fHour;
                        if (checkHours > 0)
                        {
                            getHoursMeeting += 1;
                        }

                        reservationCost = (cost.HasValue ? (double)cost.Value : 0) * getHoursMeeting;
                    }

                    dataBooking.ServerDate = dataBooking.Date.ToDateTime(TimeOnly.MinValue);
                    dataBooking.ServerStart = dataBooking.Start;
                    dataBooking.ServerEnd = dataBooking.End;

                    // TODO: Is Merge Condition
                    // gak tau flownya gimana? karna saay buat reserve room tidak ada merge room

                    // check room is available with the selected date and time
                    Booking bookingExistFilter = new Booking
                    {
                        BookingId = dataBooking.BookingId,
                        RoomId = dataBooking.RoomId,
                        Date = dataBooking.Date,
                        Start = dataBooking.Start,
                        End = dataBooking.End,
                    };
                    var bookingExist = await _bookingRepo.GetAllAvailableItemAsync(bookingExistFilter);

                    if (bookingExist.Any())
                    {
                        ret.Status = ReturnalType.Failed;
                        ret.Message = "The schedule have been created by other";
                        return ret;
                    }

                    // TODO: Update Booking Invoice Flow
                    // gak tau flownya gimana? karna saay buat reserve room tidak ada insert booking invoice
                    /* if (
                        modules.ContainsKey("invoice") 
                        && modules["invoice"]?.IsEnabled == 1
                        && modules.ContainsKey("price") 
                        && modules["price"]?.IsEnabled == 1
                    )
                    {
                        
                    } */

                    dataBooking.BookingDevices = "web";
                    dataBooking.DurationPerMeeting = fHour;
                    dataBooking.CostTotalBooking = (int)reservationCost;
                    dataBooking.TotalDuration = (int)duration;
                    dataBooking.IsExpired = 0;
                    dataBooking.IsCanceled = 0;
                    dataBooking.IsRescheduled = 1;
                    dataBooking.RescheduledBy = authUserNIK ?? "";
                    dataBooking.RescheduledAt = now;
                    dataBooking.UpdatedAt = now;
                    dataBooking.UpdatedBy = authUserNIK ?? "";
                    dataBooking.IsDeleted = 0;

                    await _bookingRepo.UpdateAsync(dataBooking);

                    // TODO: send mail & notif stage

                    scope.Complete();

                    ret.Collection = dataBooking;

                    return ret;
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public async Task<ReturnalModel> CancelBookingAsync(BookingVMCancelFR request)
        {
            ReturnalModel ret = new();

            var authUserNIK = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;

            var now = DateTime.Now;

            var modules = await getModules();

            var dataBooking = await _bookingRepo.GetItemFilteredByBookingIdAsync(request.BookingId);

            if (dataBooking == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Data booking not exist";
                return ret;
            }

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    if (modules.ContainsKey("pantry") && modules["pantry"]?.IsEnabled == 1)
                    {
                        var setPantryConfig = await _settingPantryConfigRepository.GetSettingPantryConfigTopOne();

                        var pantryBeforeOrderMeeting = setPantryConfig?.BeforeOrderMeeting ?? 0;

                        var pantryTransaksiList = await _pantryTransaksiRepo.GetAllItemFilteredByEntity(new PantryTransaksi { BookingId = dataBooking!.BookingId, Via = "booking", OrderSt = 0 });

                        if (pantryTransaksiList.Count() > 0)
                        {
                            var pantryTransaksi = pantryTransaksiList.First();

                            var pantryTransaksiStatus = await _pantryTransaksiStatusRepository.GetAllPantryTransaksiStatus(4);


                            pantryTransaksi.Via = "booking";
                            pantryTransaksi.OrderSt = (int)(pantryTransaksiStatus?.Id ?? 0);
                            pantryTransaksi.OrderStName = pantryTransaksiStatus?.Name ?? "";
                            pantryTransaksi.IsCanceled = 1;
                            pantryTransaksi.CanceledAt = now;
                            pantryTransaksi.CanceledBy = authUserNIK ?? "";

                            await _pantryTransaksiRepo.UpdateAsync(pantryTransaksi);
                        }
                    }

                    dataBooking.BookingDevices = "web";
                    dataBooking.IsExpired = 0;
                    dataBooking.IsCanceled = 1;
                    dataBooking.IsRescheduled = 0;
                    dataBooking.CanceledBy = authUserNIK ?? "";
                    dataBooking.CanceledAt = now;
                    dataBooking.CanceledNote = request.Reason ?? "";
                    dataBooking.UpdatedAt = now;
                    dataBooking.UpdatedBy = authUserNIK ?? "";
                    dataBooking.IsDeleted = 0;

                    await _bookingRepo.UpdateAsync(dataBooking);

                    // TODO: Insert Notif
                    // gak tau flownya gimana? karna saay buat reserve room tidak ada insert notif

                    // TODO: Send Mail
                    // gak tau flownya gimana? karna saay buat reserve room tidak ada fungsi send mail

                    scope.Complete();

                    return ret;
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public async Task<ReturnalModel> EndMeetingAsync(BookingVMEndMeetingFR request)
        {
            ReturnalModel ret = new();

            var authUserNIK = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;

            var authUsername = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

            var now = DateTime.Now;

            var dataBooking = await _bookingRepo.GetItemFilteredByBookingIdAsync(request.BookingId);

            if (dataBooking == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Data booking not exist";
                return ret;
            }

            // TODO: Insert Notif
            // gak tau flownya gimana? karna saay buat reserve room tidak ada insert notif

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    dataBooking.EndEarlyMeeting = 1;
                    dataBooking.UpdatedAt = now;
                    dataBooking.UpdatedBy = authUserNIK ?? "";
                    dataBooking.IsAlive = 4;
                    dataBooking.EarlyEndedBy = authUserNIK ?? "";
                    dataBooking.EarlyEndedAt = now;
                    dataBooking.TextEarly = request.User ? authUsername : "By Admin";
                    dataBooking.IsDeleted = 0;
                    dataBooking.IsExpired = 0;
                    dataBooking.IsCanceled = 0;
                    dataBooking.IsRescheduled = 0;

                    await _bookingRepo.UpdateAsync(dataBooking);

                    scope.Complete();

                    return ret;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<ReturnalModel> CheckExtendMeetingTimeAsync(BookingVMCheckExtendMeetingFR request)
        {
            ReturnalModel ret = new();

            var dataSettingGeneral = await _settingRuleBookingRepo.GetSettingRuleBookingTopOne();

            if (dataSettingGeneral?.ExtendMeeting != 1)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Extend Time feature is disabled, please enable for use this feature.";
                return ret;
            }

            var max = dataSettingGeneral?.ExtendMeetingMax ?? 0;
            var pieceTime = dataSettingGeneral?.ExtendCountTime ?? 0;

            if (max < pieceTime)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Extend time not available.";
                return ret;
            }

            var dataBooking = await _bookingRepo.GetItemFilteredByBookingIdAsync(request.BookingId);

            if (dataBooking == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Data booking not exist";
                return ret;
            }

            var workEnd = _String.ToTimeOnly(dataBooking.RoomWorkEnd ?? "00:00:00");
            var extend = dataBooking.ExtendedDuration ?? 0;
            var end = dataBooking.End;
            var endWithExtend = end.AddMinutes((double)extend!);

            var bookingsByRoomIdAndDate = await _bookingRepo.GetBookingsByRoomIdsAndDateAsync(new string[] { dataBooking.RoomId! }, request.Date, true);

            var collectCheck = new List<int>();
            var bookingDurations = new List<BookingVMDuration>();

            for (int x = pieceTime; x <= max; x += pieceTime)
            {
                var timeroom = _DateTime.Combine(request.Date, workEnd);
                // var checknow = request.Date.Add(endWithExtend);
                var aftersumnow = endWithExtend.AddMinutes(x);

                // if (aftersumnow > timeroom)
                if (aftersumnow < timeroom)
                {
                    collectCheck.Add(x);
                }

                var endtimeExtent = aftersumnow;

                var bookingByDuration = bookingsByRoomIdAndDate
                                            .Where(q => endtimeExtent.TimeOfDay >= q.Start.TimeOfDay && endtimeExtent.TimeOfDay <= q.End.AddMinutes(q.ExtendedDuration ?? 0).TimeOfDay)
                                            .ToList();

                bookingDurations.Add(new BookingVMDuration
                {
                    Duration = x,
                    Book = bookingByDuration.Count,
                    TimeData = endtimeExtent.TimeOfDay
                });
            }

            bookingDurations = bookingDurations
            .Select(bd =>
            {
                // if (collectCheck.Contains(bd.Duration))
                if (!collectCheck.Contains(bd.Duration))
                {
                    bd.Book = 1;
                }
                return bd;
            })
            .Where(bd => bd.Book != 1)
            .ToList();

            if (bookingDurations.Count() < 1)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Extend time not available.";
                return ret;
            }

            ret.Collection = bookingDurations;

            return ret;
        }

        public async Task<ReturnalModel> SetExtendMeetingAsync(BookingVMExtendMeetingFR request)
        {
            ReturnalModel ret = new();

            var authUserNIK = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;

            var now = DateTime.Now;

            var dataBooking = await _bookingRepo.GetItemFilteredByBookingIdAsync(request.BookingId);

            if (dataBooking == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Data booking not exist";
                return ret;
            }

            var dataSettingGeneral = await _settingRuleBookingRepo.GetSettingRuleBookingTopOne();

            var fHour = dataSettingGeneral?.Duration ?? 0;
            var allDuration = dataBooking.ExtendedDuration + request.Extend;
            var getHoursMeeting = Math.Floor((double)allDuration! / fHour);
            var checkHours = allDuration % fHour;
            if (checkHours > 0)
            {
                getHoursMeeting += 1;
            }

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var reservationCost = dataBooking.RoomPrice + getHoursMeeting;
                    dataBooking.ExtendedDuration += request.Extend;
                    dataBooking.CostTotalBooking = (int)reservationCost!;
                    dataBooking.UpdatedAt = now;
                    dataBooking.UpdatedBy = authUserNIK ?? "";
                    dataBooking.IsDeleted = 0;

                    await _bookingRepo.UpdateAsync(dataBooking);

                    // TODO: Insert Notif
                    // gak tau flownya gimana? karna saay buat reserve room tidak ada insert notif

                    scope.Complete();

                    return ret;
                }
                catch (Exception)
                {
                    throw;
                }
            }

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
    
        private async Task<IEnumerable<SettingInvoiceText>?> getStatusInvoiceName()
        {
            var (list, _) = await _settingInvoiceTextRepository.GetAllSettingInvoiceTextsAsync();
            return list;
        }
    
        private async Task<RoomViewModelAlt?> CheckAvailableRoom(
            string roomId, 
            DateOnly bookDate, 
            DateTime bookStart, 
            DateTime bookEnd)
        {
            // check room is available with the selected date and time
            Booking cBook = new Booking
            {
                RoomId = roomId,
                Date = bookDate,
                Start = bookStart,
                End = bookEnd,
            };
            var bookingExist = await _bookingRepo.GetAllAvailableItemAsync(cBook);

            if (bookingExist.Any())
            {
                return null;
            }
            // .check room is available with the selected date and time

            // check room with the selected date and time
            Room cRoom = new Room
            {
                Radid = roomId,
                WorkDay = new List<string> { _String.ToDayName(bookDate.ToString("yyyy-MM-dd")) },
                WorkStart = bookStart.ToString("HH:mm:ss") ?? "00:00:00",
                WorkEnd = bookEnd.ToString("HH:mm:ss") ?? "23:59:00",
            };
            var roomExist = await _roomRepo.GetAllRoomAvailableAsync(cRoom);

            if (!roomExist.Any())
            {
                return null;
            }

            var room = _mapper.Map<RoomViewModelAlt>(roomExist.First());
            // .check room with the selected date and time

            return room;
        }

        private string generatePantryOrderNumber(long pantryId, DateTime date)
        {
            // var orderNo = "ORD-" + _Random.AlphabetNumeric(4) + _Random.Numeric(3);
            var orderNo = "0001";
            try
            {
                var maxOrderNo = _pantryTransaksiRepo.GetMaxOrderNo(date, pantryId);
                if (!string.IsNullOrEmpty(maxOrderNo))
                {
                    int oldNoOrderPantry = int.Parse(maxOrderNo);
                    int noSortOrderPantry = oldNoOrderPantry + 1;
                    orderNo = noSortOrderPantry.ToString("D4");
                }
                return orderNo;
            }
            catch (Exception)
            {
                return orderNo;
            }
        }

        private async Task<string> generateBookingOrderNumber(string year)
        {
            var orderNo = "001";
            try
            {
                var maxOrderNo = (await _bookingRepo.GetMaxOrderNumberAsync(year))?.NoOrder;
                if (!string.IsNullOrEmpty(maxOrderNo))
                {
                    // Extract the first part of the order number before the first '/'
                    var orderNumberPart = maxOrderNo.Split('/')[0];
                    int oldNoOrderPantry = int.Parse(orderNumberPart);
                    int noSortOrderPantry = oldNoOrderPantry + 1;
                    orderNo = noSortOrderPantry.ToString("D3");
                }
                return orderNo;
            }
            catch (Exception)
            {
                return orderNo;
            }
        }

        private async Task<Booking> checkMeetingVipAndApprovalAccess(Booking booking, RoomViewModelAlt room, Dictionary<string, ModuleBackend?>? modules)
        {
            var authUserId = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            booking.VipApproveBypass = 0;
            booking.VipLimitCapBypass = 0;
            booking.VipLockRoom = 0;
            booking.VipUser = "";
            booking.IsVip = 0;

            Employee? authUser = null;
            if (authUserId != null)
            {
                authUser = await _employeeRepo.GetItemByIdAsync(authUserId);
                if (authUser != null)
                {
                    var vipEnabled = modules?.GetValueOrDefault("vip")?.IsEnabled == 1;
                    var roomAdvEnabled = modules?.GetValueOrDefault("room_adv")?.IsEnabled == 1;

                    if (vipEnabled && roomAdvEnabled)
                    {
                        booking.VipApproveBypass = authUser.VipApproveBypass ?? 0;
                        booking.VipLimitCapBypass = authUser.VipLimitCapBypass ?? 0;
                        booking.VipLockRoom = authUser.VipLockRoom ?? 0;
                        booking.VipUser = authUser.Nik;
                        booking.IsVip = authUser.IsVip ?? 0;
                    }
                }
            }

            booking.IsApprove = 0;
            booking.UserApproval = "";
            if ((booking.IsVip == 1 && booking.VipApproveBypass == 1) || room.IsEnableApproval == 0)
            {
                booking.IsApprove = 1;
                booking.UserApproval = authUser?.Nik ?? "";
            }

            // if (room.IsConfigSettingEnable == 0 || room.IsEnableApproval == 0)
            // {
            //     booking.IsApprove = 3;
            //     booking.UserApproval = "";
            //     return booking;
            // }

            // if (booking.IsVip == 0 || booking.VipApproveBypass == 0)
            // {
            //     booking.IsApprove = 0;
            //     booking.IsAlive = booking.IsAlive == 0 ? 0 : 1;
            //     booking.UserApproval = "";
            //     return booking;
            // }

            // booking.IsApprove = 1;
            // booking.UserApproval = booking.VipUser;
            return booking;
        }
    }
}