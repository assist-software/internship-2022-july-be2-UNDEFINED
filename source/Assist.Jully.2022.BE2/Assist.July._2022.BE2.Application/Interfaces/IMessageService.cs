using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Application.Dtos.MessageDtos;
namespace Assist.July._2022.BE2.Application.Interfaces
{
    public interface IMessageService
    {
        Task PostAsync(PostMessageDto request);
        Task<IEnumerable<MessageDto>> GetAllAsync(Guid listingId);//get all messages from a listing(need the id)
        Task DeleteAllAsync(Guid listingId);//delete all messages from a listing using the listing id
        Task DeleteAsync(Guid messageId);//delete a single message from a post
        
    }
}
