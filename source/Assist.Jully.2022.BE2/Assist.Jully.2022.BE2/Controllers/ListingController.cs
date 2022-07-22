using Assist.July._2022.BE2.Application.Dtos.Blob;
using Assist.July._2022.BE2.Application.Dtos.ListingDtos;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Assist.Jully._2022.BE2.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class ListingController : Controller
    {
        private IListingService listingService;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IAzureStorage _storage;

        public ListingController(IListingService listingService, ApplicationDbContext applicationDbContext, IAzureStorage storage)
        {
            this.listingService = listingService;
            this.applicationDbContext = applicationDbContext;
            _storage = storage;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNewListing( IFormFile file)
        {
            try
            {
                //await listingService.AddAsync(request);
                BlobResponse? response = await _storage.UploadAsync(file);



                return Ok("Listing Added");
            }
            catch(Exception)
            {
                return BadRequest("An error has occured");
            }
        }
        [HttpPost("create2")]
        public async Task<IActionResult> CreateNewListing2(PostListingRequestDto request)
        {
            try
            {
                await listingService.AddAsync(request);
                BlobResponse? response = await _storage.UploadAsync(request.Images);



                return Ok("Listing Added");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }
        [HttpPost("create3")]
        public async Task<IActionResult> CreateNewListing3(PostListingRequestDto request, IFormFile file)
        {
            try
            {
                await listingService.AddAsync(request);
                BlobResponse? response = await _storage.UploadAsync(request.Images);



                return Ok("Listing Added");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Listing>>> GetAllListings()
        {
            try
            {
                var response = await listingService.GetAllListingsAsync();

                if (response == null)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }
        [HttpGet("sort")]
        public async Task<ActionResult<List<Listing>>> GetSortedListingsAsync([FromQuery] SortListingDto sortListingDto)
        {
            try
            {
                var response = await listingService.GetSortedListingsAsync(sortListingDto);

                if (response == null)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateListing([FromBody]PostListingRequestDto request, Guid id)
        {
            try
            {
                var response = await listingService.PutListingAsync(request, id);

                if (response == null)
                { 
                    return NotFound(); 
                }

                await applicationDbContext.SaveChangesAsync();

                return new OkObjectResult("Update a listing");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAListing(Guid id)
        {
            try
            {
                var response = await listingService.GetListingByIdAsync(id);

                if (response == null)
                { 
                    return NotFound(); 
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAListing(Guid id)
        {
            try
            {
                var response = await listingService.DeleteListingAsync(id);

                if (response == null)
                { 
                    return NotFound(); 
                }

                return Ok("Listing deleted");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }
    }
}
