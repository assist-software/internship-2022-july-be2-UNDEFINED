using Assist.July._2022.BE2.Application.Dtos.Blob;
using Assist.July._2022.BE2.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assist.Jully._2022.BE2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IAzureStorage _storage;
        public StorageController(IAzureStorage storage)
        {
            _storage = storage;
        }
        [HttpGet(nameof(Get))]
        public async Task<IActionResult> Get()
        {
            List<BlobFile>? files = await _storage.ListAsync();
            return StatusCode(StatusCodes.Status200OK, files);
        }
        [HttpPost(nameof(Upload))]
        public async Task<IActionResult> Upload([FromBody]string file,string name)
        {
            await _storage.UploadAsync64(file,name);
            return new OkObjectResult("ok");
        }
        [HttpGet("{filename}")]
        public async Task<IActionResult> Download(string filename)
        {
            BlobFile? file = await _storage.DownloadAsync(filename);
            if (file == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"File {filename} could not be downloaded.");
            }
            else
            {
                return File(file.Content, file.ContentType, file.Name);
            }
        }
        [HttpDelete("filename")]
        public async Task<IActionResult> Delete(string filename)
        {
            BlobResponse response = await _storage.DeleteAsync(filename);
            if (response.Error == true)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, response.Status);
            }
        }
    }
}
