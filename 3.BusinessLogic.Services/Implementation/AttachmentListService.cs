using _2.BusinessLogic.Services.Interface;
using _5.Helpers.Consumer._Response;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Hosting;
using SkiaSharp;
using System.Buffers.Text;
using System.Diagnostics;
using System.Net.Mail;
using System.Runtime.InteropServices;

namespace _3.BusinessLogic.Services.Implementation;

public class AttachmentListService : IAttachmentListService
{
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _env;

    private readonly MethodHelperService _helper;
    private readonly IS3Service _s3Service;
    private readonly string _attachmentFolder;
    private string[] _extensionAllowed;
    private string[] _contentTypeAllowed;
    private long _sizeLimit;
    private string _thisTableFolder;
    private bool _isAttachToS3;
    public AttachmentListService(IConfiguration config, MethodHelperService helper, IWebHostEnvironment env, IS3Service s3Service)
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
        _s3Service = s3Service;
        bool isImageServiceResult;
        bool isImageService = bool.TryParse(config["UploadFileSetting:isAttachToS3"] ?? "false", out isImageServiceResult);
        _isAttachToS3 = isImageService ? isImageServiceResult : false;
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

            if (_isAttachToS3)
            {
                fileName = await UploadToS3(file, fileName);
            }
            else
            {
                fileName = await InsertToAttachment(file, fileName);
            }
        }

        return (fileName, null);
    }

    private async Task<string> UploadToS3(IFormFile file, string? fileName = null)
    {
        var fileExtension = Path.GetExtension(file.FileName);
        var newGuid = Guid.NewGuid();

        fileName = string.IsNullOrEmpty(fileName) ?
                    $"{newGuid}{fileExtension}" :
                    $"{fileName}{fileExtension}";

        try
        {
            using var stream = file.OpenReadStream(); // Convert IFormFile to Stream
            await _s3Service.UploadImageAsync(_thisTableFolder, fileName, stream, file.ContentType);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error uploading file to S3: {ex.Message}", ex);
        }

        return fileName;
    }

    public async Task DeleteFile(string fileName)
    {
        try
        {
            if (_isAttachToS3)
            {
                await _s3Service.DeleteImageAsync(_thisTableFolder, fileName);
            }
            else
            {
                await _helper.IDeleteFile(_thisTableFolder, fileName);
            }
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

    public async Task<string> NoImageBase64(string filename = "no-image.jpg", int sizeh = 100)
    {
        var sepp = _helper.separator();
        var folder = $"{_env.WebRootPath}{sepp}assets{sepp}images";
        var memoryStream = await _helper.GetMemoryStreamFile(folder, filename);
        return ChangeImageSize(sizeh, memoryStream);
    }
    public async Task<FileReady> ViewNoImage()
    {
        var base64 = await NoImageBase64();
        MemoryStream dataStream = ConvertBase64ToMemoryStream(base64);
        return new FileReady() { FileStream = dataStream, FileName = "Preview No Image" };
    }

    public async Task<string> GenerateThumbnailBase64(string? fileName, int sizeh = 60)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return "";
        }

        try
        {
            if (_isAttachToS3)
            {
                var imageUrl = _s3Service.GetPresignedUrl(_thisTableFolder, fileName, 1);
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);
                        using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                        {
                            return ChangeImageSize(sizeh, memoryStream);
                        }
                    }
                }
                return "";
            }
            else
            {
                // Mendapatkan MemoryStream file
                var memoryStream = await _helper.GetMemoryStreamFile(_thisTableFolder, fileName);
                return ChangeImageSize(sizeh, memoryStream);
            }

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

    public async Task<long> ProcessBase64ToBlob(string base64, string filename)
    {
        var byteFile = ExtractByte(base64);
        long result = byteFile.Length;
        validateLogo(byteFile);

        if (_isAttachToS3)
        {
            // Convert byte array to a memory stream for S3 upload
            using (var stream = new MemoryStream(byteFile))
            {
                await _s3Service.UploadImageAsync(_thisTableFolder, filename, stream, "image/png");
            }
        }
        else
        {
            await IByteToPhysical(byteFile, _thisTableFolder, filename);
        }

        return result;
    }


    public string GetFileExtensionFromBase64(string base64String)
    {
        // Memisahkan metadata dari data base64
        var data = base64String.Split(',');

        // Mengekstrak tipe MIME dari metadata
        var mimeTypeMatch = Regex.Match(data[0], @"data:(?<type>.+?);base64");

        if (!mimeTypeMatch.Success)
        {
            throw new ArgumentException("Invalid base64 string");
        }

        // Mendapatkan ekstensi file dari tipe MIME
        var mimeType = mimeTypeMatch.Groups["type"].Value;
        var extension = mimeType.Split('/')[1];

        return extension;
    }

    public byte[] ExtractByte(string base64)
    {
        var cleanbase64 = base64.Trim('\'').Replace("data:image/png;base64,", string.Empty);
        cleanbase64 = cleanbase64.Replace("data:image/jpg;base64,", string.Empty);
        cleanbase64 = cleanbase64.Replace("data:image/jpeg;base64,", string.Empty);

        byte[] ImageData = Convert.FromBase64String(cleanbase64);

        return ImageData;
    }

    public async Task IByteToPhysical(byte[] uploadedFile, string folder, string fileName)
    {
        var path = Path.Combine(
            folder, fileName);

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            exec("chmod 777 -R " + path);
        }

        await File.WriteAllBytesAsync(path, uploadedFile);
    }

    private void validateLogo(byte[] uploadedFile)
    {
        if (uploadedFile == null)
            throw new InvalidOperationException("File cannot be NULL");
        if (uploadedFile.Length <= 0)
            throw new InvalidOperationException("File cannot be Empty");
        if (uploadedFile.Length > 10000000)
            throw new InvalidOperationException("Logo Size is too Large");
    }

    private void exec(string cmd)
    {
        var escapedArgs = cmd.Replace("\\", "/");

        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "/bin/bash",
                Arguments = $"-c \"{escapedArgs}\""
            }
        };

        process.Start();
        process.WaitForExit();
    }

    public async Task<(string?, string?)> FileUploadToWWWroot(IFormFile? file, string folder, string fileName)
    {
        if (file != null && file.Length > 0)
        {
            var errMsg = IsFileAllowed(file);
            if (errMsg != null)
            {
                return (null, errMsg);
            }

            var sepp = _helper.separator();
            var endfolder = $"{_env.WebRootPath}{sepp}{folder}";
            await _helper.IFormFileToPhysical(file, endfolder, fileName);
        }

        return (fileName, null);
    }

}
