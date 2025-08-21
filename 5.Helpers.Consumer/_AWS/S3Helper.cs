using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace _5.Helpers.Consumer._AWS
{
    public class S3Helper
    {
        private readonly AmazonS3Client _s3Client;

        public S3Helper(string accessKey, string secretKey, RegionEndpoint region)
        {
            _s3Client = new AmazonS3Client(accessKey, secretKey, region);
        }

        /// <summary>
        /// Uploads a file to S3.
        /// </summary>
        public async Task<bool> UploadFileAsync(string bucketName, string folder, string fileName, Stream fileStream, string contentType)
        {
            try
            {
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = $"{folder}/{fileName}",
                    InputStream = fileStream,
                    ContentType = contentType
                };

                // console request
                Console.WriteLine($"Uploading file to S3: {request.BucketName}");

                var response = await _s3Client.PutObjectAsync(request);


                Console.WriteLine($"PutObject status code: {response.HttpStatusCode}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"S3 region: {_s3Client.Config.RegionEndpoint}");
                Console.WriteLine($"Upload failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gets the file URL (only for public files).
        /// </summary>
        public string GetFileUrl(string bucketName, string folder, string fileName)
        {
            return $"https://{bucketName}.s3.amazonaws.com/{folder}/{fileName}";
        }

        /// <summary>
        /// Downloads a file from S3 and saves it locally.
        /// </summary>
        public async Task<bool> DownloadFileAsync(string bucketName, string folder, string fileName, string localPath)
        {
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = $"{folder}/{fileName}"
                };

                using (var response = await _s3Client.GetObjectAsync(request))
                using (var fileStream = File.Create(localPath))
                {
                    await response.ResponseStream.CopyToAsync(fileStream);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Download failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Deletes a file from S3.
        /// </summary>
        public async Task<bool> DeleteFileAsync(string bucketName, string folder, string fileName)
        {
            try
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = $"{folder}/{fileName}"
                };

                await _s3Client.DeleteObjectAsync(request);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete failed: {ex.Message}");
                return false;
            }
        }

        public string GeneratePresignedUrl(string bucketName, string folder, string fileName, int expiryMinutes = 60)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = $"{folder}/{fileName}",
                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes)
            };

            return _s3Client.GetPreSignedURL(request);
        }

    }
}