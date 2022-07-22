using Assist.July._2022.BE2.Application.Dtos.Blob;
using Assist.July._2022.BE2.Application.Interfaces;
using Azure;
using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

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
        public async Task<string> UploadAsync64(string file,string name)
        {
        
            var blobHttpHeader=new BlobHttpHeaders();
            BlobResponse response = new BlobResponse();
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            
            if(file.Contains("png"))
            {
                file = file.Remove(0, 22);
                name += ".PNG";
                blobHttpHeader = new BlobHttpHeaders { ContentType = "image/png" };
            }

            if (file.Contains("jpg"))
            {
                file = file.Remove(0, 22);
                name += "jpg";
                blobHttpHeader = new BlobHttpHeaders { ContentType = "image/png" };
            }

            if (file.Contains("jpeg"))
            {
                file = file.Remove(0, 27);
                name += ".jpeg";
                blobHttpHeader = new BlobHttpHeaders { ContentType = "image/jpeg" };
            }
           
            if (isbase64(file) == false)
                return null;

            byte[] data = Convert.FromBase64String(file);
            BlobClient client = container.GetBlobClient(name);
            MemoryStream ms = new MemoryStream(data);
            await client.UploadAsync(ms, new BlobUploadOptions
            { HttpHeaders = blobHttpHeader });
            string link=client.Uri.Authority;
            link += client.Uri.LocalPath;
            return link;
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
        public static bool isbase64(string s)
        {
            s = s.Trim();

            return (s.Length%4==0)&&Regex.IsMatch(s, @"^[a-zA-Z0-9+/]*={0,3}$", RegexOptions.None);
        }

            return new BlobResponse { Error = false, Status = $"File: {blobFilename} has been successfully deleted." };
        }
    }
}
