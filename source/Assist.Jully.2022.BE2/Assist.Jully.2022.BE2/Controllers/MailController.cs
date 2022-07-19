using Microsoft.AspNetCore.Mvc;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Application.Dtos.MailDtos;

[Route("api/[controller]"), ApiController]
public class MailController : ControllerBase
{
    private readonly IMailService MailService;
    public MailController(IMailService mailService)
    {
        MailService = mailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMail([FromForm] MailRequest request)
    {
        try
        {
            await MailService.SendEmailAsync(request);
            
            return new OkObjectResult("Ok");
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult("Error");
        }
    }
}
