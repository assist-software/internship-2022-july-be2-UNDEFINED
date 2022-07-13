using Microsoft.AspNetCore.Mvc;

namespace Assist.Jully._2022.BE2.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class MessageController : Controller
    {
        [HttpPost("{listingId}/{userId}")]
        public IActionResult SendingNewMessage()
        {
            try
            {
                return new OkObjectResult("Sending new message");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }
        [HttpGet("{listingId}")]
        public IActionResult GetAllMessages()
        {
            try
            {
                return new OkObjectResult("Get all messages");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }
        [HttpDelete("{listingId}")]
        public IActionResult DeleteAllMessages()
        {
            try
            {
                return new OkObjectResult("Delete all messages");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }
    }
}
