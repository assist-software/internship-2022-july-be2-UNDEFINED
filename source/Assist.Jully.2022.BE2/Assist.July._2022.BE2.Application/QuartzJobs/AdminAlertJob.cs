using Microsoft.Extensions.Logging;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Application.Interfaces;
using Quartz;
using Assist.July._2022.BE2.Application.Dtos.MailDtos;

namespace Assist.July._2022.BE2.Application.QuartzJobs
{
    [DisallowConcurrentExecution]
    public class AdminAlertJob : IJob
    {
        private readonly ILogger<AdminAlertJob> _logger;
        private List<Listing> listOfPendingListings = new List<Listing>();
        private IListingService listingService;
        private  IMailService mailService;
        private const string AdminEmailAdress = "pisicicainii1@gmail.com";

        public AdminAlertJob(ILogger<AdminAlertJob> logger, IMailService mailService, IListingService  listingService)
        {
            _logger = logger;
            this.mailService = mailService;
            this.listingService = listingService;
        }

        async Task SendEmailNotification(String listingName, string email)
        {
            MailRequest MailToSend = new MailRequest();
            MailToSend.ToEmail = email;
            MailToSend.Subject = "Validate Listing!";
            MailToSend.Body = "Validate: " + listingName;
            await mailService.SendEmailAsync(MailToSend);
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var ListOfListings = await listingService.GetAllListingsAsync();

            listOfPendingListings.Clear();

            foreach (var listing in ListOfListings)
            {
                if (listing.Status == 0)
                {
                    listOfPendingListings.Add(listing);
                }
            }

            foreach (var listing in listOfPendingListings)
            {
                if ((DateTime.Now - listing.CreatedAt).Days >= 2)
                {
                    await SendEmailNotification(listing.Title, AdminEmailAdress);

                    _logger.LogInformation("--------------------------------------An email has been sent: " + DateTime.UtcNow + "For timespan: " + (DateTime.Now - listing.CreatedAt).Days);
                }
            }
        }
    }
}
