using Assist.July._2022.BE2.Domain;


namespace Assist.July._2022.BE2.Infrastructure
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }  
}
