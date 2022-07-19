using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Application.Dtos.MessageDtos;
namespace Assist.July._2022.BE2.Application.Interfaces
{
    public interface IMessageService
    {
        Task PostAsync(PostMessageDto request);
        Task<IEnumerable<Message>> GetAllAsync(Guid listingId);
        Task DeleteAllAsync(Guid listingId);
        Task DeleteAsync(Guid messageId);
    }
}
