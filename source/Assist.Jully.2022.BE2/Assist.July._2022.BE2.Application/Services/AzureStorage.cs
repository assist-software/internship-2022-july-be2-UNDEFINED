using Assist.July._2022.BE2.Application.Dtos.Blob;
using Assist.July._2022.BE2.Application.Interfaces;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace Assist.July._2022.BE2.Application.Services
{
    public class AzureStorage:IAzureStorage
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;
        private readonly ILogger<AzureStorage> _logger;
        public AzureStorage(IConfiguration configuration, ILogger<AzureStorage> logger)
        {
            _storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            _storageContainerName = configuration.GetValue<string>("BlobContainerName");
            _logger = logger;
        }
        public async Task<List<BlobFile>> ListAsync()
        {
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            List<BlobFile> files = new List<BlobFile>();
            await foreach (BlobItem file in container.GetBlobsAsync())
            {
                string uri = container.Uri.ToString();
                var name = file.Name;
                var fullUri = $"{uri}/{name}";
                files.Add(new BlobFile
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType
                });
            }
            return files;
        }
        public async Task<BlobResponse> UploadAsync(IFormFile blob)
        {
            BlobResponse response = new();
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            try
            {
               
                BlobClient client = container.GetBlobClient(blob.FileName);
                await using (Stream? data = blob.OpenReadStream())
                {
                    await client.UploadAsync(data);
                }
                response.Status = $"File {blob.FileName} Uploaded Successfully";
                response.Error = false;
                response.Blob.Uri = client.Uri.AbsoluteUri;
                response.Blob.Name = client.Name;
            }
            catch (RequestFailedException ex)
               when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                _logger.LogError($"File with name {blob.FileName} already exists in container. Set another name to store the file in the container: '{_storageContainerName}.'");
                response.Status = $"File with name {blob.FileName} already exists. Please use another name to store your file.";
                response.Error = true;
                return response;
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
                response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                response.Error = true;
                return response;
            }
            return response;
        }

        public async Task UploadAsync64(string file)
        {
            BlobResponse response = new BlobResponse();
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);


        }

        public async Task<BlobFile> DownloadAsync(string blobFilename)
        {
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            try
            {
                BlobClient file = client.GetBlobClient(blobFilename);
                if (await file.ExistsAsync())
                {
                    var data = await file.OpenReadAsync();
                    Stream blobContent = data;
                    var content = await file.DownloadContentAsync();
                    string name = blobFilename;
                    string contentType = content.Value.Details.ContentType;
                    return new BlobFile { Content = blobContent, Name = name, ContentType = contentType };
                }
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                _logger.LogError($"File {blobFilename} was not found.");
            }
            return null;
        }
        public async Task<BlobResponse> DeleteAsync(string blobFilename)
        {
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            BlobClient file = client.GetBlobClient(blobFilename);
            try
            {
                await file.DeleteAsync();
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                _logger.LogError($"File {blobFilename} was not found.");
                return new BlobResponse { Error = true, Status = $"File with name {blobFilename} not found." };
            }
            return new BlobResponse { Error = false, Status = $"File: {blobFilename} has been successfully deleted." };
        }
    }
}
