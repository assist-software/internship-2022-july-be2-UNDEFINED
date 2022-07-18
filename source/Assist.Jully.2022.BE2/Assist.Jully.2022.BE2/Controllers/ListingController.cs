using Assist.July._2022.BE2.Application.Dtos.ListingDtos;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Assist.Jully._2022.BE2.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class ListingController : Controller
    {
        private IListingService listingService;
        //private readonly IMapper mapper;

        public ListingController(IListingService listingService, IMapper mapper)
        {
            //this.mapper = mapper;
            this.listingService = listingService;
        }

        [HttpPost("create")]
        public IActionResult CreateNewListing(PostListingRequestDto request)
        {
            try
            {
                listingService.AddAsync(request);
                return new OkObjectResult("Created new listing");
            }
            catch(Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpGet]
        public IActionResult GetAllListings()
        {
            try
            {
                return new OkObjectResult(listingService.GetAllListings());
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAListing()
        {
            try
            {
                return new OkObjectResult("Update a listing");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAListing()
        {
            try
            {
                return new OkObjectResult("Get a listing");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAListing()
        {
            try
            {
                return new OkObjectResult("Delete a listing");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }
    }
}
