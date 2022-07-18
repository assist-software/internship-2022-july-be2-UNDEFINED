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

        [HttpPost("{listingId}/{userId}")]
        public IActionResult SendingNewMessage(Guid listingId, Guid userId)
        {
            try
            {
                messageService.PostAsynk(listingId, userId);
                return new OkObjectResult("Sending new message listing: "+ listingId + "user: " + userId);
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpPost("{test}")]
        public async Task <IActionResult> SendNewMessage(PostMessageDto request)
        {
            try
            {
                messageService.AddAsync(request);
                return new OkObjectResult("Async method done!");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpGet("{listingId}")]
        public IActionResult GetAllMessages(Guid listingId)
        {
            try
            {
                return new OkObjectResult("Get all messages for listingId: "+listingId);
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
                return new OkObjectResult("Delete all messages: "+listingId);
            }
            catch (Exception)
            {
                return BadRequest("An error has occured");
            }
        }
    }
}
