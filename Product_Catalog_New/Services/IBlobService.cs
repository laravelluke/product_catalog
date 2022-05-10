using Azure.Storage.Blobs;

namespace Product_Catalog_New.Services
{
    public interface IBlobService
    {
        Task<string> UploadAsync(Stream stream, string blobName, string fileType, string containerName);
    }
}
