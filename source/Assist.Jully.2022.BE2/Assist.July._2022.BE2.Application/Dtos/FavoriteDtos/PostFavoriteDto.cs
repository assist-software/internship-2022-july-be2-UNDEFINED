using Assist.July._2022.BE2.Domain.Entities;

namespace Assist.July._2022.BE2.Application.Dtos.FavoriteDtos
{
    public class PostFavoriteDto
    {
        public User? Users { get; set; }
        public Listing? Listings { get; set; }
    }
}
