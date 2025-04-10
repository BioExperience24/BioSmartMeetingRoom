

using System.Transactions;
using _2.BusinessLogic.Services.Interface;
using _5.Helpers.Consumer.EnumType;

namespace _3.BusinessLogic.Services.Implementation;

public class CompanyService : BaseService<CompanyViewModel, Company>, ICompanyService
{
    private readonly CompanyRepository _repo;
    private readonly IMapper __mapper;
    private readonly IAttachmentListService _attachmentListService;
    public CompanyService(CompanyRepository repo, IMapper mapper, IAttachmentListService attachmentListService, IConfiguration config) : base(repo, mapper)
    {
        _repo = repo;
        __mapper = mapper;

        attachmentListService.SetTableFolder(config["UploadFileSetting:tableFolder:company"] ?? "company");
        attachmentListService.SetExtensionAllowed(config["UploadFileSetting:imageExtensionAllowed"]!);
        attachmentListService.SetTypeAllowed(config["UploadFileSetting:imageContentTypeAllowed"]!);
        attachmentListService.SetSizeLimit(Convert.ToInt32(config["UploadFileSetting:imageSizeLimit"] ?? "8")); // MB
        _attachmentListService = attachmentListService;
    }

    public async Task<CompanyViewModel?> GetOneItemAsync(bool withImageBase64 = true)
    {
        var result = await _repo.GetOneItemAsync();
        if (result != null && withImageBase64 == true)
        {
            var imagesTask = new[]
            {
                _attachmentListService.GenerateThumbnailBase64(result.Picture!, 81),
                _attachmentListService.GenerateThumbnailBase64(result.Icon!, 81),
                _attachmentListService.GenerateThumbnailBase64(result.Logo!, 81),
                _attachmentListService.GenerateThumbnailBase64(result.MenuBar!, 81)
            };

            // Menunggu semua task selesai
            var thumbnails = await Task.WhenAll(imagesTask);

            result.Picture = thumbnails[0];
            result.Icon = thumbnails[1];
            result.Logo = thumbnails[2];
            result.MenuBar = thumbnails[3];

        }
        return result == null ? null : __mapper.Map<CompanyViewModel>(result);
    }

    public override async Task<CompanyViewModel?> Update(CompanyViewModel request)
    {
        var company = await _repo.GetOneItemAsync();

        if (company == null)
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
                company.Name = request.Name ?? "";
                company.Address = request.Address ?? "";
                company.City = request.City ?? "";
                company.State = request.State ?? "";
                company.Phone = request.Phone ?? "";
                company.Email = request.Email ?? "";
                company.UrlAddress = request.UrlAddress;

                await _repo.UpdateAsync(company);

                scope.Complete();

                return _mapper.Map<CompanyViewModel>(company);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public async Task<ReturnalModel> UploadMediaAsync(CompanyVMMediaFR request)
    {
        ReturnalModel ret = new() { };

        ret.Status = ReturnalType.Failed;

        var company = await _repo.GetOneItemAsync();

        if (company == null)
        {
            ret.Message = "Failed update a company image";
            return ret;
        }

        if (request.File != null && request.File.Length > 0)
        {
            var fileTypeMap = new Dictionary<string, string>
            {
                { "bg", "bg_logo_company.jpg" },
                { "icon", "icon_logo_company.png" },
                { "logo", "logo_company.png" },
                { "menu", "menu_logo_company.png" }
            };
            var fn = fileTypeMap[request.Type!] ?? "logo_company";

            var (fileName, errMsg) = await _attachmentListService.FileUploadToWWWroot(request.File, "media", fn);
            if (errMsg != null)
            {
                // ret.Message = "Failed to upload image";
                ret.Message = errMsg;
                return ret;
            }

            switch (request.Type)
            {
                case "bg":
                    company.Picture = fileName!;
                    break;

                case "icon":
                    company.Icon = fileName;
                    break;

                case "logo":
                    company.Logo = fileName;
                    break;

                case "menu":
                    company.MenuBar = fileName;
                    break;

                default:
                    company.Picture = fileName!;
                    break;

            }

            await _repo.UpdateAsync(company);

            ret.Status = ReturnalType.Success;
            ret.Message = "Success update a company image";
            ret.Collection = request;
        }

        return ret;
    }
}
