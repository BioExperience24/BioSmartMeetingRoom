using _5.Helpers.Consumer._AWS;
using Amazon;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace _3.BusinessLogic.Services.Implementation
{
    public class S3Service : IS3Service
    {
        private readonly S3Helper _s3Helper;
        private readonly string SmrBucket = "pama-smr";

        public S3Service(IConfiguration _config)
        {
            _s3Helper = new S3Helper(
                    _config["AwsSetting:AccessId"],
                    _config["AwsSetting:SecretKey"],
                    RegionEndpoint.GetBySystemName(_config["AwsSetting:Region"])
                );
            SmrBucket = _config["AwsSetting:Bucket"];
        }

        /// <summary>
        /// Upload an image or QR code to SMR's S3 bucket.
        /// </summary>
        public async Task<bool> UploadImageAsync(string folder, string fileName, Stream fileStream, string contentType)
        {
            return await _s3Helper.UploadFileAsync(SmrBucket, folder, fileName, fileStream, contentType);
        }

        /// <summary>
        /// Get the public URL of a file (for publicly accessible files in pama-smr/qr).
        /// </summary>
        public string GetPublicFileUrl(string folder, string fileName)
        {
            return _s3Helper.GetFileUrl(SmrBucket, folder, fileName);
        }

        /// <summary>
        /// Download an image from SMR's S3 bucket.
        /// </summary>
        public async Task<bool> DownloadImageAsync(string folder, string fileName, string localPath)
        {
            return await _s3Helper.DownloadFileAsync(SmrBucket, folder, fileName, localPath);
        }

        /// <summary>
        /// Delete an image from SMR's S3 bucket.
        /// </summary>
        public async Task<bool> DeleteImageAsync(string folder, string fileName)
        {
            return await _s3Helper.DeleteFileAsync(SmrBucket, folder, fileName);
        }

        public string GetPresignedUrl(string folder, string fileName, int expiryMinutes = 60)
        {
            return _s3Helper.GeneratePresignedUrl(SmrBucket, folder, fileName, expiryMinutes);
        }

        public async Task<(Stream?, string?)> DownloadFileFromPresignedUrlAsync(string folder, string fileName, int expiryMinutes = 60)
        {
            string presignedUrl = GetPresignedUrl(folder, fileName, expiryMinutes);

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(presignedUrl);

                if (!response.IsSuccessStatusCode)
                {
                    return (null, "Failed to download the file from S3.");
                }

                var stream = await response.Content.ReadAsStreamAsync();
                string contentType = response.Content.Headers.ContentType?.ToString() ?? "application/octet-stream";

                return (stream, contentType);
            }
        }


    }
}
