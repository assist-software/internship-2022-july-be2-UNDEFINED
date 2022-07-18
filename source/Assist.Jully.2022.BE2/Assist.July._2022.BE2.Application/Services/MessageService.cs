using Assist.July._2022.BE2.Application.Dtos.MessageDtos;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Assist.July._2022.BE2.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private IMapper mapper;
        public MessageService(ApplicationDbContext _applicationDbContext, IMapper _mapper)
        {
            this.applicationDbContext = _applicationDbContext;
            this.mapper = _mapper;
        }

        public async Task DeleteAllAsync(Guid listingId)
        {
            var dbMessages = applicationDbContext.Messages.Where(x => x.ListingId == listingId).ToList();

            if(!dbMessages.Any())
            {
                throw new KeyNotFoundException("Not found");
            }
            foreach(var dbMessage in dbMessages)
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
                await applicationDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Message>> GetAllAsync(Guid listingId)
        {
            var dbMessages = await applicationDbContext.Messages.Where(x => x.ListingId == listingId).ToListAsync();
        
            if (!dbMessages.Any())
            {
                throw new KeyNotFoundException("Not found");
            }
            else
            {
                return dbMessages;
            }
        }

        public async Task PostAsync(PostMessageDto request)
        {
            Message newMessage = mapper.Map<Message>(request);
            newMessage.Id = Guid.NewGuid();

            applicationDbContext.Messages.Add(newMessage);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
