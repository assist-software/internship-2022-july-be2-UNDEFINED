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

        public ListingController(IListingService listingService, ApplicationDbContext applicationDbContext)
        {
            this.listingService = listingService;
            this.applicationDbContext = applicationDbContext;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNewListing(PostListingRequestDto request)
        {
            try
            {
                await listingService.AddAsync(request);

                return Ok("{ 'message' : 'Listing Added' }");
            }
            catch(Exception)
            {
                return BadRequest("{ 'message' : 'An error has occured' }");
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
                return BadRequest("{ 'message' : 'An error has occured' }");
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
                return BadRequest("{ 'message' : 'An error has occured' }");
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
                    return NotFound("{ 'message' : 'Listing not found' }"); 
                }

                await applicationDbContext.SaveChangesAsync();

                return new OkObjectResult("{ 'message' : 'Update a listing' }");
            }
            catch (Exception)
            {
                return BadRequest("{ 'message' : 'An error has occured' }");
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
                return BadRequest("{ 'message' : 'An error has occured' }");
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

                return Ok("{ 'message' : 'Listing deleted' }");
            }
            catch (Exception)
            {
                return BadRequest("{ 'message' : 'An error has occured' }");
            }
        }
    }
}
