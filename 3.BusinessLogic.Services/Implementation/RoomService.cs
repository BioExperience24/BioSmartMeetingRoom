using System.Net;
using System.Transactions;
using _2.BusinessLogic.Services.Interface;
using _5.Helpers.Consumer.EnumType;
using _7.Entities.Models;
using Newtonsoft.Json;

namespace _3.BusinessLogic.Services.Implementation
{
    public class RoomService : BaseLongService<RoomViewModel, Room>, IRoomService
    {
        private readonly RoomRepository _repo;
        private readonly ModuleBackendRepository _repoModuleBackend;
        private readonly IAttachmentListService _attachmentListService;

        private readonly BookingRepository _repoBooking;
        private readonly IS3Service _s3Service;

        public RoomService(RoomRepository repo, IMapper mapper,
        IConfiguration config, IAttachmentListService attachmentListService, ModuleBackendRepository repoModuleBackend, BookingRepository repoBooking, IS3Service s3Service) : base(repo, mapper)
        {
            _repo = repo;
            _repoModuleBackend = repoModuleBackend;
            _repoBooking = repoBooking;

            // attachmentListService.SetTableFolder(config["UploadFileSetting:tableFolder:room"] ?? "room");
            attachmentListService.SetTableFolder("images");
            attachmentListService.SetExtensionAllowed(config["UploadFileSetting:imageExtensionAllowed"]!);
            attachmentListService.SetTypeAllowed(config["UploadFileSetting:imageContentTypeAllowed"]!);
            attachmentListService.SetSizeLimit(Convert.ToInt32(config["UploadFileSetting:imageSizeLimit"] ?? "8")); // MB
            _attachmentListService = attachmentListService;
            _s3Service = s3Service;
        }

        public async Task<IEnumerable<RoomViewModelAlt>> GetAllRoomItemAsync(bool withIsDisabled = true)
        {
            var items = await _repo.GetAllRoomItemAsync(withIsDisabled);

            return _mapper.Map<List<RoomViewModelAlt>>(items);
        }

        public async Task<int> GetCountRoomItemAsync(bool withIsDisabled = true)
        {
            var countItem = await _repo.GetCountRoomItemAsync(false);

            return countItem;
        }

        public async Task<IEnumerable<RoomViewModelAlt>> GetAllRoomRoomDisplayItemAsync()
        {
            var items = await _repo.GetAllRoomRoomDisplayItemAsync();

            return _mapper.Map<List<RoomViewModelAlt>>(items);
        }

        public async Task<IEnumerable<RoomViewModelAlt>> GetAllRoomWithRadidsItemAsycn(string[] radIds)
        {
            var items = await _repo.GetAllRoomWithRadidsItemAsycn(radIds);

            return _mapper.Map<List<RoomViewModelAlt>>(items);
        }

        public async Task<IEnumerable<RoomViewModelAlt>> GetAllRoomAvailableAsync(RoomVMFindAvailable request, bool withImage64 = true)
        {
            Room entity = new Room{};

            entity.KindRoom = (request.RoomCategory != "")
                    ? request.RoomCategory
                    : "room";

            if (request.RoomCategory == "trainingroom")
            {
                DateOnly dateFrom = DateOnly.Parse(request.Date);
                DateOnly dateUntil = DateOnly.Parse(request.DateUntil);
                
                entity.WorkDay = Enumerable
                    .Range(0, dateUntil.DayNumber - dateFrom.DayNumber + 1)
                    .Select(offset => dateFrom.AddDays(offset).ToString("dddd").ToUpper())
                    .Distinct()
                    .ToList();

                entity.WorkDate = dateFrom;
                entity.WorkDateUntil = dateUntil;
                
            }
            else 
            {
                if (request.Date != "")
                {
                    entity.WorkDay = new List<string>{ _String.ToDayName(request.Date) };
                    entity.WorkDate = DateOnly.Parse(request.Date);
                }
            }

            if (request.BuildingId > 0)
            {
                entity.BuildingId = request.BuildingId;
            }

            entity.WorkStart =  (request.TimeFrom != "") 
                    ? $"{_String.RemoveAllSpace(request.TimeFrom)}:00"
                    : "00:00:00";
            
            entity.WorkEnd = (request.TimeUntil != "") 
                    ? $"{_String.RemoveAllSpace(request.TimeUntil)}:00"
                    : entity.WorkStart;

            if (request.FacilityRoom != null)
            {
                entity.FacilityRoom = request.FacilityRoom.Select(number => number.ToString()).ToList();
            }

            if (request.Seat != null)
            {
                var capacities = request.Seat.Split("_");
                if (capacities.Count() > 0)
                {
                    entity.Capacities = Array.ConvertAll(capacities, int.Parse);
                }
            }

            if (request.IsAllDay == "on")
            {
                entity.IsAllDay = true;
            }

            var items = await _repo.GetAllRoomAvailableAsync(entity);

            var results = _mapper.Map<List<RoomViewModelAlt>>(items);

            if (withImage64)
            {
                var noImage = await _attachmentListService.NoImageBase64("no-image.jpg", 100);

                // List untuk task generasi thumbnail
                var tasks = results.Select(async item =>
                {
                    var image = noImage;
                    if (item.Image != null)
                    {
                        var base64 = await _attachmentListService.GenerateThumbnailBase64(item.Image, 100);
                        if (!string.IsNullOrEmpty(base64))
                        {
                            image = base64;
                        }
                    
                    }
                    item.Image = image;
                }).ToList();

                // Tunggu semua task selesai
                await Task.WhenAll(tasks);
            }

            // generate waktu yang sudah di booking
            var roomIds = results.Select(s => s.Radid).ToArray();

            if (roomIds.Any())
            {
                IEnumerable<Booking> bookRooms = new List<Booking>();

                if (request.RoomCategory != "trainingroom")
                {
                    bookRooms = await _repoBooking.GetBookingsByRoomIdsAndDateAsync(roomIds, (DateOnly)entity.WorkDate!);

                }
                else
                {
                    bookRooms = await _repoBooking.GetBookingsByRoomIdsAndDateRangeAsync(roomIds, (DateOnly)entity.WorkDate!, (DateOnly)entity.WorkDateUntil!);
                }
                
                if (bookRooms.Any())
                {
                    foreach (var item in results)
                    {
                        var bookTimes = bookRooms.Where(q => q.RoomId == item.Radid).Select(q => new { q.Start, q.End }).ToList();

                        List<string> lTimes = new();

                        foreach (var time in bookTimes)
                        {
                            var times = _DateTime.GenerateTimeSlots(time.Start, time.End, TimeSpan.FromMinutes(5));
                            
                            lTimes.AddRange(times);
                        }

                        item.ReservedTimes = lTimes;
                    }
                }

            }
            // .generate waktu yang sudah di booking

            return results;
        }

        public virtual async Task<IEnumerable<RoomDataViewModel>> GetRoomData()
        {
            var entities = await _repoModuleBackend.GetRoomDataAsync();
            var result = _mapper.Map<List<RoomDataViewModel>>(entities.Data);
            return result;
        }

        public async Task<List<RoomMergeDetailViewModel>> GetAllRoomMerge()
        {
            var entities = await _repoModuleBackend.GetAllRoomMerge();
            var result = _mapper.Map<List<RoomMergeDetailViewModel>>(entities);
            return result;
        }

        public async Task<List<SettingInvoiceTextViewModel>> GetInvoiceStatus()
        {
            var entities = await _repoModuleBackend.GetInvoiceStatus();
            var result = _mapper.Map<List<SettingInvoiceTextViewModel>>(entities);
            return result;
        }

        public async Task<List<RoomMergeDetailViewModel>> GetRoomMerge(string radId)
        {
            var entities = await _repoModuleBackend.GetRoomMerge(radId);
            var result = _mapper.Map<List<RoomMergeDetailViewModel>>(entities);
            return result;
        }

        public async Task<List<RoomMergeDetailViewModel>> GetStatusInvoice(string radId)
        {
            var entities = await _repoModuleBackend.GetRoomMerge(radId);
            var result = _mapper.Map<List<RoomMergeDetailViewModel>>(entities);
            return result;
        }

        //public async Task<List<SearchCriteriaViewModel>> GetReportusage(SearchCriteriaViewModel viewModel)
        //{
        //    var wreport = string.Empty;

        //    if (!string.IsNullOrEmpty(viewModel.BuildingSearch))
        //    {
        //        wreport += $" AND bu.id={viewModel.BuildingSearch} ";
        //    }

        //    if (!string.IsNullOrEmpty(viewModel.RoomSearch))
        //    {
        //        wreport += $" AND r.radid={viewModel.RoomSearch} ";
        //    }

        //    if (!string.IsNullOrEmpty(viewModel.DepartmentSearch))
        //    {
        //        wreport += $" AND a.id='{viewModel.DepartmentSearch}' ";
        //    }

        //    var wdate = $" AND b.date >= '{viewModel.Date1Search}' AND b.date <= '{viewModel.Date2Search}' ";


        //    //jika sudah ada autentikasi, tambahkan:
        //    //$userg = $this->session->userdata('levelid-nya');
        //    //var userg = 0;

        //    //switch (userg)
        //    //{
        //    //    case 1:
        //    //        // Admin
        //    //        wreport += " AND bi.is_pic = 1 ";
        //    //        break;
        //    //    case 2:
        //    //        wreport += " AND bi.nik ";
        //    //        break;
        //    //    default:
        //    //        // Optionally handle other cases if needed
        //    //        break;
        //    //}

        //    var entities = await _repoModuleBackend.GetReportusage(wreport, wdate);
        //    var result = _mapper.Map<List<SearchCriteriaViewModel>>(entities);
        //    return result;
        //}

        public virtual async Task<List<RoomViewModel>> GetSingleRoomData()
        {
            // Get the rooms from the repository (this returns a list of Room entities)
            var entities = await _repoModuleBackend.GetSingleRoomsAsync();

            // Map the entities (Room) to RoomDataViewModel
            var result = _mapper.Map<List<RoomViewModel>>(entities);

            return result;
        }


        public async Task<RoomVMUResponseFRViewModel> GetRoomById(long id)
        {
            // Get the room from the repository (this returns a Room entity)
            var entity = await _repo.GetById(id);

            var data = _mapper.Map<RoomVMUResponseFRViewModel>(entity);
            if (entity != null)
            {
                // Query room_detail table
                var facilityRoom2 = await _repoModuleBackend.GetRoomDetailById(id);

                var facilityRoomMap = _mapper.Map<List<RoomDetailViewModel>>(facilityRoom2);

                data.FacilityRoom2 = facilityRoomMap;


            }

            return data;
        }

        public async Task<List<RoomForUsageDetailListViewModel>> GetConfigRoomForUsageByIdRoom(long id)
        {
            // Get the room from the repository (this returns a Room entity)
            var entity = await _repoModuleBackend.GetRoomForUsageDetail(id);

            var data = _mapper.Map<List<RoomForUsageDetailListViewModel>>(entity);

            return data;
        }

        public async Task<RoomDetailsViewModel> GetRoomDetailsAsync(string pagename = "Room")
        {

            // Assuming session ID is provided or defaults to 1
            var sessionId = 1;

            // Fetch menu, module details, room usage, and check-in data
            var menu = await _repoModuleBackend.GetMenuAsync(pagename, 0, sessionId);
            var module_automation = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Automation);
            var vip = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.UserVIP);
            var module_price = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Price);
            var module_int_365 = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.Int365);
            var module_int_google = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.IntGoogle);
            var module_room_adv = await _repoModuleBackend.GetModuleByTextAsync(ModuleBackendTextModule.RoomAdvance);

            // Fetch room usage and check-in data
            var room_for_usage = await _repoModuleBackend.SelectAllRoomForUsageAsync();
            _mapper.Map<List<RoomForUsageViewModel>>(room_for_usage);

            var room_user_checkin = await _repoModuleBackend.SelectAllRoomForUserCheckinAsync();
            _mapper.Map<List<RoomUserCheckinViewModel>>(room_user_checkin);

            // If room advance module is enabled, fetch additional employee approval and permission data
            List<EmployeeWithAccessInfo> userApproval = null;
            List<EmployeeWithDetails> userPermission = null;
            if (module_room_adv?.IsEnabled == 1)
            {
                userApproval = await _repoModuleBackend.GetEmployeesWithAccessAsync();
                _mapper.Map<List<EmployeeWithAccessInfoViewModel>>(userApproval);

                userPermission = await _repoModuleBackend.GetEmployeesWithDetailsAsync();
                _mapper.Map<List<EmployeeWithDetailsViewModel>>(userPermission);

            }

            // Fetch building and floor data
            var building = await _repoModuleBackend.GetDataBuildingAsync(new Dictionary<string, object>());
            _mapper.Map<List<RoomBuildingViewModel>>(building);


            var floor = await _repoModuleBackend.GetDataFloorAsync(new Dictionary<string, object>());
            _mapper.Map<List<BuildingFloorViewModel>>(floor);

            // Map modules to the response view model with detailed properties
            var modules = new RoomModulesViewModel
            {
                Automation = module_automation != null ? new ModuleDetailsViewModel
                {
                    ModuleId = module_automation.ModuleId.ToString(),
                    ModuleText = module_automation.ModuleText,
                    Name = module_automation.Name,
                    ModuleSerial = module_automation.ModuleSerial,
                    IsEnabled = module_automation.IsEnabled
                } : new ModuleDetailsViewModel(),

                Vip = vip != null ? new ModuleDetailsViewModel
                {
                    ModuleId = vip.ModuleId.ToString(),
                    ModuleText = vip.ModuleText,
                    Name = vip.Name,
                    ModuleSerial = vip.ModuleSerial,
                    IsEnabled = vip.IsEnabled
                } : new ModuleDetailsViewModel(),

                Price = module_price != null ? new ModuleDetailsViewModel
                {
                    ModuleId = module_price.ModuleId.ToString(),
                    ModuleText = module_price.ModuleText,
                    Name = module_price.Name,
                    ModuleSerial = module_price.ModuleSerial,
                    IsEnabled = module_price.IsEnabled
                } : new ModuleDetailsViewModel(),

                Int365 = module_int_365 != null ? new ModuleDetailsViewModel
                {
                    ModuleId = module_int_365.ModuleId.ToString(),
                    ModuleText = module_int_365.ModuleText,
                    Name = module_int_365.Name,
                    ModuleSerial = module_int_365.ModuleSerial,
                    IsEnabled = module_int_365.IsEnabled
                } : new ModuleDetailsViewModel(),

                IntGoogle = module_int_google != null ? new ModuleDetailsViewModel
                {
                    ModuleId = module_int_google.ModuleId.ToString(),
                    ModuleText = module_int_google.ModuleText,
                    Name = module_int_google.Name,
                    ModuleSerial = module_int_google.ModuleSerial,
                    IsEnabled = module_int_google.IsEnabled
                } : new ModuleDetailsViewModel(),

                RoomAdv = module_room_adv != null ? new ModuleDetailsViewModel
                {
                    ModuleId = module_room_adv.ModuleId.ToString(),
                    ModuleText = module_room_adv.ModuleText,
                    Name = module_room_adv.Name,
                    ModuleSerial = module_room_adv.ModuleSerial,
                    IsEnabled = module_room_adv.IsEnabled
                } : new ModuleDetailsViewModel(),
            };

            // Returning response data in a structured format
            return new RoomDetailsViewModel
            {
                Pagename = pagename,
                Menu = menu,
                Building = building,
                Floor = floor,
                RoomForUsage = room_for_usage,
                UserApproval = userApproval,
                UserPermission = userPermission,
                RoomUserCheckin = room_user_checkin,
                Modules = modules
            };
        }

        public async Task<ReturnalModel> CreateRoom(RoomVMDefaultFR postData)
        {
            var ret = new ReturnalModel();

            var viewModel = _mapper.Map<RoomViewModel>(postData);

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {

                    var checkLicence = await CheckLicence();

                    if (checkLicence.Status == ReturnalType.Failed) {
                        ret.Message = checkLicence.Message;
                        ret.StatusCode = checkLicence.StatusCode;
                        return checkLicence;
                    }

                    var random = new Random();
                    string radid = random.Next(100000, 999999).ToString();

                    
	            	viewModel.Radid = radid;

                    // Handle the primary image upload
                    var (fileName, errMsg) = await DoUploadAsync(postData.Image);
                    if (errMsg != null)
                    {
                        ret.Status = ReturnalType.Failed;
                        ret.Message = errMsg; // Return specific error message
                        return ret;
                    }

                    if (fileName != null)
                    {
                        viewModel.Image = fileName;
                    }

                    // Handle additional images
                    string combinedImageNames = string.Empty;

                    if (postData.Image2 != null && postData.Image2.Count > 0)
                    {
                        // Use StringBuilder for better performance in concatenation
                        var imageBuilder = new StringBuilder();

                        for (int index = 0; index < postData.Image2.Count; index++)
                        {
                            var data = postData.Image2[index];

                            // Attempt to upload the image
                            var (fileName2, errMsg2) = await DoUploadAsync(data);
                            if (errMsg2 != null)
                            {
                                ret.Status = ReturnalType.Failed;
                                ret.Message = errMsg2; // Return specific error message for secondary image
                                return ret;
                            }

                            // Append the file name
                            imageBuilder.Append(fileName2);

                            // Add separator except for the last item
                            if (index < postData.Image2.Count - 1)
                            {
                                imageBuilder.Append("##");
                            }
                        }

                        combinedImageNames = imageBuilder.ToString();
                    }

                    // Assign combined image names if needed
                    if (!string.IsNullOrEmpty(combinedImageNames))
                    {
                        viewModel.Image2 = combinedImageNames; // Or assign to the appropriate field
                    }

                    if (string.IsNullOrEmpty(postData.WorkDay?.ToString()))
                    {
                        viewModel.WorkDay = [];
                    }

                    // Handle work_time
                    viewModel.WorkTime = postData.WorkStart + "-" + postData.WorkEnd;

                    // Handle work_start and work_end
                    viewModel.WorkStart = postData.WorkStart + ":00";
                    viewModel.WorkEnd = postData.WorkEnd + ":00";

                    // Set created_at and updated_at to the current datetime
                    var datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    viewModel.CreatedAt = datetime;
                    viewModel.UpdatedAt = datetime;

                    // Set is_deleted to 0
                    viewModel.IsDeleted =0;
                    viewModel.UpdatedAt = datetime;

                    var result_map = _mapper.Map<RoomViewModel>(viewModel);

                    var item = _mapper.Map<Room>(result_map);


                    var entity = await _repo.Create(item);

                    if (postData.FacilityRoomName != null)
                    {
                        // Delete existing data in the `room_detail` table for the specified room ID

                        await _repoModuleBackend.RemoveRoomDetail(item.Radid);

                        // Prepare the list for batch insert
                        var colInsertFac = new List<RoomDetail>();
                            foreach (var facilityId in postData.FacilityRoom)
                            {
                                colInsertFac.Add(new RoomDetail
                                {
                                    RoomId = item.Id,
                                    FacilityId = facilityId,
                                    Datetime = DateTime.Now
                                });
                            }

                            // Batch insert into `room_detail` table
                            if (colInsertFac.Count > 0)
                            {
                                await _repoModuleBackend.CreateBulkRoomDetail(colInsertFac);

                                //postData.FacilityRoom = string.Join(",", postData.FacilityRoomName);
                            }

                        // Remove FacilityRoomName from the post data
                        postData.FacilityRoomName = null;
                    }

                    if (postData.TypeRoom == "merge")
                    {
                        //await _repoModuleBackend.RemoveRoomMergeDetail(radid);

                        var colInsertMerge = new List<RoomMergeDetail>();

                        foreach (var mergeRoomId in postData?.MergeRoom)
                        {
                            var postMerge = new RoomMergeDetail
                            {
                                RoomId = entity?.Radid,
                                MergeRoomId = mergeRoomId
                            };
                            colInsertMerge.Add(postMerge);
                        }

                        if (colInsertMerge.Count > 0)
                        {
                            await _repoModuleBackend.CreateBulkRoomMergeDetail(colInsertMerge);
                        }
                    }

                    if (postData.RoomUsageDetail != null)
                    {
                        // Deserialize JSON string into a list of UsageDetail objects

                        var colInsertMergeRoomDetail = new List<RoomForUsageDetail>();

                        // Iterate over the deserialized details
                        foreach (var usage_detail in postData.RoomUsageDetail)
                        {
                            var postMerge = new RoomForUsageDetail
                            {
                                RoomId = entity?.Id ?? 0, // Default to 0 or another fallback value
                                RoomUsageId = usage_detail.RoomUsageId,
                                MinCap = usage_detail.MinCap,
                                Internal = usage_detail.Internal,
                                External = usage_detail.External
                            };

                            colInsertMergeRoomDetail.Add(postMerge);
                        }

                        // Only insert if there's data to insert
                        if (colInsertMergeRoomDetail.Count > 0)
                        {
                            await _repoModuleBackend.CreateBulkRoomForUsageDetail(colInsertMergeRoomDetail);
                        }
                    }

                    scope.Complete();

                    return ret;
                }
                catch (Exception)
                {
                    if (viewModel.Image != null)
                    {
                        await _attachmentListService.DeleteFile(viewModel.Image);
                        await _attachmentListService.DeleteFile(viewModel?.Image2);
                    }
                    throw;
                }
            }
        }


        public async Task<ReturnalModel> CheckLicence()
        {
            var ret = new ReturnalModel();

            var licenseInfo = await _repoModuleBackend.GetLicenseByModule();

            var checkRoomModuleLicense = await _repoModuleBackend.CheckRoomModuleLicense();

            var countnew = checkRoomModuleLicense.Count - 0 + 1;

            // Step 4: Check license status
            if (licenseInfo?.Status == "1" || licenseInfo?.Status.ToString() == "1")
            {
                var licenseQuantity = licenseInfo.Qty;

                if (countnew > licenseQuantity)
                {
                    ret.Status = ReturnalType.Failed;
                    ret.StatusCode = (int)HttpStatusCode.BadRequest;
                    ret.Message = "Failed: Room has reached the max limit. Please check your license.";
                }
            }
            else
            {

                ret.Status = ReturnalType.Failed;
                ret.StatusCode = (int)HttpStatusCode.BadRequest;
                ret.Message = "Failed: Room has limited access. Please check your license.";
            }
            ret.Status = ReturnalType.Success;
            ret.Message = "Room data retrieved successfully";

            return ret;
        }

        public async Task<ReturnalModel> UpdateRoom(RoomVMUpdateFRViewModel request, long id)
        {
            DateTime now = DateTime.Now;

            var old_data = await _repo.GetById(id);

            if (old_data == null)
            {
                return new ReturnalModel
                {
                    Status = ReturnalType.Failed,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Booked Time feature is disabled, please enable for use this feature."
                };
            }

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                var ret = new ReturnalModel();
                var viewModel = _mapper.Map<RoomViewModel>(request);
                viewModel.Id = id;

                try
                {


                    var item = _mapper.Map<Room>(viewModel);
                    item.IsDeleted = 0;
                    item.UpdatedAt = now;
                    item.CreatedBy = old_data.CreatedBy;
                    // item.CreatedBy = // uncomment jika auth sudah ada

                    if (request.FacilityRoomName != null)
                    {
                        // Delete existing data in the `room_detail` table for the specified room ID

                        await _repoModuleBackend.RemoveRoomDetail(old_data.Radid!);

                        // Prepare the list for batch insert
                        var colInsertFac = new List<RoomDetail>();
                        foreach (var facilityId in request.FacilityRoom)
                        {
                            colInsertFac.Add(new RoomDetail
                            {
                                RoomId = item.Id,
                                FacilityId = facilityId,
                                Datetime = DateTime.Now
                            });
                        }

                        // Batch insert into `room_detail` table
                        if (colInsertFac.Count > 0)
                        {
                            await _repoModuleBackend.CreateBulkRoomDetail(colInsertFac);

                            //postData.FacilityRoom = string.Join(",", postData.FacilityRoomName);
                        }

                        // Remove FacilityRoomName from the post data
                        request.FacilityRoomName = null;
                    }

                    if (request.TypeRoom == "merge")
                    {
                        //await _repoModuleBackend.RemoveRoomMergeDetail(radid);

                        var colInsertMerge = new List<RoomMergeDetail>();

                        foreach (var mergeRoomId in request?.MergeRoom)
                        {
                            var postMerge = new RoomMergeDetail
                            {
                                RoomId = item.Radid,
                                MergeRoomId = mergeRoomId
                            };
                            colInsertMerge.Add(postMerge);
                        }

                        if (colInsertMerge.Count > 0)
                        {
                            await _repoModuleBackend.CreateBulkRoomMergeDetail(colInsertMerge);
                        }
                    }


                    if (request.Image != null )
                    {
                        // Handle the primary image upload
                        var (fileName, errMsg) = await DoUploadAsync(request.Image);
                        if (errMsg != null)
                        {
                            ret.Status = ReturnalType.Failed;
                            ret.Message = errMsg; // Return specific error message
                            return ret;
                        }

                        if (fileName != null)
                        {
                            item.Image = fileName;
                        }
                    }
                    else
                    {
                        item.Image = old_data.Image;
                    }

                    var imageBuilder = new StringBuilder();

                    // Split the old image data into an array
                    var oldImages = old_data.Image2?.Split("##") ?? new string[3]; // Default to an array of 3 empty strings if null

                    if (request.Image2_1 != null)
                    {
                        // Handle the primary image upload
                        var (fileName, errMsg) = await DoUploadAsync(request.Image2_1);
                        if (errMsg != null)
                        {
                            ret.Status = ReturnalType.Failed;
                            ret.Message = errMsg; // Return specific error message
                            return ret;
                        }

                        // Update the first image
                        oldImages[0] = fileName;
                    }

                    if (request.Image2_2 != null)
                    {
                        // Handle the secondary image upload
                        var (fileName, errMsg) = await DoUploadAsync(request.Image2_2);
                        if (errMsg != null)
                        {
                            ret.Status = ReturnalType.Failed;
                            ret.Message = errMsg; // Return specific error message
                            return ret;
                        }

                        // Update the second image
                        oldImages[1] = fileName;
                    }

                    if (request.Image2_3 != null)
                    {
                        // Handle the tertiary image upload
                        var (fileName, errMsg) = await DoUploadAsync(request.Image2_3);
                        if (errMsg != null)
                        {
                            ret.Status = ReturnalType.Failed;
                            ret.Message = errMsg; // Return specific error message
                            return ret;
                        }

                        // Update the third image
                        oldImages[2] = fileName;
                    }

                    // Rebuild the string with updated values
                    imageBuilder.Append(string.Join("##", oldImages));

                    // The final updated image data
                    var updatedImage2 = imageBuilder.ToString();

                    // Use `updatedImage2` as needed

                    item.Image2 = imageBuilder.ToString();

                    if (string.IsNullOrEmpty(request.WorkDay?.ToString()))
                    {
                        item.WorkDay = [];
                    }

                    if(item.IsConfigSettingEnable == 1 && request.ConfigRoomForUsage != null && request.ConfigRoomForUsage.Count > 0)
                    {

                        // Delete existing data
                        await _repoModuleBackend.DeleteOldRoomForUsageDetailAsync(id);

                        try
                        {
                            if (request?.RoomUsageDetail != null)
                            {
                                // Deserialize the JSON data
                                var dataRoomForUsageDetail = JsonConvert.DeserializeObject<List<RoomForUsageDetailViewModel>>(request?.RoomUsageDetail);

                                var mapper_room_for_usage_detail = _mapper.Map<List<RoomForUsageDetail>>(dataRoomForUsageDetail);

                                // Check if the list has any elements
                                if (dataRoomForUsageDetail != null && dataRoomForUsageDetail.Count > 0)
                                {
                                    // Insert the new data in batch
                                    _repoModuleBackend.CreateBulkRoomForUsageDetail(mapper_room_for_usage_detail);
                                }
                            }
                        }
                        catch (Exception error)
                        {
                            throw error;
                        }
                    }


                    if (request.TypeRoom == "merge")
                    {
                        //await _repoModuleBackend.RemoveRoomMergeDetail(radid);

                        var colInsertMerge = new List<RoomMergeDetail>();

                        foreach (var mergeRoomId in request?.MergeRoom)
                        {
                            var postMerge = new RoomMergeDetail
                            {
                                RoomId = request.Radid,
                                MergeRoomId = mergeRoomId
                            };
                            colInsertMerge.Add(postMerge);
                        }

                        if (colInsertMerge.Count > 0)
                        {
                            await _repoModuleBackend.CreateBulkRoomMergeDetail(colInsertMerge);
                        }
                    }


                    // Handle work_time
                    item.WorkTime = request.WorkStart + "-" + request.WorkEnd;

                    // Handle work_start and work_end
                    item.WorkStart = request.WorkStart + ":00";
                    item.WorkEnd = request.WorkEnd + ":00";

                    // Set created_at and updated_at to the current datetime
                    item.UpdatedAt = DateTime.Now;


                    // Set is_deleted to 0
                    item.IsDeleted = 0;

                    var entity = await _repo.UpdateRoom(item);

                    scope.Complete();

                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

                return ret;
            }
        }

        public async Task<(string?, string?)> DoUploadAsync(IFormFile? file)
        {
            string radid = _Random.Numeric(3).ToString();
            DateTime now = DateTime.Now;
            var fn = $"{now.ToString("yyyyMMddHHmmss")}_floor_{radid}";

            // Upload file if exists
            if (file != null && file.Length > 0)
            {
                _attachmentListService.SetTableFolder("images");
                return await _attachmentListService.FileUploadProcess(file, fn);
            }

            return (null, null);
        }


        

    }

}