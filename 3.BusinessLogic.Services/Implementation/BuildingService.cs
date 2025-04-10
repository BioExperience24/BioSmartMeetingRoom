using System.Transactions;
using _2.BusinessLogic.Services.Interface;
using _5.Helpers.Consumer._Encryption;
using _5.Helpers.Consumer._Encryption._Secure;

namespace _3.BusinessLogic.Services.Implementation
{
    public class BuildingService : BaseLongService<BuildingViewModel, Building>, IBuildingService
    {
        private readonly BuildingRepository _repo;
        private readonly IMapper __mapper;
        private readonly IAttachmentListService _attachmentListService;
        private readonly _Aes _aes;
        private readonly ISecureService _secureService;

        public BuildingService(BuildingRepository repo, IMapper mapper, IAttachmentListService attachmentListService, IConfiguration config, ISecureService secureService) 
            : base(repo, mapper)
        { 
            _repo = repo;
            __mapper = mapper;

            attachmentListService.SetTableFolder(
                config["UploadFileSetting:tableFolder:building"] ?? "building");
            attachmentListService.SetExtensionAllowed(config["UploadFileSetting:imageExtensionAllowed"]!);
            attachmentListService.SetTypeAllowed(config["UploadFileSetting:imageContentTypeAllowed"]!);
            attachmentListService.SetSizeLimit(Convert.ToInt32(config["UploadFileSetting:imageSizeLimit"] ?? "8")); // MB
            _attachmentListService = attachmentListService;

            _aes = new _Aes(config["EncryptSetting:AesKeyEncryptor"] ?? "VeRYV3rYSUp3RdUuP3RrRsEcr3tKeY!!");

            _secureService = secureService;
        }

        public async Task<IEnumerable<BuildingViewModel>> GetAllItemAsync(long? excludeId = null)
        {
            var items = await _repo.GetAllItemAsync(excludeId);

            var results = __mapper.Map<List<BuildingViewModel>>(items);

            // List untuk task generasi thumbnail
            var tasks = results.Select(async item =>
            {
                item.Image = item.Image != null
                    ? await _attachmentListService.GenerateThumbnailBase64(item.Image, 125)
                    : null;

                if (item.Id != null)
                {
                    // item.Encrypt = _aes.Encrypt(item.Id.ToString()!);
                    item.Encrypt = _secureService.Encrypt(item.Id.ToString()!);
                }
            }).ToList();

            // Tunggu semua task selesai
            await Task.WhenAll(tasks);

            return results;
        }

        public async Task<BuildingViewModel?> GetItemByIdAsync(long id)
        {
            var item = await _repo.GetItemByIdAsync(id);
            if (item == null)
            {
                return null;
            }
            
            var result = __mapper.Map<BuildingViewModel>(item);

            if (result.Image != null)
            {
                result.Image = await _attachmentListService.GenerateThumbnailBase64(result.Image, 125);
            }

            if (result.Id != null)
            {
                // result.Encrypt = _aes.Encrypt(result.Id.ToString()!);
                result.Encrypt = _secureService.Encrypt(result.Id.ToString()!);
            }

            return result;
        }

        public async Task<BuildingViewModel?> CreateAsync(BuildingVMDefaultFR request)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    DateTime now = DateTime.Now;
                    var item = __mapper.Map<Building>(request);
                    // item.Id = _String.RandomNumeric(10);
                    item.Id = new DateTimeOffset(now).ToUnixTimeMilliseconds() + _Random.Numeric(3);
                    item.IsDeleted = 0;
                    item.CreatedAt = now;
                    item.UpdatedAt = now;

                    var building = await _repo.Create(item);

                    scope.Complete();

                    return __mapper.Map<BuildingViewModel>(building);
                }
                catch (Exception)
                {
                    if (request.Image != null)
                    {
                        await _attachmentListService.DeleteFile(request.Image);
                    }
                    throw;
                }
            }
        }

        public async Task<BuildingViewModel?> UpdateAsync(long id, BuildingVMDefaultFR request)
        {
            var building = await _repo.GetById(id);
            
            if (building == null)
            {
                return null;
            }

            var oldestImage = building.Image;

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    DateTime now = DateTime.Now;

                    __mapper.Map(request, building);

                    building.IsDeleted = 0;
                    building.UpdatedAt = now;
                    building.DetailAddress = request.DetailAddress == null ? null : building.DetailAddress;
                    building.GoogleMap = request.GoogleMap == null ? null : building.GoogleMap;

                    await _repo.Update(building);
                    
                    scope.Complete();

                    if (request.Image != null && oldestImage != null)
                    {
                        await _attachmentListService.DeleteFile(oldestImage);
                    }

                    return __mapper.Map<BuildingViewModel>(building);
                }
                catch (Exception)
                {
                    if (request.Image != null)
                    {
                        await _attachmentListService.DeleteFile(request.Image);
                    }
                    throw;
                }
            }
        }

        public async Task<BuildingViewModel?> DeleteAsync(BuildingVMDeleteFR request)
        {
            var building = await _repo.GetById(request.Id);
            
            if (building == null)
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

                    var name = building.Name;
                    __mapper.Map(request, building);

                    building.Name = name;
                    building.IsDeleted = 1;
                    building.UpdatedAt = now;

                    await _repo.Update(building);
                    
                    scope.Complete();

                    return __mapper.Map<BuildingViewModel>(building);
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