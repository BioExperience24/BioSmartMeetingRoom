using _2.BusinessLogic.Services.Interface;
using _7.Entities.Models;
using System;

namespace _3.BusinessLogic.Services.Implementation;

public class PantryService(PantryRepository repo, IMapper mapper, IAttachmentListService attachment, IConfiguration config)
    : BaseLongService<PantryViewModel, Pantry>(repo, mapper), IPantryService
{
    private readonly string tableFolder = attachment.SetTableFolder(config["UploadFileSetting:tableFolder:pantry"] ?? "Pantry");
    private readonly string[] extension = attachment.SetExtensionAllowed(config["UploadFileSetting:imageExtensionAllowed"]!);
    private readonly string[] typeFile = attachment.SetTypeAllowed(config["UploadFileSetting:imageContentTypeAllowed"]!);
    private readonly long sizeLimit = attachment.SetSizeLimit(Convert.ToInt32(config["UploadFileSetting:imageSizeLimit"] ?? "8"));

    public override async Task<PantryViewModel?> GetById(long id)
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

    public async Task<IEnumerable<PantryViewModel>> GetAllPantryAndImage()
    {
        var getallPantry = await base.GetAll();
        //image ambil dr API Pantry/GetPantryView
        return getallPantry.OrderBy(x => x.name);
    }

    public override async Task<PantryViewModel?> Update(PantryViewModel cReq)
    {
        var temp = cReq.image?.FileName;
        if (!string.IsNullOrEmpty(temp))
        {
            cReq.pic = temp;
        }
        cReq.updated_at = DateTime.Now;
        var result = await base.Update(cReq);
        if (result != null && cReq.image != null)
        {
            var fileName = Path.ChangeExtension(cReq.image.FileName, ".jpg");
            await attachment.FileUploadProcess(cReq.image, result.Id?.ToString());
        }
        return result;
    }

    public async Task<PantryViewModel?> CreatePantry(PantryViewModel cReq)
    {
        cReq.pic = cReq.image?.FileName;
        cReq.created_at = DateTime.Now;
        var result = await base.Create(cReq);
        if (result != null && cReq.image != null)
        {
            var fileName = Path.ChangeExtension(cReq.image.FileName, ".jpg");
            await attachment.FileUploadProcess(cReq.image, result.Id?.ToString());
        }

        return result;
    }

    public async Task<FileReady> GetPantryView(long id, int h = 80)
    {
        var base64 = await attachment.GenerateThumbnailBase64(id.ToString() + ".jpg", h);
        if (string.IsNullOrEmpty(base64))
        {
            base64 = await attachment.NoImageBase64("pantry.jpeg", h);
        }
        MemoryStream dataStream = attachment.ConvertBase64ToMemoryStream(base64);

        return new FileReady() { FileStream = dataStream, FileName = "Preview Pantry Detail" };
    }
}

