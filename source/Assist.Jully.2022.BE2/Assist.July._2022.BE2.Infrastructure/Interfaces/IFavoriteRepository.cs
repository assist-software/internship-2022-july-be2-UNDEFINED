using Assist.July._2022.BE2.Domain.Entities;

namespace Assist.July._2022.BE2.Infrastructure.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<Listing>> GetAllListingsByUserIdAsync(Guid userId);
        Task PostAsync(Favorite favorite);
        Task DeleteAsync(Favorite favoriteId);
        Task <Favorite> GetFavoriteByIdAsync(Guid id);
    }
}
