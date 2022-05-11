using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Product_Catalog_New.Services
{
    public class BlobService : IBlobService
    {
        private readonly IConfiguration _configuration;

        public BlobService(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        // Upload Methode für File Upload auf Blob Storage 
        public async Task<string> UploadAsync(Stream stream, string blobName, string fileType, string containerName)
        {
            string connectionString = _configuration["StorageConnectionString"];

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString,containerName);
            blobContainerClient.CreateIfNotExists();

            BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);

            BlobHttpHeaders blobHttpHeaders = new BlobHttpHeaders { ContentType = fileType };

            await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });



            return "Succesfull Upload";

        }
    }
}
