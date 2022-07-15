using Assist.July._2022.BE2.Application.Dtos.MailDtos;

namespace Assist.July._2022.BE2.Application.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
