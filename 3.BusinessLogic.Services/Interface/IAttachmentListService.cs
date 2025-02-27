namespace _2.BusinessLogic.Services.Interface;

public interface IAttachmentListService
{
    string SetTableFolder(string folder);
    string[] SetExtensionAllowed(string extension);
    string[] SetTypeAllowed(string type);
    long SetSizeLimit(long size);
    Task<ReturnalModel> ProcessFileUploadToBlob(IFormFile uploadedFile);
    Task<(string?, string?)> FileUploadProcess(IFormFile? file, string? fileName = null);
    Task DeleteFile(string fileName);
    Task<FileReady> DownloadFile(Guid id);
    Task<string> GenerateThumbnailBase64(string? fileName, int sizeh = 60);
    Task<string> NoImageBase64(string filename = "no-image.jpg", int sizeh = 100);
    Task<FileReady> ViewNoImage();
    MemoryStream ConvertBase64ToMemoryStream(string base64String);

    Task<long> ProcessBase64ToBlob(string base64, string filename);
}