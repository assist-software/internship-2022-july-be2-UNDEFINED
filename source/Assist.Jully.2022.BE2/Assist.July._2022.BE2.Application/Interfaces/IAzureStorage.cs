using Assist.July._2022.BE2.Application.Dtos.Blob;
using Microsoft.AspNetCore.Http;

namespace Assist.July._2022.BE2.Application.Interfaces
{
    public interface IAzureStorage
    {
        Task<BlobFile> DownloadAsync(string blobFilename);

        Task<string> UploadAsync64(string file, string name);

        Task<BlobResponse> DeleteAsync(string blobFilename);
        Task<List<BlobFile>> ListAsync();
    }
}
