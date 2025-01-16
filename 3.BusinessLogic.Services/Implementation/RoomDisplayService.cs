

using System.Transactions;
using _2.BusinessLogic.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Index.HPRtree;

namespace _3.BusinessLogic.Services.Implementation
{
    public class RoomDisplayService 
        : BaseLongService<RoomDisplayViewModel, RoomDisplay>, IRoomDisplayService
    {
        private readonly IAccessControlService _accessControlService;
        private readonly IAttachmentListService _attachmentListService;
        private readonly RoomDisplayRepository _repo;
        private readonly IServiceProvider _serviceProvider;

        public RoomDisplayService(
            IMapper mapper,
            RoomDisplayRepository repo,
            IAccessControlService accessControlService,
            IAttachmentListService attachmentListService,
            IConfiguration config,
            IServiceProvider serviceProvider
        ) : base (repo, mapper)
        {
            _repo = repo;

            _accessControlService = accessControlService;

            attachmentListService.SetTableFolder(
                config["UploadFileSetting:tableFolder:roomDisplay"] ?? "display/background");
            attachmentListService.SetExtensionAllowed(config["UploadFileSetting:imageExtensionAllowed"]!);
            attachmentListService.SetTypeAllowed(config["UploadFileSetting:imageContentTypeAllowed"]!);
            attachmentListService.SetSizeLimit(Convert.ToInt32(config["UploadFileSetting:imageSizeLimit"] ?? "8")); // MB
            _attachmentListService = attachmentListService;

            _serviceProvider = serviceProvider;
        }

        public async Task<IEnumerable<RoomDisplayViewModel>> GetAllItemAsync()
        {
            var items = await _repo.GetAllItemAsync();

            var result = _mapper.Map<List<RoomDisplayViewModel>>(items);

            var tasks = result.Select(async item => {
                string[] roomRadids = item.RoomSelect.Split(",", StringSplitOptions.RemoveEmptyEntries);
                if (roomRadids.Count() > 0)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var accessControlService = scope.ServiceProvider.GetRequiredService<IAccessControlService>();
                        item.RoomSelectData = await accessControlService.GetAllItemRoomWithRadidsAsycn(roomRadids);
                    }
                }
                
                item.Background = item.Background != ""
                ? await _attachmentListService.GenerateThumbnailBase64(item.Background, 118)
                : "";

            }).ToArray();

            // Tunggu semua task selesai
            await Task.WhenAll(tasks);
            
            return result;
        }

        public override async Task<RoomDisplayViewModel?> Create(RoomDisplayViewModel viewModel)
        {
            using (var scope = new TransactionScope (
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    DateTime now = DateTime.Now;

                    // TODO: create method repo checkDisplayModuleLicense from Model_License.php
                    // On progress

                    var entity = _mapper.Map<RoomDisplay>(viewModel);

                    entity.Enabled = 0;
                    entity.HardwareUuid = "";
                    entity.HardwareInfo = ""; 
                    entity.HardwareLastsync = now;
                    entity.StatusSync = 0;
                    entity.BackgroundUpdate = 1;
                    entity.CreatedAt = now;
                    entity.UpdatedAt = now;
                    // entity.CreatedBy = ""; // uncomment jika data auth sudah ada

                    var item = await _repo.Create(entity);

                    scope.Complete();

                    return item != null ? _mapper.Map<RoomDisplayViewModel>(item) : null;
                }
                catch (Exception)
                {
                    if (viewModel.Background != "")
                    {
                        await _attachmentListService.DeleteFile(viewModel.Background);
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

        public async  Task<RoomDisplayViewModel?> ChangeStatusEnabledAsync(RoomDisplayViewModel viewModel)
        {
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
                    item.StatusSync = 2;
                    item.CreatedAt = now;
                    // item.CreatedBy = ""; // uncomment jika data auth user sudah ada

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
                    // entity.UpdatedBy = ""; // uncomment jika data auth user sudah ada

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