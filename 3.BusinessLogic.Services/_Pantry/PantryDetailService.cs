using _2.BusinessLogic.Services.Interface;
using _7.Entities.Models;
using System.Net.Mail;

namespace _3.BusinessLogic.Services.Implementation;

public class PantryDetailService(PantryDetailRepository repo, IMapper mapper, IAttachmentListService attachment, IConfiguration config)
    : BaseLongService<PantryDetailViewModel, PantryDetail>(repo, mapper), IPantryDetailService
{
    private readonly string tableFolder = attachment.SetTableFolder("PantryDetail");
    private readonly string[] extension = attachment.SetExtensionAllowed(config["UploadFileSetting:imageExtensionAllowed"]!);
    private readonly string[] typeFile = attachment.SetTypeAllowed(config["UploadFileSetting:imageContentTypeAllowed"]!);
    private readonly long sizeLimit = attachment.SetSizeLimit(Convert.ToInt32(config["UploadFileSetting:imageSizeLimit"] ?? "8"));

    public override async Task<IEnumerable<PantryDetailViewModel>> GetAll()
    {
        var getallPantry = await base.GetAll();

        //var result = await FillPicImageBase64(getallPantry);

        return getallPantry.OrderBy(x => x.name);
    }

    private async Task<List<PantryDetailViewModel>> FillPicImageBase64(IEnumerable<PantryDetailViewModel> getallPantry)
    {
        List<PantryDetailViewModel> result = [];

        Dictionary<long, Task<string>> listTask = [];
        foreach (var pantry in getallPantry)
        {
            if (!string.IsNullOrEmpty(pantry.pic))
            {
                listTask.Add(pantry.Id ?? -1, attachment.GenerateThumbnailBase64(pantry.Id?.ToString()));
            }
            result.Add(pantry);
        }

        foreach (var item in listTask)
        {
            var findObj = result.FirstOrDefault(x => x.Id == item.Key);
            var img64 = await item.Value;
            if (!string.IsNullOrEmpty(img64) && findObj != null)
            {
                findObj.pic = img64;
            }
        }

        return result;
    }

    public override async Task<PantryDetailViewModel?> GetById(long id)
    {
        var result = await base.GetById(id);
        if (result?.pic != null)
        {
            var img64 = await attachment.GenerateThumbnailBase64(result.Id?.ToString());
            if (!string.IsNullOrEmpty(img64))
            {
                result.picBase64 = img64;
            }
        }
        return result;
    }

    public override async Task<PantryDetailViewModel?> Create(PantryDetailViewModel viewModel)
    {
        viewModel.pic = viewModel.image?.FileName;
        viewModel.created_at = DateTime.Now;
        var result = await base.Create(viewModel);
        if (result != null)
        {
            await attachment.FileUploadProcess(viewModel.image, result.Id?.ToString());
        }

        return result;
    }

    public override async Task<PantryDetailViewModel?> Update(PantryDetailViewModel viewModel)
    {
        var temp = viewModel.image?.FileName;
        if (!string.IsNullOrEmpty(temp))
        {
            viewModel.pic = temp;
        }
        viewModel.updated_at = DateTime.Now;
        var result = await base.Update(viewModel);
        if (result != null)
        {
            await attachment.FileUploadProcess(viewModel.image, result.Id?.ToString());
        }
        return result;
    }

    public async Task<IEnumerable<PantryDetailViewModel>> GetByPantryId(string id)
    {
        var getallPantry = await base.GetListByField("pantry_id", id);

        //var result = await FillPicImageBase64(getallPantry);

        return getallPantry.OrderBy(x => x.Id);
    }

    public async Task<FileReady> GetPantryDetailView(long id, int h = 60)
    {
        var base64 = await attachment.GenerateThumbnailBase64(id.ToString(), h);
        if (string.IsNullOrEmpty(base64))
        {
            base64 = await attachment.NoImageBase64("pantry.jpeg", h);
        }
        MemoryStream dataStream = attachment.ConvertBase64ToMemoryStream(base64);

        return new FileReady() { FileStream = dataStream, FileName = "Preview Pantry Detail" };
    }
}

