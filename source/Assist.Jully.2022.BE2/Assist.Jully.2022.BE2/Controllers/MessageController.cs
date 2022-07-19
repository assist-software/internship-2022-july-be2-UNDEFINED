using Microsoft.AspNetCore.Mvc;
using Assist.July._2022.BE2.Application.Dtos.MessageDtos;
using Assist.July._2022.BE2.Application.Interfaces;


namespace Assist.Jully._2022.BE2.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class MessageController : Controller
    {
        private IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost("new")]
        public async Task <IActionResult> SendingNewMessage(PostMessageDto request)
        {
            try
            {
                await messageService.PostAsync(request);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpGet("{listingId}")]
        public async Task<IActionResult> GetAllMessages(Guid listingId)
        {
            try
            {
                var allMessages = await messageService.GetAllAsync(listingId);

                return new OkObjectResult(allMessages);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpDelete("deleteAll/{listingId}")]
        public IActionResult DeleteAllMessages(Guid listingId)
        {
            try
            {
                messageService.DeleteAllAsync(listingId);

                return new OkObjectResult("All messages have been deleted!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteMessage(Guid messageId)
        {
            try
            {
                await messageService.DeleteAsync(messageId);

                return new OkObjectResult("Message deleted!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}
