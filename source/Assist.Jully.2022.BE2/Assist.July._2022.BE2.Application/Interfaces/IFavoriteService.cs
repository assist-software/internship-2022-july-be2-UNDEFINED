using Assist.July._2022.BE2.Application.Dtos.FavoriteDtos;
using Assist.July._2022.BE2.Domain.Entities;

namespace Assist.July._2022.BE2.Application.Interfaces
{
    public interface IFavoriteService
    {
        Task<FavoriteDto> PostAsync(FavoriteDto request);
        Task <IEnumerable<Listing>>GetAsync(Guid userId);
        Task DeleteAsync(Guid id);
        Task DeleteByUserAndListingIdAsync(Guid userId, Guid listingId);
    }
}
