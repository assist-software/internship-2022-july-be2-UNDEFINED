using Assist.July._2022.BE2.Application.Dtos.MessageDtos;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Interfaces;
using AutoMapper;

namespace Assist.July._2022.BE2.Application.Services
{
    public class MessageService : IMessageService
    {
        private IMessageRepository messageRepository;
        private IMapper mapper;

        public MessageService(IMapper mapper, IMessageRepository messageRepository)
        {
            this.mapper = mapper;
            this.messageRepository = messageRepository;
        }

        public async Task DeleteAllAsync(Guid listingId)
        {
            await messageRepository.DeleteAllAsync(listingId);
        }

        public async Task DeleteAsync(Guid messageId)
        {
            await messageRepository.DeleteAsync(messageId);
        }

        public async Task<IEnumerable<Message>> GetAllAsync(Guid listingId)
        {
            var dbMessages = await messageRepository.GetAllAsync(listingId);

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
            if(request.Content == null)
            {
                throw new ArgumentNullException(nameof(request.Content));
            }

            Message newMessage = mapper.Map<Message>(request);

            await messageRepository.PostAsync(newMessage);
        }
    }
}
