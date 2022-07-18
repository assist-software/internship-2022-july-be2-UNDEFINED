using Microsoft.AspNetCore.Mvc;
using Assist.July._2022.BE2.Application.Dtos.MessageDtos;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Application.Dtos.MessageDtos;


namespace Assist.Jully._2022.BE2.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class MessageController : Controller
    {
        private IMessageService messageService;

        public MessageController(IMessageService _messageService)
        {
            messageService = _messageService;
        }

        [HttpPost("test")]
        public async Task <IActionResult> SendingNewMessage(PostMessageDto request)
        {
            try
            {
                await messageService.PostAsync(request);
                return new OkObjectResult("Async method done!");
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
                return new OkObjectResult("Get all messages for listingId: "+listingId+"----"+allMessages);
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }
        [HttpDelete("{listingId}")]
        public IActionResult DeleteAllMessages(Guid listingId)
        {
            try
            {
                messageService.DeleteAllAsync(listingId);
                return new OkObjectResult("Delete all messages: "+listingId);
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }
        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteMessage(Guid messageId)
        {
            try
            {
                await messageService.DeleteAsync(messageId);
                return new OkObjectResult("Delete one message, messageId: "+messageId);
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }
    }
}
