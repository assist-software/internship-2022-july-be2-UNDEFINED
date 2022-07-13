using Microsoft.AspNetCore.Mvc;
using AutoMapper;


namespace Assist.Jully._2022.BE2.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class ListingController : Controller
    {
        private IMapper mapper;

        [HttpGet("message")]
        public IActionResult GetMessage()
        {
            return new OkObjectResult("Listing Controller!");
        }

        [HttpPost("Create")]
        public IActionResult CreateNewListing()
        {
            return new OkObjectResult("Create new listing");
        }

        [HttpGet]
        public IActionResult GetAllListings()
        {
            return new OkObjectResult("Get all listings");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAListing()
        {
            return new OkObjectResult("Update a listing");
        }

        [HttpGet("{id}")]
        public IActionResult GetAListing()
        {
            return new OkObjectResult("Get a listing");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAListing()
        {
            return new OkObjectResult("Delete a listing");
        }
    }
}
