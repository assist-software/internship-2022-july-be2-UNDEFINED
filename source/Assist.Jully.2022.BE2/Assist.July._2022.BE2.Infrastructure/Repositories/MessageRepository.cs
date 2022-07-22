using System;
using Microsoft.EntityFrameworkCore;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Interfaces;

namespace Assist.July._2022.BE2.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public MessageRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task DeleteAllAsync(Guid listingId)
        {
            var dbMessages = applicationDbContext.Messages.Where(x => x.ListingId == listingId).ToList();

            if (!dbMessages.Any())
            {
                throw new KeyNotFoundException("Not found");
            }
            foreach (var dbMessage in dbMessages)
            {
                applicationDbContext.Messages.Remove(dbMessage);
            }

            await applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid messageId)
        {
            var dbMessage = applicationDbContext.Messages.Find(messageId);

            if (dbMessage == null)
            {
                throw new KeyNotFoundException("Not found");
            }
            else
            {
                applicationDbContext.Messages.Remove(dbMessage);
            }

            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetAllAsync(Guid listingId)
        {
            return await applicationDbContext.Messages.Where(x => x.ListingId == listingId).ToListAsync();
        }

        public async Task PostAsync(Message request)
        {
            applicationDbContext.Messages.Add(request);

            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetAllByListingReceiverAndSenderId(Guid senderId, Guid receiverId, Guid listingId)
        {
            return await applicationDbContext.Messages.Where(x => x.ListingId == listingId && (x.SenderId == senderId || x.SenderId == receiverId) && (x.ReceiverId == receiverId || x.ReceiverId == senderId)).ToListAsync();
        }
    }
}
