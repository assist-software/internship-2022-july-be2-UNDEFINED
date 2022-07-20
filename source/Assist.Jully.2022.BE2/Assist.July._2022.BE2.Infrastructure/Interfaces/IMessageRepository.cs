using Assist.July._2022.BE2.Domain.Entities;

namespace Assist.July._2022.BE2.Infrastructure.Interfaces
{
    public interface IMessageRepository
    {
        Task DeleteAllAsync(Guid listingId);
        Task DeleteAsync(Guid messageId);
        Task<IEnumerable<Message>> GetAllAsync(Guid listingId);
        Task PostAsync(Message request);
    }
}
