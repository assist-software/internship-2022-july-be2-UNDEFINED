using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Application.Dtos.MessageDtos;
namespace Assist.July._2022.BE2.Application.Interfaces
{
    public interface IMessageService
    {
        void PostAsync(Guid listingId, Guid userId);//create new message on a listing(need the id) by an user(need the id) send notification to receiverId!!!
        IEnumerable<MessageDto> GetAllAsync(Guid listingId);//get all messages from a listing(need the id)
        void DeleteAllAsync(Guid listingId);//delete all messages from a listing using the listing id
        //-------------------
        void DeleteAsync(Guid messageId);//delete a single message from a post
        Task AddAsync(PostMessageDto request);
    }
}
