using System.Net;
using System.Runtime.InteropServices;
using System.Security.Claims;
using _2.BusinessLogic.Services.Interface;
using _4.Helpers.Consumer;
using _4.Helpers.Consumer.Report;
using _5.Helpers.Consumer;
using _5.Helpers.Consumer._QRCodeGenerator;
using _5.Helpers.Consumer.Custom;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace _3.BusinessLogic.Services.Implementation;

public class APIMainDisplayService(
    IMapper _mapper, 
    IConfiguration _config, 
    IHttpContextAccessor context,
    APICaller _apiCaller,
    AccessControlRepository _accessControlRepository,
    BookingRepository _repo,
    BookingInvitationRepository _repoBI,
    EmployeeRepository _employeeRepository,
    PantryDetailRepository _repoPD,
    RoomRepository _repoRoom,
    SettingRuleBookingRepository _repoSettingRuleBooking,
    ModuleBackendRepository _repoModuleBackend,
    UserRepository _repoUser,
    RoomDisplayRepository _repoRoomDisplayRepository,
    PantryTransaksiRepository _pantryTransaksiRepository,
    BookingInvoiceRepository _bookingInvoiceRepository,
    SendingEmailRepository _sendingEmailRepository,
    SendingNotifRepository _sendingNotifRepository,
    SettingLogConfigRepository _settingLogConfigRepository,
    IS3Service _s3Service,
    IPantryTransaksiService _pantryTransaksiService,
    IAttachmentListService _attachmentListService,
    IExportReport _exp
    )
    : IAPIMainDisplayService
{


    public async Task<ReturnalModel> DisplayGetRoomMerge(ListBookingByDateFR request)
    {
        var ret = new ReturnalModel();

        // Validate request object
        if (request == null || string.IsNullOrWhiteSpace(request.Date) || request.RadId == null)
        {
            ret.StatusCode = (int)HttpStatusCode.BadRequest;
            ret.Status = ReturnalType.Failed;
            ret.Message = "Invalid request: RadId and Date are required.";
            return ret;
        }

        // Validate the date format
        if (!DateTime.TryParseExact(request.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            ret.StatusCode = (int)HttpStatusCode.BadRequest;
            ret.Status = ReturnalType.Failed;
            ret.Message = "Invalid date format. Expected format: yyyy-MM-dd";
            return ret;
        }

        // Fetch room details
        var getRoom = await _repoRoom.GetByRadId(request.RadId);
        if (getRoom == null)
        {
            ret.StatusCode = (int)HttpStatusCode.NotFound;
            ret.Status = ReturnalType.Failed;
            ret.Message = "Room not found.";
            return ret;
        }

        // Fetch bookings
        var booking = await _repo.GetAllFilteredByDate(request.Date, getRoom.Radid) ?? new List<Booking>();

        if (booking.Any())
        {
            ret.Message = "Success get data to LIS";
            ret.Collection = _mapper.Map<List<BookingViewModel>>(booking);
        }
        else
        {
            ret.Status = ReturnalType.Failed;
            ret.StatusCode = (int)HttpStatusCode.NotFound;
            ret.Message = "Data not found";
        }

        return ret;
    }

    public async Task<ReturnalModel> DisplayScheduleMergeTimeBooked(ListBookingByDateFR request)
    {
        var ret = new ReturnalModel();

        // Validate request
        if (request == null || string.IsNullOrWhiteSpace(request.Date) || string.IsNullOrWhiteSpace(request.RadId))
        {
            ret.Status = ReturnalType.Failed;
            ret.StatusCode = (int)HttpStatusCode.BadRequest;
            ret.Message = "Invalid request: RoomId and Date are required.";
            return ret;
        }

        // Get booking settings (use default values if null)
        var settingRuleBooking = await _repoSettingRuleBooking.GetFirstSettingAsync();
        var pieceTime = settingRuleBooking?.Duration - 0 ?? 0;
        var maxDisplayDuration = settingRuleBooking?.MaxDisplayDuration - 0 ?? 0;

        // Adjust interval if needed
        if (pieceTime == 15) pieceTime = 30;

        // Fetch room details
        var getRoom = await _repoRoom.GetByRadId(request.RadId);
        if (getRoom == null)
        {
            ret.Status = ReturnalType.Failed;
            ret.StatusCode = (int)HttpStatusCode.NotFound;
            ret.Message = "Room not found.";
            return ret;
        }

        // Get room booking data
        var dataRoom = await _repoModuleBackend.GetDataMergeRoomBookingAsync(getRoom.Radid!);
        if (!dataRoom.Any())
        {
            ret.Status = ReturnalType.Failed;
            ret.StatusCode = (int)HttpStatusCode.NotFound;
            ret.Message = "Booked Time feature is disabled, please enable for use this feature.";
            return ret;
        }

        var startTime = DateTime.ParseExact($"{request.Date} 00:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

        // Fetch booked times for each room concurrently
        var bookingTasks = dataRoom
            .Select(async room =>
            {
                room.BookedTimes = await _repoModuleBackend.GetBookedTimesAsync(room.Radid!, startTime, pieceTime, maxDisplayDuration);
                return room.BookedTimes; // Ensure returning booked times
            })
            .ToList();

        // Await all tasks and flatten the results
        var bookedTimesList = (await Task.WhenAll(bookingTasks))
            .SelectMany(bt => bt) // Flatten List<List<TimeBookingDTO>> into List<TimeBookingDTO>
            .ToList();

        ret.Message = "Booked time available.";
        ret.Collection = _mapper.Map<List<BookedTimeViewModel>>(bookedTimesList);
        return ret;
    }

    public async Task<ReturnalModel> DisplayScheduleTimeFastBooked(ListBookingByDateNikFRViewModel request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Date) || string.IsNullOrWhiteSpace(request.RadId))
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Invalid request: RadId and Date are required."
            };
        }

        var settingRuleBooking = await _repoSettingRuleBooking.GetFirstSettingAsync();
        var pieceTime = settingRuleBooking?.Duration ?? 30;
        var maxDisplayDuration = settingRuleBooking?.MaxDisplayDuration ?? 0;

        if (pieceTime == 15) pieceTime = 30;

        var rooms = (await _repoRoom.GetByDataRoomByRadId(request.RadId)).ToList();
        if (!rooms.Any())
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = "Booked Time feature is disabled, please enable for use this feature."
            };
        }

        var firstRoom = rooms.First();
        if (firstRoom.TypeRoom == "merge")
        {
            var mergedRooms = (await _repoModuleBackend.GetDataMergeRoomBookingAsync(firstRoom.Radid!)).ToList();
            if (mergedRooms.Any())
            {
                rooms = mergedRooms.Select(m => new RoomDetailDto
                {
                    Id = m.Room?.Id,
                    Radid = m.Room?.Radid,
                    Name = m.Room?.Name,
                    Description = m.Room?.Description,
                    Capacity = m.Room?.Capacity,
                    GoogleMap = m.Room?.GoogleMap,
                    RaName = m.RaName,
                    RaId = m.RaId
                }).ToList();
            }
        }

        var startTime = DateTime.SpecifyKind(DateTime.Parse($"{request.Date} 00:00:00"), DateTimeKind.Utc);

        var roomBookings = new List<RoomBookingDTO>();

        foreach (var room in rooms)
        {
            var roomId = room.Radid;
            var timeSlots = new List<TimeBookingDTO>();
            int durationCounter = 0;

            for (int x = 0; x <= maxDisplayDuration; x += pieceTime)
            {
                durationCounter += pieceTime;
                var timeData = startTime.AddMinutes(x);

                var bookedTimes = await _repoModuleBackend.GetBookedTimesAsync(roomId!, startTime, pieceTime, maxDisplayDuration);

                timeSlots.Add(new TimeBookingDTO
                {
                    TimeArray = timeData.ToString("HH:mm:ss"),
                    RoomId = room.Id,
                    RadId = room.Radid,
                    BookedCount = bookedTimes.Count,
                    Canceled = bookedTimes.Sum(t => t.Canceled),
                    Expired = bookedTimes.Sum(t => t.Expired),
                    EndEarly = bookedTimes.Sum(t => t.EndEarly),

                    Duration = durationCounter
                });
            }

            var roomBooking = new RoomBookingDTO
            {
                RoomId = room.Id,
                Radid = room.Radid,
                TypeRoom = room.TypeRoom,
                DataTime = timeSlots
            };

            var mergeRooms = await _repoModuleBackend.GetDataMergeRoomBookingAsync(room.Radid!);
            if (room.TypeRoom == "merge" && mergeRooms != null && mergeRooms.Any())
            {
                roomBooking.MergeRoom = mergeRooms.ToList();
            }

            roomBookings.Add(roomBooking);
        }

        if (!roomBookings.Any())
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = "Booked Time feature is disabled, please enable this feature."
            };
        }

        var firstRoomData = roomBookings.First();
        var filteredTimeSlots = firstRoomData.DataTime.Take(4).ToList();


        return new ReturnalModel
        {
            Status = ReturnalType.Success,
            Message = "Booked time available.",
            Collection = new TimeBookingListViewModel
            {
                RadId = firstRoomData.Radid,
                TypeRoom = firstRoomData.TypeRoom,
                Datetime = _mapper.Map<List<TimeBookingDTOViewModel>>(filteredTimeSlots)
            }
        };
    }

    public async Task<ReturnalModel> GetDisplayRoomList()
    {
        var ret = new ReturnalModel();

        try
        {
            var rooms = await _repoRoom.GetAllAsync();
            var orderedRooms = rooms.OrderBy(r => r.Name).ToList();
            ret.Collection = _mapper.Map<List<RoomViewModel>>(orderedRooms);
            ret.Status = ReturnalType.Success;
            ret.Message = "Rooms retrieved successfully.";
        }
        catch (Exception ex)
        {
            ret.Status = ReturnalType.Failed;
            ret.StatusCode = (int)HttpStatusCode.InternalServerError;
            ret.Message = $"An error occurred while retrieving rooms: {ex.Message}";
        }

        return ret;
    }

    public async Task<ReturnalModel> GetDisplayRoomMergeList(ListRoomMergeFRViewModel request)
    {
        var ret = new ReturnalModel();

        if (request == null || string.IsNullOrWhiteSpace(request.RadId))
        {
            ret.Status = ReturnalType.Failed;
            ret.StatusCode = (int)HttpStatusCode.BadRequest;
            ret.Message = "Invalid request: RadId is required.";
            return ret;
        }

        try
        {
            var rooms = await _repoRoom.GetMergeRoomList(request.RadId);
            if (rooms == null || !rooms.Any())
            {
                ret.Status = ReturnalType.Failed;
                ret.StatusCode = (int)HttpStatusCode.NotFound;
                ret.Message = "No merge rooms found.";
                return ret;
            }

            ret.Collection = _mapper.Map<List<RoomViewModel>>(rooms);
            ret.Status = ReturnalType.Success;
            ret.Message = "Success get data to merge room.";
        }
        catch (Exception ex)
        {
            ret.Status = ReturnalType.Failed;
            ret.StatusCode = (int)HttpStatusCode.InternalServerError;
            ret.Message = $"An error occurred while retrieving rooms: {ex.Message}";
        }

        return ret;
    }

    public async Task<ReturnalModel> GetDisplayScheduleOccupiedList(ListScheduleOccupiedFRViewModel request)
    {
        var ret = new ReturnalModel();

        try
        {
            string timezone = request.Timezone ?? _config?["OTHER_SETTING:DefaultTimeZone"] ?? "Asia/Jakarta";

            string dateTimeString = $"{request.Date} {request.Time}";

            DateTime localDateTime = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            DateTime serverDateTime = localDateTime;

            var getData = await _repo.GetMeetingOccupiedByDisplay(request.RadId!, serverDateTime);

            var bookings = _mapper.Map<List<BookingViewModel>>(getData);

            if (getData != null && getData.Count > 0)
            {
                ret.Status = ReturnalType.Success;
                ret.Collection = bookings;
                ret.Message = "Success get data to active";
            }
            else
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "No occupied schedules found.";
            }
        }
        catch (Exception ex)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = $"Error: {ex.Message}";
        }

        return ret;
    }


    public async Task<ReturnalModel> GetDisplayBySerial(ListDisplaySerialFRViewModel request)
    {
        // Validate request
        if (string.IsNullOrEmpty(request.Serial))
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Failed restrict access"
            };
        }

        // Fetch display serial data
        var displaySerialData = await _repoRoomDisplayRepository.GetDisplaySerialBySerialNumber(request.Serial!);
        if (displaySerialData?.DisplaySerial == null)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = "Display not available/registered"
            };
        }

        // Manually map to avoid AutoMapper issues
        var displaySerial = new RoomDisplayBySerialViewModel
        {
            Id = displaySerialData.Id,
            DisplaySerial = displaySerialData.DisplaySerial,
            RoomSelect = displaySerialData.RoomSelect
        };

        // Fetch room data if RoomSelect is not null/empty
        var roomIds = displaySerial.RoomSelect?
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .ToArray();

        if (roomIds != null && roomIds.Length > 0)
        {
            var fetchRoomData = await _repoRoom.GetDataRoomDisplayByListID(roomIds);
            displaySerial.RoomSelectData = _mapper.Map<List<RoomViewModel>>(fetchRoomData);
        }
        else
        {
            displaySerial.RoomSelectData = new List<RoomViewModel>(); // Ensure it's never null
        }

        return new ReturnalModel
        {
            Status = ReturnalType.Success,
            Collection = displaySerial,
            Message = "Success get data to active"
        };
    }



    public async Task<ReturnalModel> DisplayMeetingWithMoreRoomListDisplay(ListDisplayMeetingScheduleTodayFRViewModel request)
    {
        var meetingData = await DisplayMeetingWithMoreRoom(request);

        if (meetingData.Status != ReturnalType.Success)
        {
            return meetingData;
        }

        //Explicit cast to anonymous type
        var collection = (meetingData.Collection as dynamic)!;
        var meetingsData = await _repo.GetMeetingListByDisplay(collection.ServerDateTime, collection.RoomSelect);

        var result = _mapper.Map<List<BookingViewModel>>(meetingsData);
        return new ReturnalModel
        {
            Status = ReturnalType.Success,
            Collection = result,
            Message = "Success get data to list"
        };
    }

    public async Task<ReturnalModel> DisplayMeetingWithMoreRoomOccupiedListDisplay(ListDisplayMeetingScheduleTodayFRViewModel request)
    {
        var meetingData = await DisplayMeetingWithMoreRoom(request);

        if (meetingData.Status != ReturnalType.Success)
        {
            return meetingData;
        }

        //Explicit cast to anonymous type
        var collection = (meetingData.Collection as dynamic)!;
        var meetingsData = await _repo.GetMeetingListOccupiedByDisplay(collection.ServerDateTime, collection.RoomSelect);
        
        var result = _mapper.Map<List<BookingViewModel>>(meetingsData);
        return new ReturnalModel
        {
            Status = ReturnalType.Success,
            Collection = result,
            Message = "Success get data to list"
        };
    }

    private async Task<ReturnalModel> DisplayMeetingWithMoreRoom(ListDisplayMeetingScheduleTodayFRViewModel request)
    {

        // Validate display serial data
        var displaySerialData = await _repoRoomDisplayRepository.GetDisplaySerialBySerialNumber(request.Serial!);
        if (displaySerialData?.DisplaySerial == null)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = "Display not available/registered"
            };
        }
        if (displaySerialData.Enabled == 0)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = "Display not enabled"
            };
        }

        // // Set default timezone if not provided
        // string timezone = !string.IsNullOrEmpty(request.Timezone) ? request.Timezone
        //     : _config?["OTHER_SETTING:DefaultTimeZone"] ?? "Asia/Jakarta";

        // // Convert date and time to DateTime based on timezone
        // if (!DateTime.TryParse($"{request.Date} {request.Time}", out DateTime localDateTime))
        // {
        //     return new ReturnalModel
        //     {
        //         Status = ReturnalType.Failed,
        //         Message = "Invalid date/time format"
        //     };
        // }

        // try
        // {
        //     localDateTime = FastBook.ConvertTimezoneId(localDateTime, timezone);
        // }
        // catch (TimeZoneNotFoundException)
        // {
        //     return new ReturnalModel
        //     {
        //         Status = ReturnalType.Failed,
        //         Message = $"Invalid timezone: {timezone}"
        //     };
        // }

        // // Convert to default system timezone (APP_GMT)
        // string defaultTimeZone = _config?["OTHER_SETTING:DefaultTimeZone"] ?? "Asia/Jakarta";
        // DateTime serverDateTime = FastBook.ConvertTimezoneId(localDateTime, defaultTimeZone);

        // // Update request date and time
        // request.Date = serverDateTime.ToString("yyyy-MM-dd");
        // request.Time = serverDateTime.ToString("HH:mm:ss");
        if (!DateTime.TryParse($"{request.Date} {request.Time}", out DateTime localDateTime))
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = "Invalid date/time format"
            };
        }
        var serverDateTime = localDateTime;
        // Determine selected rooms
        var roomSelect = (!string.IsNullOrEmpty(request.Type) && (request.Type == EnumBookingTypeRoom.AllRoom || request.Type == EnumBookingTypeRoom.Receptionist))
            ? request.RoomSelect!.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            : !string.IsNullOrEmpty(request.RadId) ? new List<string> { request.RadId } : new List<string>();
        
        return new ReturnalModel
        {
            Collection = new MeetingDisplayCollection
            {
                ServerDateTime = serverDateTime,
                RoomSelect = roomSelect,
            }
        };
    }

    // jangan dihapus, belum selesai
    public async Task<ReturnalModel> DisplayScheduleFastBooked(BookingDisplayScheduleFastBookedFRViewModel viewModel)
    {

        // Get modules
        var modules_loker = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Loker);
        var modules_email = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Email);

        string tmpDir = _config["UploadFileSetting:attachmentFolder"] ?? string.Empty;

        var checkSerial = await CheckSerialIsAlready(viewModel.Serial);
        if (checkSerial.Status == ReturnalType.Failed)
        {
            return checkSerial;
        }


        if (viewModel.Timezone != null)
        {
            viewModel.Timezone = _config["OTHER_SETTING:DefaultTimeZone"] ?? "Asia/Jakarta";
        }


        var datetime = DateTime.Now;
        int timeDuration = viewModel.Duration;
        string roomId = viewModel.RadId;
        string randomId = _Random.Numeric(10).ToString();
        string id = $"DISP-{randomId}";
        string invoiceId = randomId;
        string nikPic = viewModel.Nik;

        // Get employee data
        var resPic = await _employeeRepository.GetNikEmployeeByPic(nikPic);
        var getDataPic = resPic;

        // --
        if (getDataPic == null || string.IsNullOrEmpty(getDataPic.Nik))
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = "Your username/NIK doesn't have access"
            };
        }
        // ---

        var alocation = new FastBookAlocationVMDefaultFR
        {
            Id = getDataPic.AlocationId,
            Name = getDataPic.AlocationName
        };

        short isMerge = viewModel.IsMerge ?? 0;
        var mergeRoom = viewModel.MergeRoom ?? new List<string>();
        var dataMergeRoomWidthJson = new List<RoomMergeViewModel>();
        // Get room data
        var roomData = await _repoRoom.GetByRadId(roomId);
        var room = _mapper.Map<FastBookRoomViewModel>(roomData);
        if (room == null)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = "Room not found"
            };
        }

        string roomName = room.Name;
        string mergeRoomName = room.Name;
        string mergeRoomId = room.Radid;
        List<RoomMergeViewModel> dataMergeRoom = new List<RoomMergeViewModel>();

        if (isMerge == 1)
        {
            List<string> tempMergeRoomNames = new List<string>();

            // Fetch all rooms in one go
            var mergedRooms = await _repoRoom.GetByRadIdsAsync(mergeRoom);

            // Process the data in memory
            foreach (var mergedRoom in mergedRooms)
            {
                tempMergeRoomNames.Add(mergedRoom.Name);
                dataMergeRoom.Add(new RoomMergeViewModel
                {
                    RadId = mergedRoom.Radid,
                    Name = mergedRoom.Name,
                    Location = mergedRoom.Location,
                    Link = mergedRoom.GoogleMap
                });
            }

            if (tempMergeRoomNames.Count > 0)
            {
                roomName += $" ({string.Join(", ", tempMergeRoomNames)})";
                dataMergeRoomWidthJson = dataMergeRoom;
            }
        }

        var dataSettingGeneral = await _repoSettingRuleBooking.GetFirstSettingAsync();
        // var dataEmailInternal = new List<EmployeeViewModel>();   
        var fHour = dataSettingGeneral?.Duration ?? 0;
        var time1 = DateTime.Now;
        if (!string.IsNullOrEmpty(viewModel.StartTime))
        {
            string dateTimeString = $"{viewModel.Date:yyyy-MM-dd} {viewModel.StartTime}";

            try
            {
                time1 = _String.ToDateTime(dateTimeString, "yyyy-MM-dd HH:mm:ss");
            }
            catch (FormatException)
            {
                return new ReturnalModel
                {
                    Title = ReturnalType.Failed,
                    Status = ReturnalType.Failed,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = $"Invalid date format: {dateTimeString}"
                };
            }
        }

        var time2 = time1.AddMinutes(timeDuration);
        var startStr = time1.ToString("yyyy-MM-dd HH:mm:ss");
        var endStr = time2.ToString("yyyy-MM-dd HH:mm:ss");

        var duration = timeDuration;
        var getHoursMeeting = duration / fHour;
        var checkHours = duration % fHour;
        if (checkHours > 0)
        {
            getHoursMeeting += 1;
        }
        var tanggalMeeting = viewModel.Date.ToString();
        var waktuMulai = time1;
        var waktuEnd = time2;
        var waktuAkhir = waktuEnd;
        var waktuTimeStart = startStr;
        var waktuTimeEnd = endStr;

        var serverDate1 = new DateTime(waktuMulai.Ticks, DateTimeKind.Utc);
        var serverStart1 = new DateTime(waktuMulai.Ticks, DateTimeKind.Utc);
        var serverEnd1 = new DateTime(waktuAkhir.Ticks, DateTimeKind.Utc);

        var ckServerDate = FastBook.ConvertTimezoneId(serverDate1, viewModel.Timezone!);
        var ckServerStart = FastBook.ConvertTimezoneId(serverStart1, viewModel.Timezone!).TimeOfDay;
        var ckServerEnd = FastBook.ConvertTimezoneId(serverEnd1, viewModel.Timezone!).TimeOfDay;

        var databook = new FastBookBookingViewModel
        {
            Title = viewModel.Title,
            Date = viewModel.Date,
            Start = time1,
            End = time2,
        };

        var ruangan = room.Radid;
        var reservationCost = room.Price * getHoursMeeting;
        // var internalBatch = new List<object>(); // Removed duplicate declaration

        if (isMerge == 1)
        {
            var tempMergeRoomName = new List<string>();
            foreach (var mv in mergeRoom)
            {
                var ruanganMId = mv;
                await _repo.ChecBookingConditionPerRoomAsync(ruanganMId, ckServerDate, ckServerStart, ckServerEnd);
            }
        }
        else
        {
            await _repo.ChecBookingConditionPerRoomAsync(ruangan, ckServerDate, ckServerStart, ckServerEnd);
        }

        var internalDataRow = viewModel.InternalData ?? new List<FastBookListlDataInternalFRViewModel>();
        var internalData = _mapper.Map<List<FastBookEmployeeViewModel>>(internalDataRow); ;
        var externalData = viewModel.ExternalData ?? new List<FastBookListlDataExternalFRViewModel>();
        var nikArray = internalData.Select(i => i.Nik).ToList();

        var rowInternalData = nikArray.Count > 0 ? (await _employeeRepository.GetNikEmployeeByPic(nikArray))?.Where(e => e != null).Cast<Employee>().ToList() ?? new List<Employee>() : new List<Employee>();

        var rowInternal = _mapper.Map<List<FastBookEmployeeViewModel>>(rowInternalData); ;
        // START MODULE PANTRY
        await _pantryTransaksiService.CreatePantryOrderAsync(databook, id);
        // END MODULE PANTRY

        // START MODULE INVOICE & PRICE
        var durationCalculate = FastBook.CalculateDuration(databook);
        var reservationCostCalculate = await CalculateReservationCost(databook, room);
        var formatInvoice = await CreateInvoiceOrderAsync(databook, alocation, room);
        // END MODULE INVOICE & PRICE

        var internalBatch = new List<FastBookBookingInvitationViewModel>();
        var eksternalBatch = new List<FastBookBookingInvitationViewModel>();
        // var dataEmailInternalData = rowInternal;
        var dataEmailInternal = _mapper.Map<List<FastBookEmployeeViewModel>>(rowInternal);
        var dataEmailEksternal = new List<FastBookBookingInvitationViewModel>();
        var dataEmailInternalArray = new List<object>();
        var data = new FastBookBookingViewModel();  // DATA BOOKING

        data = FastBook.MobileFormatBooking(databook);
        data.BookingId = id;
        data.NoOrder = formatInvoice;
        data.RoomId = room.Radid;
        data.TotalDuration = durationCalculate;
        data.DurationPerMeeting = fHour;
        data.CostTotalBooking = reservationCostCalculate;
        data.CreatedAt = DateTime.Now;
        data.CreatedBy = nikPic; // mobile created
        data.RoomName = roomName;
        data.AlocationId = alocation.Id;
        data.AlocationName = alocation.Name;
        data.Pic = getDataPic.Name;
        data.IsMerge = isMerge;
        data.MergeRoomName = mergeRoomName;
        data.MergeRoomId = mergeRoomId;
        data.MergeRoom = JsonConvert.SerializeObject(dataMergeRoomWidthJson);
        data.Timezone = viewModel.Timezone!;
        data.BookingDevices = "display";
        data.BookingType = "general";
        data.IsDeleted = 0;
        data.UpdatedAt = DateTime.Now;
        data.Note = viewModel.Note ?? "";

        var serverStart = DateTime.SpecifyKind(DateTime.ParseExact(viewModel.StartTime!, "HH:mm:ss", CultureInfo.InvariantCulture), DateTimeKind.Utc);
        var serverEnd = serverStart.AddMinutes(timeDuration);
        data.ServerStart = FastBook.ConvertTimezoneId(serverStart, viewModel.Timezone!);
        data.ServerEnd = FastBook.ConvertTimezoneId(serverEnd, viewModel.Timezone!);
        data.ServerDate = FastBook.ConvertTimezoneId(serverStart, viewModel.Timezone!);

        var modulesAdvance = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.RoomAdvance);
        var modulesVip = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.UserVIP);
        var resVIPFastBookEmployee = await _employeeRepository.GetByIdAsync(data.VipUser);

        var resVIPFastBook = _mapper.Map<FastBookEmployee>(resVIPFastBookEmployee);

        data = FastBook.AdjustAdvanceMeeting(data, room, modulesAdvance?.IsEnabled);
        data = FastBook.CheckMeetingVipAccess(data, room, resVIPFastBook, modulesAdvance?.IsEnabled, modulesVip?.IsEnabled);
        data = FastBook.CheckApprovalMeetingAccess(data, room, modulesVip?.IsEnabled);

        // Create PIC/Organizer
        var invitationPic = FastBook.CreateInvitationPic(data, nikPic, getDataPic);
        var qrInvitationPic = $"{id}_{invitationPic.PinRoom}";

        var qrCodeInvitationBase64 = _QRCodeGenerator.GenerateQRBase64(qrInvitationPic);
        _attachmentListService.SetTableFolder("qr");
        await _attachmentListService.ProcessBase64ToBlob(qrCodeInvitationBase64, $"{qrInvitationPic}.png");

        // START ATTENDEES AREA

        var listPinRoom = new List<string>();
        foreach (var val in internalData)
        {

            string numStrInternal = FastBook.GenerateRandomPin();
            var qrnvitationInternal = data.BookingId + "_" + numStrInternal;

            var qrCodeInternalBase64 = _QRCodeGenerator.GenerateQRBase64(qrnvitationInternal);
            await _attachmentListService.ProcessBase64ToBlob(qrCodeInternalBase64, $"{qrnvitationInternal}.png");
            listPinRoom.Add(numStrInternal);
        }

        var dataGenerateInternal = FastBook.CreateInternalBatch(data, listPinRoom, rowInternal, nikPic, true);
        internalBatch = dataGenerateInternal.InternalBatch;
        var newDataEmailInternal = _mapper.Map<List<FastBookEmployeeViewModel>>(dataGenerateInternal.DataEmailInternal);
        
        // insert of PIC invitation
        var ipicemail = new FastBookEmployeeViewModel
        {
            Nik = getDataPic.Nik,
            Name = getDataPic.Name,
            IsPic = 1,
            Email = getDataPic.Email,
            Pin = invitationPic.PinRoom
        };
        newDataEmailInternal.Add(ipicemail);

        var listPinRoomExternal = new List<string>();
        foreach (var val in externalData)
        {

            string pinRoomExternal = FastBook.GenerateRandomPin();
            var qrnvitationExternal = data.BookingId + "_" + pinRoomExternal;

            var qrCodeExternalBase64 = _QRCodeGenerator.GenerateQRBase64(qrnvitationExternal);
            await _attachmentListService.ProcessBase64ToBlob(qrCodeExternalBase64, $"{data.BookingId}_{pinRoomExternal}.png");
            listPinRoomExternal.Add(pinRoomExternal);
        }

        var dataGenerateExternal = FastBook.CreateExternalBatch(data, listPinRoomExternal, externalData, nikPic);
        eksternalBatch = dataGenerateExternal.eksternalBatch;
        var newDataEmailEksternal = dataGenerateExternal.dataEmailEksternal;

        var dataToSend = new
        {
            InternalEmails = newDataEmailInternal,
            ExternalEmails = newDataEmailEksternal
        };

        var batchSendingEmail = JsonConvert.SerializeObject(dataToSend);
        var batchSendingNotif = JsonConvert.SerializeObject(dataToSend.InternalEmails);

        var sending_email = CreateSendingBatchEmail(id, batchSendingEmail, DateTime.Now);
        var sending_notif = CreateSendingBatchNotif(id, batchSendingNotif, DateTime.Now);

        var fastBookinvitationPic = _mapper.Map<BookingInvitation>(invitationPic);
        await _repoBI.Create(fastBookinvitationPic);
        if (internalBatch.Count > 0)
        {
            var internalBatchData = _mapper.Map<List<BookingInvitation>>(internalBatch);
            await _repoBI.CreateBulk(internalBatchData);

        }
        if (eksternalBatch.Count > 0)
        {
            var externalBatchData = _mapper.Map<List<BookingInvitation>>(eksternalBatch);
            await _repoBI.CreateBulk(externalBatchData);

        }

        // =========================================================================
        // START 365
        // =========================================================================
        var moduleInt365 = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Int365);
        var moduleIntGoogle = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.IntGoogle);
        var room365 = room.ConfigMicrosoft ?? string.Empty;
        var roomGoogle = room.ConfigGoogle ?? string.Empty;

        if (moduleInt365.IsEnabled == 1 && data.IsAlive == 1 && !string.IsNullOrEmpty(room365))
        {
            var ms365 = await _repoModuleBackend.GetIntegration365Top();
            var ck365 = await _repoModuleBackend.GetIntegration365TopByStatus();
            if (ck365.Any())
            {
                var res365 = await CreateEvent365(data, room, ms365, newDataEmailInternal, dataEmailEksternal);
                try
                {
                    // var jres365 = JsonConvert.DeserializeObject<BookingViewModel>(res365.Collection);
                    if (res365 != null)
                    {
                        var collection = res365.Collection as dynamic;
                        data.BookingId365 = collection?.Id ?? string.Empty;
                    }
                    else
                    {
                        data.BookingId365 = string.Empty;
                    }
                }
                catch (Exception)
                {
                    data.BookingId365 = string.Empty;
                }
            }
        }

        if (moduleIntGoogle.IsEnabled == 1 && data.IsAlive == 1 && !string.IsNullOrEmpty(roomGoogle))
        {
            data.BookingIdGoogle = string.Empty;
        }
        // =========================================================================
        // END 365
        // =========================================================================
        // =========================================================================
        // CREATE BOOKING
        // =========================================================================
        if (isMerge == 0)
        {
            var dataBookingInsert = _mapper.Map<Booking>(data);
            await _repo.Create(dataBookingInsert);
        }
        else
        {
            foreach (var roomBooking in dataMergeRoom)
            {
                data.RoomId = roomBooking.RadId;
                var dataBookingInsert = _mapper.Map<Booking>(data);
                await _repo.Create(dataBookingInsert);
            }
        }

        // =========================================================================
        // END BOOKING
        // =========================================================================


        // =========================================================================
        // START NOTIFICATION MEETING
        // =========================================================================
        var sendingEmailData = _mapper.Map<SendingEmail>(sending_email);
        await _sendingEmailRepository.AddAsync(sendingEmailData);
        var sendingNotifData = _mapper.Map<SendingNotif>(sending_notif);
        await _sendingNotifRepository.AddAsync(sendingNotifData);

        var notifCollectData = new List<NotificationData>();
        notifCollectData = CreateNotifikasiCollectData(id, newDataEmailInternal, data, DateTime.Now);

        var typeNotif = 1; // notification_type 1=booking
        var notifInsert = false; // notification_type 1=booking

        var notifPic = new NotificationData
        {
            Datetime = DateTime.Now,
            Nik = nikPic,
            Type = typeNotif,
            Value = id,
            Title = "Create a meeting schedule",
            Body = $"{databook.Title} - {databook.Date}",
            IsSending = 0,
            IsDeleted = 0,
            CreatedAt = DateTime.Now
        };

        notifCollectData.Add(notifPic);

        await InsertNotifAdmin(12, "Create meeting", data.Title);
        await _repoModuleBackend.CreateBulk(notifCollectData);

        // START MANAGE NOTIF
        var sendNotif = true;
        var notifReminder = viewModel.Notif;
        if (notifReminder == 0)
        {
            sendNotif = false;
        }
        // END MANAGE NOTIF

        var bookingId = id;
        var getBooking = await _repo.GetDataBookingById(bookingId);
        if (getBooking == null)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Data not exist"
            };
        }

        var dataBooking = getBooking;
        dataBooking.FormatTimeStart = waktuTimeStart;
        dataBooking.FormatTimeEnd = waktuTimeEnd;
        dataBooking.FormatDate = FormatDate(tanggalMeeting);

        // MODULE EMAIL
        if (modules_email?.IsEnabled == 1  && sendNotif)
        {
            foreach (var people in dataToSend.InternalEmails)
            {
                await SendEmail("invitation", dataBooking, people, isExternal: false);
            }
            foreach (var people in dataToSend.ExternalEmails)
            {
                await SendEmail("invitation", dataBooking, people, isExternal: true);

            }
        }

        // MODULE NOTIFICATION
        var meetingTitle = databook.Title;
        var meetingDate = databook.Date.ToString();
        var meetingStart = databook.Start.ToString();
        var meetingEnd = databook.End.ToString();

        if (data.IsAlive == 1 && sendNotif)
        {
            var notificationTitle = $"Invitation Meeting of {meetingTitle}";
            var notificationBody = $"{FormatDate(meetingDate)} {FormatTime(meetingStart)}-{FormatTime(meetingEnd)} at {roomName}";
            await PushNotificationAsync(notificationTitle, notificationBody, newDataEmailInternal, typeNotif, notifInsert);
            await InsertNotifAdminApiAsync(12, "Create meeting", meetingTitle, ipicemail.Nik);
        }
 
        // MODULE LOCKER
        if (modules_loker?.IsEnabled == 1 && data.IsAlive == 1)
        {
            await BookingLockerForAttendeesAsync(id, rowInternal, data, DateTime.Now);
        }

        // Return success response
        return new ReturnalModel
        {
            Status = ReturnalType.Success,
            StatusCode = (int)HttpStatusCode.OK,
            Message = $"Success create a booking {viewModel.Title}"
        };
    }

    private static string FormatTime(string timeString)
    {
        if (string.IsNullOrEmpty(timeString))
            return string.Empty;

        string[] parts = timeString.Replace('.', ':').Split(':'); // Normalize separators
        if (parts.Length < 2)
            return timeString; // Return as is if format is incorrect

        string hours = parts[0].PadLeft(2, '0'); // Ensure two-digit hour
        string minutes = parts[1].PadLeft(2, '0'); // Ensure two-digit minutes

        return $"{hours}:{minutes}"; // Return as string, not TimeOnly
    }

    private static string FormatDate(string dateString)
    {
        if (string.IsNullOrEmpty(dateString))
            return string.Empty;

        if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
        {
            return $"{date.Day} {date.ToString("MMM", CultureInfo.InvariantCulture)} {date.Year}";
        }

        return dateString; // Return original string if parsing fails
    }

    public async Task<ReturnalModel> CheckSerialIsAlready(string serial)
    {
        if (string.IsNullOrEmpty(serial))
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Title = "Failed",
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Serial is required"
            };
        }

        var fetchData = await _repoRoomDisplayRepository.GetDisplaySerialBySerialNumber(serial);
        var fetch = _mapper.Map<RoomDisplayViewModel>(fetchData);

        if (fetch?.DisplaySerial == null)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Title = "Failed",
                StatusCode = StatusCodes.Status404NotFound,
                Message = "Display not available/registered"
            };
        }

        if (fetch.Enabled == 0)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Title = "Failed",
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Display is disabled"
            };
        }

        var fetchRoomData = await _repoRoomDisplayRepository.GetDataRoomDisplayByListID(fetch.Id);
        fetch.RoomSelectData = _mapper.Map<List<RoomDisplayInformationViewModel>>(fetchRoomData);


        var presignedUrlResult = _s3Service.GetPresignedUrl("display/background", fetch.Background, 120);

        if (presignedUrlResult != null)
        {
            fetch.Background = presignedUrlResult;
        }
        else
        {
            fetch.Background = string.Empty;
        }

        fetch.FloorIdEnc = fetch.FloorId.ToString();
        fetch.BuildingIdEnc = fetch.BuildingId.ToString();


        return new ReturnalModel
        {
            Status = ReturnalType.Success,
            Collection = fetch,
            Message = "Get success"
        };
    }

    public async Task<ReturnalModel> GetScheduledDisplay(DisplayScheduledFRViewModel request)
    {
        string defaultTimezone = _config?["OTHER_SETTING:DefaultTimeZone"] ?? "Asia/Jakarta";
        string timezone = !string.IsNullOrWhiteSpace(request.Timezone) ? request.Timezone : defaultTimezone;

        DateTime parsedDate; // Declare it outside to avoid scope issues

        try
        {
            // Convert input date and time to DateTime object
            DateTime localDateTime = DateTime.ParseExact($"{request.Date} {request.Time}", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            // Convert to server time zone
            TimeZoneInfo sourceTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            TimeZoneInfo serverTimeZone = TimeZoneInfo.FindSystemTimeZoneById(defaultTimezone);

            DateTime serverDateTime = TimeZoneInfo.ConvertTime(localDateTime, sourceTimeZone, serverTimeZone);

            // Format the converted date/time for database query
            request.Date = serverDateTime.ToString("yyyy-MM-dd");
            request.Time = serverDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            // Parse it into a DateTime object
            parsedDate = serverDateTime.Date;
        }
        catch (TimeZoneNotFoundException)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = "Invalid timezone provided."
            };
        }
        catch (FormatException)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = "Invalid date/time format."
            };
        }

        // Fetch data from the repository
        var fetchData = await _repoRoomDisplayRepository.GetMeetingDisplayInformation(parsedDate, request.Serial);

        // Check if data retrieval was successful
        if (fetchData != null)
        {
            var fetchMap = _mapper.Map<List<RoomDisplayInformationMeetingViewModel>>(fetchData);
            return new ReturnalModel
            {
                Status = ReturnalType.Success,
                Collection = fetchMap,
                Message = "Success get data to active"
            };
        }

        return new ReturnalModel
        {
            Status = ReturnalType.Failed,
            Message = "Failed error a active"
        };
    }

    public async Task<ReturnalModel> GetDisplayRoomAvailable(DisplayRoomAvailableViewModel request)
    {
        string defaultTimezone = _config?["OTHER_SETTING:DefaultTimeZone"] ?? "Asia/Jakarta";
        string timezone = !string.IsNullOrWhiteSpace(request.Timezone) ? request.Timezone : defaultTimezone;

        DateTime parsedDate;       // tanggal untuk filter hari
        DateTime serverDateTime;   // waktu server untuk "current"

        try
        {
            // Gabungkan date + time dari request
            DateTime localDateTime = DateTime.ParseExact(
                $"{request.Date} {request.Time}",
                "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture
            );

            // Konversi ke timezone server
            TimeZoneInfo sourceTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            TimeZoneInfo serverTimeZone = TimeZoneInfo.FindSystemTimeZoneById(defaultTimezone);

            serverDateTime = TimeZoneInfo.ConvertTime(localDateTime, sourceTimeZone, serverTimeZone);

            // Set nilai untuk query
            parsedDate = serverDateTime.Date;
        }
        catch (TimeZoneNotFoundException)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = "Invalid timezone provided."
            };
        }
        catch (FormatException)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = "Invalid date/time format."
            };
        }

        var fetchData = await _repoRoomDisplayRepository.GetMeetingRoomAvailableDisplayInformation(
            parsedDate,
            serverDateTime,
            request.Serial
        );

        if (fetchData != null)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Success,
                Collection = fetchData,
                Message = "Success get data to active"
            };
        }

        return new ReturnalModel
        {
            Status = ReturnalType.Failed,
            Message = "Failed error a active"
        };
    }



    private async Task<long> CalculateReservationCost(FastBookBookingViewModel databook, FastBookRoomViewModel room)
    {
        var dataSettingGeneral = await _repoSettingRuleBooking.GetSettingRuleBookingTopOne();
        var modulesPrice = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Price);

        if (modulesPrice?.IsEnabled != 1)
            return 0;

        int duration = FastBook.CalculateDuration(databook);
        int durationPerSlot = dataSettingGeneral?.Duration ?? 0;

        int meetingHours = (duration + durationPerSlot - 1) / durationPerSlot; // Round up

        return room.Price * meetingHours;
    }

    private async Task<string> CreateInvoiceOrderAsync(FastBookBookingViewModel databook, FastBookAlocationVMDefaultFR alocation, FastBookRoomViewModel room)
    {
        var modulesPrice = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Price);
        var modulesInvoice = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Invoice);

        long reservationCost = await CalculateReservationCost(databook, room);
        string invoiceFormat = await GenerateInvoiceFormatAsync(databook, alocation);

        if (string.IsNullOrEmpty(invoiceFormat))
            return string.Empty;

        var nikUser = context?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
        string formatInvoice = string.Empty;
        if (modulesInvoice?.IsEnabled == 1 && modulesPrice?.IsEnabled == 1)
        {
            string years = databook.Date.ToString("yyyy");
            string y_years = databook.Date.ToString("yy");
            string months = databook.Date.ToString("MM");
            string days = databook.Date.ToString("dd");

            var rowInvoice = await _repo.GetMaxOrderNumberAsync(years);

            string invAlocationID = $"{alocation.Id}-E-Meeting";

            if (string.IsNullOrEmpty(rowInvoice?.NoOrder))
            {
                string newNoUrut = "001";
                formatInvoice = $"{newNoUrut}/{invAlocationID}/{months}/{y_years}";
            }
            else
            {
                string oldNoInv = rowInvoice.NoOrder;
                string[] spOldInv = oldNoInv.Split('/');
                int noUrut = int.Parse(spOldInv[0]) + 1;
                string newNoUrut = noUrut.ToString("D3");
                formatInvoice = $"{newNoUrut}/{invAlocationID}/{months}/{y_years}";
            }
        }

        if (modulesInvoice?.IsEnabled == 1 && modulesPrice?.IsEnabled == 1 && databook.IsAlive == 1)
        {
            var dataInvoice = new BookingInvoice
            {
                InvoiceNo = _Random.Numeric(10, true).ToString(),
                InvoiceFormat = formatInvoice,
                BookingId = databook.BookingId,
                RentCost = reservationCost,
                Alocation = alocation.Id,
                TimeBefore = DateTime.Now,
                CreatedAt = DateTime.Now,
                CreatedBy = nikUser,
                InvoiceStatus = "0" // before send
            };
            await _bookingInvoiceRepository.Create(dataInvoice);
        }

        // var newInvoice = FastBook.CreateBookingInvoice(databook, alocation, reservationCost, invoiceFormat);
        // var mapNewInvoice = _mapper.Map<BookingInvoice>(newInvoice);
        // await _bookingInvoiceRepository.Create(mapNewInvoice);

        return invoiceFormat;
    }

    private async Task<string> GenerateInvoiceFormatAsync(FastBookBookingViewModel databook, FastBookAlocationVMDefaultFR alocation)
    {
        string year = databook.Date.ToString("yyyy");
        string shortYear = databook.Date.ToString("yy");
        string month = databook.Date.ToString("MM");

        var maxInvoice = await _pantryTransaksiRepository.GetMaxInvoiceOrderAsync(year);
        string invoiceAllocationId = $"{alocation.Id}-E-Meeting";

        string invoiceNumber = FastBook.GenerateInvoiceNumber(maxInvoice?.NoOrder!);

        return $"{invoiceNumber}/{invoiceAllocationId}/{month}/{shortYear}";
    }

    private SendingEmailViewModel CreateSendingBatchEmail(string bookingId, string batch, DateTime datetime)
    {
        return new SendingEmailViewModel
        {
            Batch = batch,
            Type = 1,
            BookingId = bookingId,
            Pending = "",
            IsStatus = 0, // Direct email PHP
            // IsStatus = 1, // Uncomment if necessary
            ErrorSending = 0,
            Success = 0,
            CreatedAt = datetime,
            UpdatedAt = datetime,
            IsDeleted = 0
        };
    }

    public SendingNotifViewModel CreateSendingBatchNotif(string bookingId, string batch, DateTime datetime)
    {
        return new SendingNotifViewModel
        {
            Batch = batch,
            Type = 1,
            BookingId = bookingId,
            IsStatus = 1,
            Pending = "",
            ErrorSending = 0,
            Success = 0,
            CreatedAt = datetime,
            UpdatedAt = datetime,
            IsDeleted = 0
        };
    }

    public  async Task<ReturnalModel> CreateEvent365(FastBookBookingViewModel databook, FastBookRoomViewModel room,
         Integration365 ms365, List<FastBookEmployeeViewModel> invInternal, List<FastBookBookingInvitationViewModel> invExternal)
    {
        var ex = "";
        var body = JsonConvert.DeserializeObject<Dictionary<string, object>>(ex);
        var accessToken = ms365.AccessToken.ToString();
        var userPrincipalName = ms365.UserPrincipalName.ToString();
        var ms365Room = await _repoModuleBackend.RoomSystemToIntegration(room.Radid);

        if (string.IsNullOrEmpty(room.ConfigMicrosoft))
        {
            return new ReturnalModel {
                Status = ReturnalType.Failed,
                Message = "Not microsoft room"
            };
        }

        var TZ = TimeZoneInfo.Local.StandardName;
        var ISO8601U = "yyyy-MM-ddTHH:mm:ss.fffK";
        var start = DateTime.Parse(databook.Start.ToString()).ToString(ISO8601U);
        var end = DateTime.Parse(databook.End.ToString()).ToString(ISO8601U);

        if (string.IsNullOrEmpty(ms365Room.Id))
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = "Id microsoft room not found/empty"
            };
        }

        var attendees = new List<Dictionary<string, object>>();

        foreach (var value in invInternal)
        {
            var datt = new Dictionary<string, object>
            {
                { "emailAddress", new Dictionary<string, string>
                    {
                        { "address", value.Email },
                        { "name", value.Name }
                    }
                },
                { "type", "required" }
            };

            if (value.IsPic == 1)
            {
                body["organizer"] = new Dictionary<string, object>
                {
                    { "emailAddress", new Dictionary<string, string>
                        {
                            { "name", value.Name },
                            { "address", value.Email }
                        }
                    }
                };
            }

            attendees.Add(datt);
        }

        foreach (var value in invExternal)
        {
            var datt = new Dictionary<string, object>
            {
                { "emailAddress", new Dictionary<string, string>
                    {
                            { "name", value.Name },
                            { "address", value.Email }
                    }
                },
                { "type", "required" }
            };

            attendees.Add(datt);
        }

        var dattroom = new Dictionary<string, object>
        {
            { "emailAddress", new Dictionary<string, string>
                {
                    { "address", ms365Room.EmailAddress.ToString() },
                    { "name", ms365Room.DisplayName.ToString() }
                }
            },
            { "type", "Resource" }
        };

        attendees.Add(dattroom);

        body["subject"] = databook.Title.ToString();
        body["location"] = new Dictionary<string, string> { { "displayName", databook.RoomName.ToString() } };
        body["attendees"] = attendees;
        body["start"] = new Dictionary<string, object>
        {
            { "dateTime", start },
            { "timeZone", TZ }
        };
        body["end"] = new Dictionary<string, object>
        {
            { "dateTime", end },
            { "timeZone", TZ }
        };
        body["originalStartTimeZone"] = TZ;
        body["originalEndTimeZone"] = TZ;

        // set static  MS_365_GRAPH && MS_365_GRAPH_PATH_EVENT
        var urlpath = "MS_365_GRAPH" + userPrincipalName + "MS_365_GRAPH_PATH_EVENT";
        var authorization = "Bearer " + accessToken;
        var headers = new Dictionary<string, string>
        {
            { "Authorization", authorization },
            { "Content-Type", "application/json" }
        };

        var res = await Send365Event(urlpath, headers, body);
        return res;
    }

    private async Task<ReturnalModel> Send365Event(string url, Dictionary<string, string> headers, object data = null)
    {
        var bodyJson = data != null ? Newtonsoft.Json.JsonConvert.SerializeObject(data) : string.Empty;

        try
        {
            var client = _apiCaller.GetHttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, _apiCaller.baseurl + url)
            {
                Content = new StringContent(bodyJson, Encoding.UTF8, "application/json")
            };

            // Attach headers
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return new ReturnalModel
                {
                    Status = ReturnalType.Failed,
                    Message = $"Error: {response.StatusCode}"
                };
            }

            var result = await response.Content.ReadAsStringAsync();

            return new ReturnalModel
            {
                Status = ReturnalType.Success,
                Collection = result
            };
        }
        catch (Exception ex)
        {
            return new ReturnalModel
            {
                Status = ReturnalType.Failed,
                Message = $"Exception: {ex.Message}"
            };
        }
    }

    private List<NotificationData> CreateNotifikasiCollectData(string bookingId, List<FastBookEmployeeViewModel> dataEmailInternal, FastBookBookingViewModel data, DateTime datetime)
    {
        var notifCollectData = new List<NotificationData>();

        foreach (var val in dataEmailInternal)
        {
            var notif = new NotificationData
            {
                Datetime = datetime,
                Nik = val.Nik, // User ID
                Type = 1, // Booking type is 1
                Value = bookingId, // Booking ID
                Title = "Invitation Meeting",
                Body = $"{data.Title} - {GetFormattedDate(data.Date)}",
                IsSending = 0,
                IsDeleted = 0,
                CreatedAt = datetime
            };

            notifCollectData.Add(notif);
        }

        return notifCollectData;
    }

    public string GetFormattedDate(DateOnly date)
    {
        if (DateTime.TryParseExact(date.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            return parsedDate.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
        }
        return date.ToString("dd MMM yyyy");
    }

    public async Task InsertNotifAdmin(int type, string title, string body)
    {
        var nikUser = context?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
        var notification = new NotificationAdmin
        {
            Nik = nikUser, // Get user from session
            Type = type,
            Title = title,
            Body = body,
            Datetime = DateTime.Now,
            IsRead = 0,
            IsSending = 0,
            IsDeleted = 0,
            CreatedAt = DateTime.Now
        };

        await _repoModuleBackend.InsertNotification(notification);
    }

    public async Task<string?> SendEmail(
        string typeEmail,
        BookingDataDto? booking,
        FastBookEmployeeViewModel? people,
        bool isExternal)
    {
        if (booking == null || people == null || string.IsNullOrEmpty(people.Email))
        {
            return "Invalid booking or recipient data";
        }

        // If external email, check if the module is enabled
        if (isExternal)
        {
            var modules = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Email);
            if (modules?.IsEnabled != 1)
            {
                return null;
            }
        }

        // Get email template data
        var template = await _repoModuleBackend.GetTemplateEmailSettingByType(typeEmail);
        if (template == null)
        {
            return "Template not found";
        }

        // var rootPath = Directory.GetCurrentDirectory();
        // var templateFolderPath = Path.Combine(rootPath, "5.Helpers.Consumer");
        // var templatePath = Path.Combine(templateFolderPath, GetTemplateFileName(typeEmail));
        var templatePath = FastBook.GetPathEmailTemplate(typeEmail);

        if (!File.Exists(templatePath))
        {
            return "Email template not found!";
        }

        string emailBody = await File.ReadAllTextAsync(templatePath);

        // Prepare participant URLs
        string baseUrl = _config["ApiUrls:BaseUrl"] ?? "";
        string participantUrl = isExternal
            ? $"{baseUrl}participant/eksternal/booking/{booking.BookingId}/email/{people.Email}/attendance"
            : $"{baseUrl}participant/internal/booking/{booking.BookingId}/employee/{people.Nik}/attendance";

        string urlHadir = $"{participantUrl}/1";
        string urlTidakHadir = $"{participantUrl}/0";

        // Replace placeholders with template data
        emailBody = emailBody
            .Replace("%title%", template.TitleOfText)
            .Replace("%kepada_text%", template.ToText)
            .Replace("%agenda_text%", template.TitleAgendaText)
            .Replace("%tanggal_text%", template.DateText)
            .Replace("%tempat_text%", template.Room)
            .Replace("%location_text%", template.DetailLocation)
            .Replace("%room_text%", template.Room)
            .Replace("%content_text%", template.ContentText)
            .Replace("%greeting_text%", template.GreetingText)
            .Replace("%attendance%", template.AttendanceText)
            .Replace("%notattendance%", template.AttendanceNoText)
            .Replace("%close_text%", template.CloseText)
            .Replace("%support_text%", template.SupportText)
            .Replace("%link_text%", template.MapLinkText)
            .Replace("%foot_text%", template.FootText)
            .Replace("%url%", template.Link);

        // Room & Building Information
        string building = !string.IsNullOrEmpty(booking.BuildingName) ? $"{booking.BuildingName} - " : "";
        string buildingLocation = !string.IsNullOrEmpty(booking.BuildingDetailAddress) ? $"{booking.BuildingDetailAddress} <br> " : "";

        // Generate Map Links
        var mapLinks = new List<string>();
        if (!string.IsNullOrEmpty(booking.BuildingGoogleMap))
            mapLinks.Add($"<a target='__blank' href='{booking.BuildingGoogleMap}'>Building Link</a>");
        if (!string.IsNullOrEmpty(booking.RoomGoogleMap))
            mapLinks.Add($"<a target='__blank' href='{booking.RoomGoogleMap}'>Room Link</a>");

        string mapLinksHtml = string.Join(" - ", mapLinks);


        string tempDir = _config["UploadFileSetting:attachmentFolder"] ?? "";

        // Generate QR code paths
        string imageQRName = $"{booking.BookingId}_{people.Pin}";
        string bucketName = _config["AwsSetting:Bucket"] ?? string.Empty;
        string bucketUrlTemplate = _config["AwsSetting:BucketUrl"] ?? string.Empty;

        string awsImageUrl = bucketUrlTemplate.Replace("{bucket}", bucketName);

        string qrImageUrl = awsImageUrl + $"qr/{imageQRName}.png";

        // string b64QrCode = await _attachmentListService.GenerateThumbnailBase64(qrImagePath) ?? "";
        string qrHtml = $"<img title='QR CODE' alt='QR CODE' src='{qrImageUrl}' style='width:205px;height:205px;' />";
        qrHtml += $"<br><a title='QR CODE' target='__blank' alt='QR CODE' href='{qrImageUrl}'>Click this if QR not show</a>";

        var startDate = FormatDate(booking.FormatTimeStart.Split(' ')[0]);
        var endDate = FormatDate(booking.FormatTimeEnd.Split(' ')[0]);
        var timeStart = FormatTime(booking.FormatTimeStart.Split(' ')[1]);
        var timeEnd = FormatTime(booking.FormatTimeEnd.Split(' ')[1]);

        // Replace placeholders with actual values
        emailBody = emailBody
            .Replace("%penyelenggara%", booking.Pic ?? "")
            .Replace("%kepada%", people.Name ?? "")
            .Replace("%agenda%", booking.Title ?? "")
            .Replace("%tanggal%", $"{startDate} - {endDate}")
            .Replace("%waktu%", $"{timeStart} - {timeEnd}")
            .Replace("%room%", $"{booking.RoomName}")
            .Replace("%tempat%", $"{building}{booking.RoomName}")
            .Replace("%location%", $"{buildingLocation}{booking.RoomLocation}")
            .Replace("%link_map%", mapLinksHtml)
            .Replace("%qrtattendance%", qrHtml)
            .Replace("%urlAttendance%", urlHadir)
            .Replace("%urlNotAttendance%", urlTidakHadir)
            .Replace("%orginizer%", people.Name ?? "")
            .Replace("%pin%", people.Pin);

        // Validate email address before sending

        var emailBodyJson = new SendMailViewModel();

        // Configure email settings
        emailBodyJson.Name = people.Name;
        emailBodyJson.To = people.Email;
        emailBodyJson.Subject = $"{template.TitleOfText} {booking.Title}";
        emailBodyJson.Body = emailBody;
        emailBodyJson.IsHtml = true;

        var getHttpUrl = await _repoModuleBackend.GetHttpUrlTop();
        var url = getHttpUrl.Url;

        // Ensure headers are not null or empty before deserialization
        Dictionary<string, string> headerDictionary = new();

        if (!string.IsNullOrEmpty(getHttpUrl.Headers))
        {
            try
            {
                var headers = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(getHttpUrl.Headers);
                headerDictionary = headers?.SelectMany(dict => dict).ToDictionary(kv => kv.Key, kv => kv.Value) ?? new Dictionary<string, string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing headers: {ex.Message}");
            }
        }
        

        await CurlPostNotifAsync(headerDictionary, url, emailBodyJson);
        

        return null;
    }

    public async Task PushNotificationAsync(
        string title,
        string body = null,
        List<FastBookEmployeeViewModel>? batch = null,
        int typeNotif = 1,
        bool insert = true)
    {
        var datetime = DateTime.Now;
        var configNotif = await _repoModuleBackend.GetTopNotificationConfig();
        var collect = new List<NotificationData>();

        if (batch == null || !batch.Any()) return;

        foreach (var recipient in batch)
        {
            var config = new NotificationConfig
            {
                Url = configNotif?.Url,
                Authorization = configNotif?.Authorization,
                Active = Convert.ToInt32(configNotif?.Active)
            };

            string topic = configNotif?.Topics + recipient.Nik;
            var payload = Fcmtopics(topic, title, body);
            var sendMsg = await FcmSendMessageAsync(config, payload);

            var notificationData = new NotificationData
            {
                Datetime = datetime,
                Nik = recipient.Nik,
                Title = title,
                Type = typeNotif,
                Body = body,
                IsSending = 1,
                CreatedAt = datetime,
                UpdatedAt = datetime,
                IsDeleted = 0
            };

            collect.Add(notificationData);
        }

        if (insert && collect.Any())
        {
            await _repoModuleBackend.CreateBulkNotificationData(collect);
        }
    }

    public string Fcmtopics(string topic, string title, object? body)
    {
        var payload = new
        {
            to = $"/topics/{topic}",
            notification = new
            {
                title = title,
                body = body,
                priority = "high",
                content_available = true
            },
            data = new
            {
                title = title,
                body = body,
                priority = "high",
                content_available = true
            }
        };

        return System.Text.Json.JsonSerializer.Serialize(payload);
    }

    public async Task<string> FcmSendMessageAsync(NotificationConfig config, string payloads)
    {
        if (config.Active == 1)
        {
            string url = config.Url;
            var headers = new
            {
                Authorization = config.Authorization,
                ContentType = "application/json"
            };

            return await CurlPostNotifAsync(headers, url, payloads);
        }
        else
        {
            return "{}";
        }
    }

    private async Task<string> CurlPostNotifAsync(dynamic headers, string url, object payload)
    {
        using (var httpClient = new HttpClient())
        {

            var response = await _apiCaller.POSTHttpRequest(url, payload, headers);

            return response;
        }
    }

    public async Task InsertNotifAdminApiAsync(int type, string title, string body, string nik)
    {

        var notification = new NotificationAdmin
        {
            Nik = nik,
            Type = type,
            Title = title,
            Body = body,
            Datetime = DateTime.Now,
            IsRead = 0,
            IsSending = 0,
            IsDeleted = 0,
            CreatedAt = DateTime.Now
        };

        await _repoModuleBackend.InsertNotification(notification);
    }

    public async Task BookingLockerForAttendeesAsync(string bookingId, List<FastBookEmployeeViewModel> dataEmailInternal, object data, DateTime datetime)
    {
        var dataLockerSystem = new List<string>();
        foreach (var vlo in dataEmailInternal)
        {
            if (!string.IsNullOrEmpty(vlo.CardNumber))
            {
                dataLockerSystem.Add(vlo.CardNumber);
            }
        }

        var dataLocker = await _repoModuleBackend.GetLockerByStatusTop();

        if (dataLocker != null && !string.IsNullOrEmpty(dataLocker.Name))
        {
            foreach (var noCard in dataLockerSystem)
            {
                await UploadDataToLockerSystemAsync(dataLocker.IpLocker, noCard);
            }
        }
    }

    public async Task<string> UploadDataToLockerSystemAsync(string ip, string noKartu)
    {
        if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(noKartu))
        {
            return "Invalid parameters";
        }

        string url = $"{ip}/api/formapi/apiregistercabin";
        var payload = JsonSerializer.Serialize(new { no_kartu = noKartu });
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        try
        {
            var client = _apiCaller.GetHttpClient(); 
            using var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    public async Task<FileReady> GetQrCodeDetailView(string id, int h = 60)
    {
        _attachmentListService.SetTableFolder("qr");
        var base64 = await _attachmentListService.GenerateThumbnailBase64(id, h);
        if (string.IsNullOrEmpty(base64))
        {
            base64 = await _attachmentListService.NoImageBase64("qr.png", h);
        }
        MemoryStream dataStream = _attachmentListService.ConvertBase64ToMemoryStream(base64);

        return new FileReady() { FileStream = dataStream, FileName = "Preview QR Detail" };
    }

    public async Task<ReturnalModel> CheckDoorOpenMeetingPin(CheckDoorOpenMeetingPinFRViewModel request)
    {
        var ret = new ReturnalModel();

        var rules = await _repoSettingRuleBooking.GetSettingRuleBookingTopOne();
        int doorBefore = rules?.NotifUnuseBeforeMeeting ?? 0;

        // Combine date and time into a DateTime object
        DateTime dateTime;
        string dateTimeString = $"{request.Date} {request.Time}".Trim();

        string[] formats = {
            "yyyy-MM-dd HH:mm:ss",  // Expected format
            "dd/MM/yyyy HH.mm",     // Format detected in your error message
            "dd/MM/yyyy HH:mm",     // Another possible variation
            "yyyy-MM-dd HH:mm",     // Without seconds
            "dd/MM/yyyy HH.mm:ss",  // Similar issue with dots
            "yyyy-MM-dd HH.mm",      // Similar issue with dots
            "MM/dd/yyyy HH:mm",
            "MM/dd/yyyy HH:mm:ss"
        };

        if (!DateTime.TryParseExact(dateTimeString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = $"Invalid date or time format. Expected one of {string.Join(", ", formats)}, but got '{dateTimeString}'";
            ret.StatusCode = (int)HttpStatusCode.BadRequest;
            return ret;
        }

        // Calculate start and end times
        DateTime startSum = dateTime.AddMinutes(doorBefore);
        DateTime endSum = dateTime.AddMinutes(-doorBefore);

        // No need to convert to string and back to DateTime
        DateTime startSumFormatted = startSum;
        DateTime endSumFormatted = endSum;


        var settingRuleBooking = await _repoBI.CheckDoorOpenMeetingPin(request.Date, startSumFormatted, endSumFormatted, request.Pin, request.RadId);

        var message = "";
        
        if (settingRuleBooking != null)
        {
            var openDoor = await OpenDoor(request.RadId, request.Pin);
            if (openDoor.Status == ReturnalType.Success){
                var getMessage = await _settingLogConfigRepository.GetListByIdAsync(3);
                if(getMessage.Count() <= 0){
                    message = "Pin have permission";
                }else{
                    message = getMessage.FirstOrDefault()?.Text ?? "Default message";
                }

                await InsertLogPin(settingRuleBooking.BookingId, request.RadId, 0, request.Pin, dateTime, message, 1);

                await _repoBI.UpdateAccessOpenPinRoomAsync(settingRuleBooking.BookingId, request.Pin);

                ret.Message = message;
                return ret;

            }else{

                var getMessage = await _settingLogConfigRepository.GetListByIdAsync(2);
                if (getMessage.Count() <= 0)
                {
                    message = "Access door not connected";
                }
                else
                {
                    message = getMessage.FirstOrDefault()?.Text ?? "Default message";
                }

                await InsertLogPin(settingRuleBooking.BookingId, request.RadId, 0, request.Pin, dateTime, message, 2);

                ret.Status = ReturnalType.Failed;
                ret.Message = message;
                ret.StatusCode = (int)HttpStatusCode.BadRequest;
                return ret;
            }
        }else{
			// route 2 with default pin

            if (Array.Exists(DefaultPin.Values, pin => pin == request.Pin))
            {

                var getMessage = await _settingLogConfigRepository.GetListByIdAsync(3);
                if (getMessage.Count() <= 0)
                {
                    message = "Pin default have permission";
                }
                else
                {
                    message = getMessage.FirstOrDefault()?.Text ?? "Default message";
                }

                await InsertLogPin("", request.RadId, 0, request.Pin, dateTime, message, 1);
                ret.Message = message;
                return ret;
            }else
            {
                var pinDefault = await _repoSettingRuleBooking.GetSettingRuleBookingByPinDefault(request.RadId);

                if(pinDefault != null){

                    var openDoor = await OpenDoor(request.RadId, request.Pin);

                    if (openDoor.Status == ReturnalType.Success)
                    {
                        var getMessage = await _settingLogConfigRepository.GetListByIdAsync(3);
                        if (getMessage.Count() <= 0)
                        {
                            message = "Pin default have permission";
                        }
                        else
                        {
                            message = getMessage.FirstOrDefault()?.Text ?? "Default message";
                        }

                        await InsertLogPin("", request.RadId, 0, request.Pin, dateTime, message, 1);

                        ret.Message = message;
                        return ret;

                    }else{

                        var getMessage = await _settingLogConfigRepository.GetListByIdAsync(2);
                        if (getMessage.Count() <= 0)
                        {
                            message = "Access door not connected";
                        }
                        else
                        {
                            message = getMessage.FirstOrDefault()?.Text ?? "Default message";
                        }

                        await InsertLogPin("", request.RadId, 0, request.Pin, dateTime, message, 2);
                        ret.Status = ReturnalType.Failed;
                        ret.Message = message;
                        ret.StatusCode = (int)HttpStatusCode.BadRequest;
                        return ret;
                    }

                }else{

                    var getMessage = await _settingLogConfigRepository.GetListByIdAsync(1);
                    if (getMessage.Count() <= 0)
                    {
                        message = "Pin not have permission";
                    }
                    else
                    {
                        message = getMessage.FirstOrDefault()?.Text ?? "Default message";
                    }

                    await InsertLogPin("", request.RadId, 0, request.Pin, dateTime, message, 2);

                    ret.Status = ReturnalType.Failed;
                    ret.Message = message;
                    ret.StatusCode = (int)HttpStatusCode.BadRequest;
                    return ret;
                }

            }

        }
    }

    private async Task<ReturnalModel> OpenDoor(string radId, string pin, string model = "")
    {
        var ret = new ReturnalModel();
        var port = _config["App:PortRuleBooking"];

        var settingRuleBooking = await _accessControlRepository.CheckDataDoorOpen(radId, EnumAccessControlModelControl.Reader);

        if (settingRuleBooking != null)
        {

            if (settingRuleBooking.Type == EnumAccessControlType.Falco || settingRuleBooking.Type == EnumAccessControlType.FalcoId)
            {
                var urlApiFalco = _config["ApiUrls:URLApiFalco"];
                var xmlBody = FastBook.FalcoPulseData(settingRuleBooking.IpController, settingRuleBooking.Channel);
                await _apiCaller.POSTHttpRequest(urlApiFalco!, xmlBody, null, null, "text/xml");

                return ret;
            }
            else if (settingRuleBooking.Type == EnumAccessControlType.EntryPass || settingRuleBooking.Type == EnumAccessControlType.EntryPassId)
            {

                var urlDoorPulse = _config["ApiUrls:URLApiDoorPulse"] + "/api/door/pulse?doorname=" + settingRuleBooking.AccessId;
                await _apiCaller.TryGETRequest(urlDoorPulse!);
                return ret;
            }
            else if (settingRuleBooking.Type == EnumAccessControlType.Custom || settingRuleBooking.Type == EnumAccessControlType.CustId)
            {
                var url = $"http://{settingRuleBooking.IpController}{port}/api/door/ON{settingRuleBooking.Channel}/{settingRuleBooking.Delay}";
                var apiDoor = await _apiCaller.TryGETRequest(url);
                if (int.TryParse(apiDoor, out int apiDoorResult) && apiDoorResult > 0 || apiDoor == null)
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = $"Error when fetch to: {url}";
                    ret.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

                return ret;
            }
        }
        else{

            ret.Status = ReturnalType.Failed;
            ret.Message = $"Error when fetch to Open Door PIN";
            ret.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        return ret;
    }
    private async Task InsertLogPin(string bookingId, string roomId, int isDefault, string pin, DateTime dateTime, string message, int status)
    {

        var nikUser = context?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
        var logEntry = new LogAccessRoom
        {
            BookingId = bookingId,
            RoomId = roomId,
            IsDefault = isDefault,
            Pin = pin,
            Datetime = dateTime,
            Msg = message,
            Status = status,
            Nik = nikUser
        };

        await _repoModuleBackend.InsertLogAccessRoom(logEntry);
    }
}