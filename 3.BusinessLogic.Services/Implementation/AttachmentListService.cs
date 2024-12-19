using _2.BusinessLogic.Services.Interface;
using _5.Helpers.Consumer._Response;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Hosting;
using SkiaSharp;

namespace _3.BusinessLogic.Services.Implementation;

public class AttachmentListService : IAttachmentListService
{
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _env;

    private readonly MethodHelperService _helper;
    private readonly string _attachmentFolder;
    private string[] _extensionAllowed;
    private string[] _contentTypeAllowed;
    private long _sizeLimit;
    private string _thisTableFolder;
    public AttachmentListService(IConfiguration config, MethodHelperService helper, IWebHostEnvironment env)
    {
        _extensionAllowed = (config["UploadFileSetting:extensionAllowed"] ?? "").Split("#");
        _contentTypeAllowed = (config["UploadFileSetting:contentTypeAllowed"] ?? "").Split("#");
        var limitConfig = config["UploadFileSetting:sizeLimit"] ?? "20";//MB
        _attachmentFolder = config["UploadFileSetting:attachmentFolder"] ?? "\\map";
        _helper = helper;
        _config = config;
        _thisTableFolder = _attachmentFolder + "attachment_table";
        _sizeLimit = Convert.ToInt32(limitConfig);
        _env = env;
    }

    public string SetTableFolder(string folder)
    {
        _thisTableFolder = _attachmentFolder + folder;
        return _thisTableFolder;
    }

    public string[] SetExtensionAllowed(string extension)
    {
        _extensionAllowed = extension.Split("#");
        return _extensionAllowed;
    }

    public string[] SetTypeAllowed(string type)
    {
        _contentTypeAllowed = type.Split("#");
        return _contentTypeAllowed;
    }

    public long SetSizeLimit(long size)
    {
        _sizeLimit = size;
        return _sizeLimit;
    }

    public async Task<ReturnalModel> ProcessFileUploadToBlob(IFormFile uploadedFile)
    {
        ReturnalModel result = new()
        {
            Status = "Success",
            Message = "Attachment File Uploaded Successfully",
        };
        if (uploadedFile.Length > 0)
        {
            var errMsg = IsFileAllowed(uploadedFile);
            if (errMsg != null)
            {
                result.StatusCode = 400;
                result.Title = ReturnalType.BadRequest;
                result.Status = ReturnalType.Failed;
                // result.Message = "File Not Allowed";
                result.Message = errMsg;
                return result;
            }
            await InsertToAttachmentList(uploadedFile);
        }

        return result;
    }

    public async Task<(string?, string?)> FileUploadProcess(IFormFile? file, string? fileName = null)
    {
        if (file != null && file.Length > 0)
        {
            var errMsg = IsFileAllowed(file);
            if (errMsg != null)
            {
                return (null, errMsg);
            }

            fileName = await InsertToAttachment(file, fileName);
        }

        return (fileName, null);
    }

    public async Task DeleteFile(string fileName)
    {
        try
        {
            await _helper.IDeleteFile(_thisTableFolder, fileName);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<FileReady> DownloadFile(Guid id)
    {
        //var objekFile = await GetById(id);
        //adding bytes to memory stream   

        //if (objekFile == null)
        //    return null; // returns a NotFoundResult with Status404NotFound response.
        //MemoryStream dataStream = await _helper.GetMemoryStreamFile(thisTableFolder, objekFile.Id.Value.ToString());

        return new FileReady() { FileStream = null, FileName = "ContohFile" };
    }

    public async Task<string> NoImageBase64(string filename = "no-image.jpg", int sizeh = 60)
    {
        var sepp = _helper.separator();
        var folder = $"{_env.WebRootPath}{sepp}assets{sepp}images";
        var memoryStream = await _helper.GetMemoryStreamFile(folder, filename);
        return ChangeImageSize(sizeh, memoryStream);
    }

    public async Task<string> GenerateThumbnailBase64(string? fileName, int sizeh = 60)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return "";
        }

        try
        {
            // Mendapatkan MemoryStream file
            var memoryStream = await _helper.GetMemoryStreamFile(_thisTableFolder, fileName);
            return ChangeImageSize(sizeh, memoryStream);
        }
        catch (Exception)
        {
            return "";
        }
    }

    private static string ChangeImageSize(int sizeh, MemoryStream memoryStream)
    {
        // Decode gambar menggunakan SkiaSharp
        SKBitmap sourceBitmap = SKBitmap.Decode(memoryStream);

        // Hitung ukuran thumbnail
        int height = sizeh;
        decimal scaleFactor = sizeh / (decimal)sourceBitmap.Height;
        int width = (int)(sourceBitmap.Width * scaleFactor);
        SKBitmap scaledBitmap = sourceBitmap.Resize(new SKImageInfo(width, height), SKFilterQuality.Medium);
        SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);

        // Tentukan format encoding (misalnya PNG)
        var imageFormat = SKEncodedImageFormat.Png; // Default format
        SKData data = scaledImage.Encode(imageFormat, 100);

        // Tentukan MIME Type berdasarkan format encoding
        string mimeType = imageFormat switch
        {
            SKEncodedImageFormat.Jpeg => "jpeg",
            SKEncodedImageFormat.Png => "png",
            SKEncodedImageFormat.Webp => "webp",
            SKEncodedImageFormat.Gif => "gif",
            _ => "octet-stream" // Fallback jika format tidak dikenali
        };

        // Return base64 dengan MIME type yang sesuai
        return $"data:image/{mimeType};base64,{Convert.ToBase64String(data.ToArray())}";
    }

    private string? IsFileAllowed(IFormFile file)
    {
        // Memeriksa ekstensi file
        string fileExtension = Path.GetExtension(file.FileName);
        if (!_extensionAllowed.Contains(fileExtension.ToLower().Trim('.')))
        {
            return "Invalid file extension.";
        }

        // Memeriksa tipe konten file
        if (!_contentTypeAllowed.Contains(file.ContentType.ToLower()))
        {
            return "Invalid content type. Please check the file.";
        }

        // Ambil batas ukuran file dari konfigurasi
        long limitBytes = _sizeLimit * 1024 * 1024; //dikali jadi size megabyte
        if (file.Length > limitBytes)
        {
            return $"File size exceeds {_sizeLimit}MB limit";
        }

        return null;
    }

    private async Task<Guid> InsertToAttachmentList(IFormFile uploadedFile)
    {
        var newGuid = Guid.NewGuid();
        try
        {
            await _helper.IFormFileToPhysical(uploadedFile, _thisTableFolder, newGuid.ToString());
        }
        catch (Exception)
        {

            throw;
        }
        return newGuid;
    }

    private async Task<string> InsertToAttachment(IFormFile file, string? fileName = null)
    {
        var fileExtension = Path.GetExtension(file.FileName);
        var newGuid = Guid.NewGuid();
        
        fileName = string.IsNullOrEmpty(fileName) ? 
                    $"{newGuid.ToString()}{fileExtension}" :
                    $"{fileName}{fileExtension}";

        try
        {
            await _helper.IFormFileToPhysical(file, _thisTableFolder, fileName);
        }
        catch (Exception)
        {
            throw;
        }
        return fileName;
    }

    public MemoryStream ConvertBase64ToMemoryStream(string base64String)
    {
        try
        {
            // Menghapus prefix
            string base64Data = base64String.Substring(base64String.IndexOf(",") + 1);
            byte[] bytes = Convert.FromBase64String(base64Data);
            MemoryStream memoryStream = new(bytes);
            return memoryStream;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
