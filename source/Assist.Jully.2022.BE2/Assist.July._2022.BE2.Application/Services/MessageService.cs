using Assist.July._2022.BE2.Application.Dtos.MessageDtos;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using AutoMapper;

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

        public void DeleteAllAsync(Guid listingId)
        {
            var dbMessages = applicationDbContext.Messages.Where(x => x.ListingId == listingId).ToList();
            foreach(var dbMessage in dbMessages)
            {
                applicationDbContext.Messages.Remove(dbMessage);
            }
            applicationDbContext.SaveChanges();
        }

        public void DeleteAsync(Guid messageId)
        {
            var dbMessage = applicationDbContext.Messages.Find(messageId);
            if (dbMessage == null)
            {
                throw new KeyNotFoundException("Not found");
            }
            else
            {
                applicationDbContext.Messages.Remove(dbMessage);
                applicationDbContext.SaveChanges();
            }
        }

        public IEnumerable<MessageDto> GetAllAsync(Guid listingId)
        {
            var dbMessages = applicationDbContext.Messages.Where(x => x.ListingId == listingId).ToList<Message>();
            
            if (dbMessages == null)
            {
                return null;
            }
            else
            {
                return (IEnumerable<MessageDto>)dbMessages;
            }
        }

        public void PostAsync(Guid listingId, Guid userId)
        {

        }

        public async Task AddAsync(PostMessageDto request)
        {
            Message newMessage = mapper.Map<Message>(request);
            newMessage.Id = Guid.NewGuid();

            applicationDbContext.Messages.Add(newMessage);
            await applicationDbContext.SaveChangesAsync();
        }
        
    }
}
