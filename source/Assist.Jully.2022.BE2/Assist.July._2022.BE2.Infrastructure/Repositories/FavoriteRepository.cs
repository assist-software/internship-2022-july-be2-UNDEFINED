using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using Assist.July._2022.BE2.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assist.July._2022.BE2.Infrastructure.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public FavoriteRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Listing>> GetAllListingsByUserIdAsync(Guid userId)
        {
            var favoritesList = await applicationDbContext.Favorites.Where(x => x.Users.Id == userId).ToListAsync();
            
            if(!favoritesList.Any())
            {
                return null;
            }

            List<Listing> listingList = new List<Listing>();

            foreach(Favorite favorite in favoritesList)
            {
                listingList.Add(await applicationDbContext.Listings.FindAsync(favorite.Listings.Id));
            }

            if(!listingList.Any())
            {
                return null;
            }

            return listingList;
        }

        public async Task PostAsync(Favorite favorite)
        {
            applicationDbContext.Favorites.Add(favorite);

            await applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Favorite favoriteId)
        {
            applicationDbContext.Favorites.Remove(favoriteId);

            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<Favorite> GetFavoriteByIdAsync(Guid id)
        {
            var favorite = await applicationDbContext.Favorites.FindAsync(id);

            if(favorite == null)
            {
                return null;
            }

            return favorite;
        }
    }
}
