using Microsoft.AspNetCore.Mvc;

namespace Assist.Jully._2022.BE2.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class WelcomeController : Controller
    {
        [HttpGet("message")]
        public IActionResult GetWelcomeMessage()
        {
            return new OkObjectResult("Welcome to ASSIST Software !");
        }
    }
}
