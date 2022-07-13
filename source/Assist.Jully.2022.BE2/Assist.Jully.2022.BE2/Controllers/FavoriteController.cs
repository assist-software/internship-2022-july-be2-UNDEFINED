using Microsoft.AspNetCore.Mvc;
namespace Assist.Jully._2022.BE2.Controllers
{
    [ApiController, Route("/api/[controller]"), Produces("application/json")]
    public class FavoriteController : ControllerBase
    {
        [HttpPost("{id}")]
        public IActionResult SaveFavoriteItem()
        {
            try
            {
                return new OkObjectResult("ok");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetFavorites()
        {
            try
            {
                return new OkObjectResult("ok");
            }
            catch (Exception)
            { 
                return BadRequest("An error has occured");
            }
        }
    }
}
