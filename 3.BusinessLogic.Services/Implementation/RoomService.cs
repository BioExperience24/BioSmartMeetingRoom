using System.Diagnostics.Metrics;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using System.Transactions;
using _2.BusinessLogic.Services.Interface;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Encryption;
using _5.Helpers.Consumer.EnumType;
using _6.Repositories.Repository;
using _7.Entities.Models;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace _3.BusinessLogic.Services.Implementation
{
    public class RoomService : BaseLongService<RoomViewModel, Room>, IRoomService
    {
        private readonly RoomRepository _repo;
        private readonly ModuleBackendRepository _repoModuleBackend;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper __mapper;

        private readonly IAttachmentListService _attachmentListService;

        public RoomService(RoomRepository repo, IMapper mapper,
        IConfiguration config, IAttachmentListService attachmentListService, ModuleBackendRepository repoModuleBackend, IHttpContextAccessor httpContextAccessor) : base(repo, mapper)
        {
            _repo = repo;
            __mapper = mapper;
            _repoModuleBackend = repoModuleBackend;
            _httpContextAccessor = httpContextAccessor;

            attachmentListService.SetTableFolder(
                config["UploadFileSetting:tableFolder:room"] ?? "room");
            attachmentListService.SetExtensionAllowed(config["UploadFileSetting:imageExtensionAllowed"]!);
            attachmentListService.SetTypeAllowed(config["UploadFileSetting:imageContentTypeAllowed"]!);
            attachmentListService.SetSizeLimit(Convert.ToInt32(config["UploadFileSetting:imageSizeLimit"] ?? "8")); // MB
            _attachmentListService = attachmentListService;
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

        public async Task<List<RoomMergeDetailViewModel>> GetRoomMerge(long id)
        {
            var entities = await _repoModuleBackend.GetRoomMerge(id);
            var result = _mapper.Map<List<RoomMergeDetailViewModel>>(entities);
            return result;
        }

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

        public async Task<List<RoomForUsageDetailViewModel>> GetConfigRoomForUsageByIdRoom(long id)
        {
            // Get the room from the repository (this returns a Room entity)
            var entity = await _repoModuleBackend.GetRoomForUsageDetail(id);

            var data = _mapper.Map<List<RoomForUsageDetailViewModel>>(entity);

            return data;
        }

        public async Task<RoomDetailsViewModel> GetRoomDetailsAsync()
        {
            var pagename = "Room";

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

                        await _repoModuleBackend.RemoveRoomDetail(item.Id);

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
                                RoomId = entity?.Id,
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

            var countnew = (checkRoomModuleLicense.Count - 0) + 1;

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

        public async Task<ReturnalModel?> UpdateRoom(RoomVMUpdateFRViewModel request, long id)
        {
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
                    DateTime now = DateTime.Now;

                    var item = _mapper.Map<Room>(viewModel);
                    item.IsDeleted = 0;
                    item.UpdatedAt = now;
                    // item.CreatedBy = // uncomment jika auth sudah ada
                    // item.UpdatedBy = // uncomment jika auth sudah ada

                    if (request.FacilityRoomName != null)
                    {
                        // Delete existing data in the `room_detail` table for the specified room ID

                        await _repoModuleBackend.RemoveRoomDetail(item.Id);

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
                                RoomId = id,
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

                    // Handle additional images
                    string combinedImageNames = string.Empty;

                    if (request.Image2 != null && request.Image2.Count > 0)
                    {
                        // Use StringBuilder for better performance in concatenation
                        var imageBuilder = new StringBuilder();

                        for (int index = 0; index < request.Image2.Count; index++)
                        {
                            var data = request.Image2[index];

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
                            if (index < request.Image2.Count - 1)
                            {
                                imageBuilder.Append("##");
                            }
                        }

                        combinedImageNames = imageBuilder.ToString();
                    }

                    // Assign combined image names if needed
                    if (!string.IsNullOrEmpty(combinedImageNames))
                    {
                        item.Image2 = combinedImageNames; // Or assign to the appropriate field
                    }

                    if (string.IsNullOrEmpty(request.WorkDay?.ToString()))
                    {
                        item.WorkDay = [];
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
                return await _attachmentListService.FileUploadProcess(file, fn);
            }

            return (null, null);
        }


        public async Task<FileReady> GetRoomDetailView(string id, int h = 60)
        {
            var base64 = await _attachmentListService.GenerateThumbnailBase64(id, h);
            if (string.IsNullOrEmpty(base64))
            {
                base64 = await _attachmentListService.NoImageBase64("pantry.jpeg", h);
            }
            MemoryStream dataStream = _attachmentListService.ConvertBase64ToMemoryStream(base64);

            return new FileReady() { FileStream = dataStream, FileName = "Preview Pantry Detail" };
        }

    }

}