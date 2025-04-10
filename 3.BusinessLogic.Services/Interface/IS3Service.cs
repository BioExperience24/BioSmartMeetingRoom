namespace _3.BusinessLogic.Services.Interface;

public interface IS3Service
{
    Task<bool> UploadImageAsync(string folder, string fileName, Stream fileStream, string contentType);
    string GetPublicFileUrl(string folder, string fileName);
    Task<bool> DownloadImageAsync(string folder, string fileName, string localPath);
    Task<bool> DeleteImageAsync(string folder, string fileName);
    string GetPresignedUrl(string folder, string fileName, int expiryMinutes = 60);
    Task<(Stream?, string?)> DownloadFileFromPresignedUrlAsync(string folder, string fileName, int expiryMinutes = 60);
}