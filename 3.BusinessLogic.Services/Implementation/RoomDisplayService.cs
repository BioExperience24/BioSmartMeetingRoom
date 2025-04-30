using System.Security.Claims;
using System.Transactions;
using _2.BusinessLogic.Services.Interface;
using _5.Helpers.Consumer._Encryption;
using _5.Helpers.Consumer._Encryption._Secure;
using _5.Helpers.Consumer.EnumType;
using Microsoft.Extensions.DependencyInjection;

namespace _3.BusinessLogic.Services.Implementation
{
    public class RoomDisplayService 
        : BaseLongService<RoomDisplayViewModel, RoomDisplay>, IRoomDisplayService
    {
        private readonly IAccessControlService _accessControlService;
        private readonly IAttachmentListService _attachmentListService;
        private readonly RoomDisplayRepository _repo;
        private readonly RoomDisplayInformationRepository _roomDisplayInformationRepo;
        private readonly LicenseListRepository _licenseListRepo;
        private readonly IServiceProvider _serviceProvider;
        private readonly _Aes _aes;
        private readonly IHttpContextAccessor _httpCtx;
        private readonly IAPIMainDisplayService _apiMainDisplayService;
        private readonly ISecureService _secureService;

        public RoomDisplayService(
            IMapper mapper,
            RoomDisplayRepository repo,
            RoomDisplayInformationRepository roomDisplayInformationRepo,
            LicenseListRepository licenseListRepo,
            IAccessControlService accessControlService,
            IAttachmentListService attachmentListService,
            IConfiguration config,
            IServiceProvider serviceProvider,
            IAPIMainDisplayService apiMainDisplayService,
            IHttpContextAccessor httpCtx,
            ISecureService secureService
        ) : base (repo, mapper)
        {
            _repo = repo;
            _roomDisplayInformationRepo = roomDisplayInformationRepo;
            _licenseListRepo = licenseListRepo;

            _accessControlService = accessControlService;

            attachmentListService.SetTableFolder(
                config["UploadFileSetting:tableFolder:roomDisplay"] ?? "display/background");
            attachmentListService.SetExtensionAllowed(config["UploadFileSetting:imageExtensionAllowed"]!);
            attachmentListService.SetTypeAllowed(config["UploadFileSetting:imageContentTypeAllowed"]!);
            attachmentListService.SetSizeLimit(Convert.ToInt32(config["UploadFileSetting:imageSizeLimit"] ?? "8")); // MB
            _attachmentListService = attachmentListService;
            _apiMainDisplayService = apiMainDisplayService;

            _serviceProvider = serviceProvider;

            _aes = new _Aes(config["EncryptSetting:AesKeyEncryptor"] ?? "VeRYV3rYSUp3RdUuP3RrRsEcr3tKeY!!");

            _httpCtx = httpCtx;

            _secureService = secureService;
        }

        public async Task<IEnumerable<RoomDisplayViewModel>> GetAllItemAsync()
        {
            var items = await _repo.GetAllItemAsync();

            var result = _mapper.Map<List<RoomDisplayViewModel>>(items);

            var displayIds = result.Select(q => q.Id ?? 0).ToList();
            var allDisplayInformations = await _roomDisplayInformationRepo.GetAllItemFilteredByDisplayIds(displayIds);

            var tasks = result.Select(async item => {
                var displayInformation = allDisplayInformations.Where(q => q.DisplayId == item.Id).ToList();
                if (displayInformation.Any())
                {
                    item.RoomSelectData = _mapper.Map<List<RoomDisplayInformationViewModel>>(displayInformation);
                }
                
                if (item.BuildingId != 0)
                {
                    // item.BuildingIdEnc = _aes.Encrypt(item.BuildingId.ToString());
                    item.BuildingIdEnc = _secureService.Encrypt(item.BuildingId.ToString());
                }

                if (item.FloorId != 0)
                {
                    // item.FloorIdEnc = _aes.Encrypt(item.FloorId.ToString());
                    item.FloorIdEnc = _secureService.Encrypt(item.FloorId.ToString());
                }
                
                item.Background = item.Background != ""
                ? await _attachmentListService.GenerateThumbnailBase64(item.Background, 118)
                : "";

            }).ToArray();

            // Tunggu semua task selesai
            await Task.WhenAll(tasks);
            
            return result;
        }

        public async Task<ReturnalModel> SaveAsync(RoomDisplayVMCreateFR request)
        {
            ReturnalModel ret = new();
            ret.Message = "Success save a display";

            // from token
            var authUserNIK = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
            // .from token
            DateTime now = DateTime.Now;
            
            RoomDisplay? entity = null;
            if (request.Id != null)
            {
                entity = await _repo.GetById(request.Id ?? 0);
                
                if (entity == null)
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = "Failed, data not found";
                    return ret;
                }
            }

            var roomId = string.Empty;
            var roomSelect = string.Empty;
            var dataDisplayInformation = new List<RoomDisplayInformationViewModel>();
            if (request.Type == "allroom")
            {
                if (request.RoomSelectArr.Length == 0)
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = "Failed, minimum select 1 room";
                    return ret;
                }

                roomId = request.RoomSelectArr[0];
                roomSelect = string.Join("##", request.RoomSelectArr);
                dataDisplayInformation = request.RoomSelected;
            }

            var background = string.Empty;
            if (request.FileBackground != null)
            {
                var (fileName, errMsg) = await DoUploadAsync(request.FileBackground);

                if (errMsg != null)
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = errMsg;
                    return ret;
                }

                if (fileName != null)
                {
                    background = fileName;
                }
            }

            using (var scope = new TransactionScope (
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var viewModel = _mapper.Map<RoomDisplayViewModel>(request);

                    if (entity == null)
                    {
                        var license = await _licenseListRepo.GetItemFilteredByModule("module_display");
                        int ttlRoomDisplay = await _repo.CountAsync();

                        if (license == null || license.Status != "1")
                        {
                            ret.Status = ReturnalType.Failed;
                            ret.Message = "Failed Display has limited access, please check your license";
                            return ret;
                        }

                        if ((ttlRoomDisplay + 1) > license.Qty)
                        {
                            ret.Status = ReturnalType.Failed;
                            ret.Message = "Failed Display has max limited, please check your license";
                            return ret;
                        }

                        entity = _mapper.Map<RoomDisplay>(viewModel);

                        if (!string.IsNullOrEmpty(request.Building))
                        {
                            // var buildingId = _aes.Decrypt(request.Building);
                            var buildingId = _secureService.Decrypt(request.Building);
                            entity.BuildingId = buildingId != null ? Convert.ToInt64(buildingId) : null;
                        }

                        if (!string.IsNullOrEmpty(request.Floor))
                        {
                            // var floorId = _aes.Decrypt(request.Floor);
                            var floorId = _secureService.Decrypt(request.Floor);
                            entity.FloorId = floorId != null ? Convert.ToInt64(floorId) : null;
                        }

                        entity.Enabled = 0;
                        entity.HardwareUuid = "";
                        entity.HardwareInfo = ""; 
                        entity.HardwareLastsync = now;
                        entity.StatusSync = 0;
                        entity.Background = background;
                        entity.BackgroundUpdate = !string.IsNullOrEmpty(background) ? 1 : 0;
                        entity.CreatedAt = now;
                        entity.UpdatedAt = now;
                        entity.CreatedBy = authUserNIK;
                        entity.IsDeleted = 0;

                        if (!string.IsNullOrEmpty(roomId))
                        {
                            entity.RoomId = roomId;
                            entity.RoomSelect = roomSelect;
                        }

                        var roomDisplay = await _repo.Create(entity);

                        if (request.Type == "allroom")
                        {
                            List<RoomDisplayInformation> dataRoomDisplayInformation = new();
                            foreach (var item in dataDisplayInformation)
                            {
                                dataRoomDisplayInformation.Add(new RoomDisplayInformation
                                {
                                    DisplayId = roomDisplay!.Id,
                                    RoomId = item.RoomId,
                                    Icon = item.Icon,
                                    Distance = item.Distance
                                });
                            }

                            await _roomDisplayInformationRepo.CreateBulkAsync(dataRoomDisplayInformation);
                        }

                        scope.Complete();

                        var roomDisplayMap = roomDisplay != null ? _mapper.Map<RoomDisplayViewModel>(roomDisplay) : null;

                        ret.Collection = roomDisplayMap;
                    } 
                    else {
                        if (!string.IsNullOrEmpty(request.Building))
                        {
                            // var buildingId = _aes.Decrypt(request.Building);
                            var buildingId = _secureService.Decrypt(request.Building);
                            entity.BuildingId = buildingId != null ? Convert.ToInt64(buildingId) : null;
                        }

                        if (!string.IsNullOrEmpty(request.Floor))
                        {
                            // var floorId = _aes.Decrypt(request.Floor);
                            var floorId = _secureService.Decrypt(request.Floor);
                            entity.FloorId = floorId != null ? Convert.ToInt64(floorId) : null;
                        }

                        if (!string.IsNullOrEmpty(roomId))
                        {
                            entity.RoomId = roomId;
                            entity.RoomSelect = roomSelect;
                        }
                        else {
                            entity.RoomId = viewModel.RoomId;
                            entity.RoomSelect = viewModel.RoomSelect;
                        }

                        var oldBackground = entity.Background;
                        if (!string.IsNullOrEmpty(background))
                        {
                            entity.Background = background;
                            entity.BackgroundUpdate = 1;
                        }

                        entity.DisplaySerial = viewModel.DisplaySerial;
                        entity.Type = viewModel.Type;
                        entity.ColorOccupied = viewModel.ColorOccupied;
                        entity.ColorAvailable = viewModel.ColorAvailable;
                        entity.Name = viewModel.Name;
                        entity.Description = viewModel.Description;
                        entity.UpdatedAt = now;
                        entity.UpdatedBy = authUserNIK;

                        await _repo.Update(entity);

                        await _roomDisplayInformationRepo.DeleteFilteredByDisplayId(entity.Id ?? 0);
                        
                        if (request.Type == "allroom")
                        {
                            List<RoomDisplayInformation> dataRoomDisplayInformation = new();
                            foreach (var item in dataDisplayInformation)
                            {
                                dataRoomDisplayInformation.Add(new RoomDisplayInformation
                                {
                                    DisplayId = entity!.Id,
                                    RoomId = item.RoomId,
                                    Icon = item.Icon,
                                    Distance = item.Distance
                                });
                            }

                            await _roomDisplayInformationRepo.CreateBulkAsync(dataRoomDisplayInformation);
                        }

                        scope.Complete();

                        if (background != "" && oldBackground != null)
                        {
                            await _attachmentListService.DeleteFile(oldBackground);
                        }

                        var roomDisplayMap = entity != null ? _mapper.Map<RoomDisplayViewModel>(entity) : null;

                        ret.Collection = roomDisplayMap;
                    }

                    return ret;
                }
                catch (Exception)
                {
                    if (string.IsNullOrEmpty(background))
                    {
                        await _attachmentListService.DeleteFile(background);
                    }
                    throw;
                }
            }
        }

        public override async Task<RoomDisplayViewModel?> Update(RoomDisplayViewModel viewModel)
        {
            var item = await _repo.GetById(viewModel.Id ?? 0);

            if (item == null)
            {
                return null;
            }

            var currentBackground = item.Background;

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    DateTime now = DateTime.Now;

                    if (viewModel.Background != "")
                    {
                        item.Background = viewModel.Background;
                        item.BackgroundUpdate = 1;
                    }

                    item.DisplaySerial = viewModel.DisplaySerial;
                    item.RoomId = viewModel.RoomId;
                    item.RoomSelect = viewModel.RoomSelect;
                    item.StatusSync = 2;
                    item.UpdatedAt = now;
                    item.Name = viewModel.Name;
                    item.Description = viewModel.Description;
                    // item.UpdatedBy = ""; // uncomment jika data auth sudah ada

                    await _repo.Update(item);

                    scope.Complete();

                    if (viewModel.Background != "" && currentBackground != null)
                    {
                        await _attachmentListService.DeleteFile(currentBackground);
                    }

                    return item != null ? _mapper.Map<RoomDisplayViewModel>(item) : null;
                }
                catch (Exception)
                {
                    if (viewModel.Background != "")
                    {
                        await _attachmentListService.DeleteFile(viewModel.Background!);
                    }
                    throw;
                }
            }
        }

        public async Task<ReturnalModel> DisplayUpdateSerialSync(DisplayUpdateSerialSyncFRViewModel viewModel)
        {
            var ret = new ReturnalModel();
            var item = await _apiMainDisplayService.CheckSerialIsAlready(viewModel.Serial!);

            if (item == null)
            {
                return new ReturnalModel
                {
                    Status = ReturnalType.Failed,
                    Title = "Failed",
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Display serial not found"
                };
            }
            var displaySerial = await _repo.GetDisplaySerialBySerialNumber(viewModel.Serial!);

            if (displaySerial == null)
            {
                return new ReturnalModel
                {
                    Status = ReturnalType.Failed,
                    Title = "Failed",
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Display not available/registered"
                };
            }
            

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    DateTime now = DateTime.Now;

                    displaySerial.HardwareLastsync = now;
                    displaySerial.DisplaySerial = viewModel.Serial;
                    displaySerial.HardwareInfo = viewModel.HardWareInfo;
                    displaySerial.HardwareUuid = viewModel.HardwareUuid;

                    await _repo.Update(displaySerial);

                    scope.Complete();

                    return ret;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async  Task<RoomDisplayViewModel?> ChangeStatusEnabledAsync(RoomDisplayViewModel viewModel)
        {
            // from token
            var authUserNIK = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
            // .from token

            var item = await _repo.GetById(viewModel.Id ?? 0);

            if (item == null)
            {
                return null;
            }

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    DateTime now = DateTime.Now;
                    
                    item.Enabled = viewModel.Enabled;
                    // item.StatusSync = 2;
                    item.StatusSync = 0;
                    item.UpdatedAt = now;
                    item.UpdatedBy = authUserNIK;
                    if (!string.IsNullOrEmpty(viewModel.DisableMsg))
                    {
                        item.DisableMsg = viewModel.DisableMsg;
                    }

                    await _repo.Update(item);

                    scope.Complete();

                    return item != null ? _mapper.Map<RoomDisplayViewModel>(item) : null;   
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public override async Task<RoomDisplayViewModel?> SoftDelete(long id)
        {
            // from token
            var authUserNIK = _httpCtx?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
            // .from token

            var entity = await _repository.GetById(id);
            if (entity == null)
            {
                return null;
            }

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    DateTime now = DateTime.Now;

                    entity.IsDeleted = 1;
                    entity.UpdatedAt = now;
                    entity.UpdatedBy = authUserNIK; // uncomment jika data auth user sudah ada

                    await _repo.Update(entity);

                    scope.Complete();

                    return _mapper.Map<RoomDisplayViewModel>(entity);

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<(string?, string?)> DoUploadAsync(IFormFile? file)
        {
            if (file != null && file.Length > 0)
            {
                return await _attachmentListService.FileUploadProcess(file);
            }

            return (null, null);
        }
    }
}