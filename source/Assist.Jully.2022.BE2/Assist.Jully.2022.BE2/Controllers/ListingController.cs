using Assist.July._2022.BE2.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Assist.Jully._2022.BE2.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class ListingController : Controller
    {
        [HttpPost("create")]
        public IActionResult CreateNewListing()
        {
            try
            {
                return new OkObjectResult("Create new listing");
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
                return new OkObjectResult("Get all listings");
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
