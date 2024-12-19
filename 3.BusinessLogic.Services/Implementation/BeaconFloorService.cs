
using System.Transactions;
using _2.BusinessLogic.Services.Interface;
using _5.Helpers.Consumer._Encryption;

namespace _3.BusinessLogic.Services.Implementation;

public class BeaconFloorService : BaseLongService<BeaconFloorViewModel, BeaconFloor>, IBeaconFloorService
{
    private readonly IMapper __mapper;
    
    private readonly BeaconFloorRepository _repo;

    private readonly IAttachmentListService _attachmentListService;

    public BeaconFloorService(
        IMapper mapper, 
        BeaconFloorRepository repo,
        IAttachmentListService attachmentListService, 
        IConfiguration config
    ) 
        : base(repo, mapper)
    { 
        _repo = repo;
        __mapper = mapper;

        attachmentListService.SetTableFolder(
            config["UploadFileSetting:tableFolder:beaconFloor"] ?? "beaconfloor");
        attachmentListService.SetExtensionAllowed(config["UploadFileSetting:imageExtensionAllowed"]!);
        attachmentListService.SetTypeAllowed(config["UploadFileSetting:imageContentTypeAllowed"]!);
        attachmentListService.SetSizeLimit(Convert.ToInt32(config["UploadFileSetting:imageSizeLimit"] ?? "8")); // MB
        _attachmentListService = attachmentListService;
    }

    public async Task<IEnumerable<BeaconFloorViewModel>> GetAllItemAsync()
    {
        var beaconFloors = await _repo.GetAllItemAsync();

        var results = __mapper.Map<List<BeaconFloorViewModel>>(beaconFloors);

        // List untuk task generasi thumbnail
        var tasks = results.Select(async item =>
        {
            item.Image = item.Image != null
                ? await _attachmentListService.GenerateThumbnailBase64(item.Image, 81)
                : null;
        }).ToList();

        // Tunggu semua task selesai
        await Task.WhenAll(tasks);

        return results;
    }

    public async Task<BeaconFloorViewModel?> GetItemByIdAsync(long id)
    {
        var beaconFloor = await _repo.GetItemByIdAsync(id);
        if (beaconFloor == null)
        {
            return null;
        }

        var result = __mapper.Map<BeaconFloorViewModel>(beaconFloor);
        result.Image = result.Image != null
                ? await _attachmentListService.GenerateThumbnailBase64(result.Image, 81)
                : null;
                
        return result;
    }

    public override async Task<BeaconFloorViewModel?> Create(BeaconFloorViewModel viewModel)
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
                
                var item = _mapper.Map<BeaconFloor>(viewModel);
                item.IsDeleted = 0;
                item.UpdatedAt = now;

                var entity = await _repo.Create(item);
                
                scope.Complete();

                return entity == null ? null : _mapper.Map<BeaconFloorViewModel>(entity);
            }
            catch (Exception)
            {
                if (viewModel.Image != null)
                {
                    await _attachmentListService.DeleteFile(viewModel.Image);
                }
                throw;
            }
        }
    }

    public override async Task<BeaconFloorViewModel?> Update(BeaconFloorViewModel viewModel)
    {
        var item = await _repo.GetById(viewModel.Id.GetValueOrDefault());
        
        if (item == null)
        {
            return null;
        }
        var oldImage = item.Image;
        __mapper.Map(viewModel, item);

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                DateTime now = DateTime.Now;
                
                item.IsDeleted = 0;
                item.Image = item.Image == null ? oldImage : item.Image;
                item.UpdatedAt = now;
                // item.CreatedBy = // uncomment jika auth sudah ada
                // item.UpdatedBy = // uncomment jika auth sudah ada

                await _repo.Update(item);
                
                scope.Complete();

                if (oldImage != null && viewModel.Image != null)
                {
                    await _attachmentListService.DeleteFile(oldImage);
                }

                return item == null ? null : _mapper.Map<BeaconFloorViewModel>(item);
            }
            catch (Exception)
            {
                if (viewModel.Image != null)
                {
                    await _attachmentListService.DeleteFile(viewModel.Image);
                }
                throw;
            }
        }
    }

    public override async Task<BeaconFloorViewModel?> SoftDelete(long id)
    {
        var entity = await _repo.GetById(id);
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
                // entity.CreatedBy = // uncomment jika auth sudah ada
                // entity.UpdatedBy = // uncomment jika auth sudah ada
                
                await _repository.Update(entity);
                
                scope.Complete();

                return _mapper.Map<BeaconFloorViewModel>(entity);

            }
            catch (Exception)
            {
                throw;
            }
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
}

