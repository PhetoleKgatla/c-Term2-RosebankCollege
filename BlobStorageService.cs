
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace RaceDayAPI.Services
{
    public class BlobStorageService
    {
        private readonly IConfiguration _configuration;

        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadImageAsync(
            IFormFile file,
            string containerName)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Please select an image.");

            if (file.Length > 5 * 1024 * 1024)
                throw new Exception("Image must be smaller than 5 MB.");

            string contentType = file.ContentType.ToLowerInvariant();

            if (contentType != "image/jpeg" &&
                contentType != "image/png" &&
                contentType != "image/webp")
            {
                throw new Exception(
                    "Only JPG, PNG and WEBP images are allowed.");
            }

            string connectionString =
                _configuration["AzureBlobStorage:ConnectionString"]
                ?? throw new Exception("Azure connection string is missing.");

            var blobServiceClient =
                new BlobServiceClient(connectionString);

            var containerClient =
                blobServiceClient.GetBlobContainerClient(containerName);

            // Create container automatically if it does not exist.
            await containerClient.CreateIfNotExistsAsync(
                PublicAccessType.Blob);

            string extension = contentType switch
            {
                "image/jpeg" => ".jpg",
                "image/png" => ".png",
                "image/webp" => ".webp",
                _ => throw new Exception("Unsupported image.")
            };

            string fileName = Guid.NewGuid() + extension;

            var blobClient = containerClient.GetBlobClient(fileName);

            using var stream = file.OpenReadStream();

            await blobClient.UploadAsync(
                stream,
                new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = contentType
                    }
                });

            return blobClient.Uri.ToString();
        }
    }
}
