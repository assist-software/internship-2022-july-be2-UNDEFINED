using Assist.July._2022.BE2.Application.Dtos.Blob;
using Microsoft.AspNetCore.Http;

namespace Assist.July._2022.BE2.Application.Interfaces
{
    public interface IAzureStorage
    { 
        Task<BlobResponse> UploadAsync(IFormFile file);
        Task<BlobFile> DownloadAsync(string blobFilename);

        Task UploadAsync64(string file);

        Task<BlobResponse> DeleteAsync(string blobFilename);
        Task<List<BlobFile>> ListAsync();
    }
}
