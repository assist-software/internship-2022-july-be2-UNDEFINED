using Assist.July._2022.BE2.Application.Dtos.FavoriteDtos;
using Assist.July._2022.BE2.Domain.Entities;

namespace Assist.July._2022.BE2.Application.Interfaces
{
    public interface IFavoriteService
    {
        Task PostAsync(FavoriteDto request);
        Task <IEnumerable<Listing>>GetAsync(Guid userId);
        Task<Favorite> DeleteAsync(Guid id);
    }
}
