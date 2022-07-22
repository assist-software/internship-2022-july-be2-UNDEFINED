using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using Assist.July._2022.BE2.Infrastructure.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Assist.July._2022.BE2.Infrastructure.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private IListingRepository listingRepository;

        public FavoriteRepository(ApplicationDbContext applicationDbContext, IListingRepository listingRepository)
        {
            this.applicationDbContext = applicationDbContext;
            this.listingRepository = listingRepository;
        }

        public async Task<IEnumerable<Listing>> GetAllListingsByUserIdAsync(Guid userId)
        {
            var favoritesList = await applicationDbContext.Favorites.Include(favorite => favorite.Users).Include(favorite => favorite.Listings).Where(favorite => favorite.Users.Id == userId).ToListAsync();

            List<Listing> listingList = new List<Listing>();

            foreach (var variable in favoritesList)
            {
                listingList.Add(await listingRepository.GetByIdAsync(variable.Listings.Id));
            }

            if (!listingList.Any())
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

        public async Task DeleteByUserAndListingIdAsync(Guid userId, Guid listingId)
        {
            var favoritesList = await applicationDbContext.Favorites.Include(favorite => favorite.Users).Include(favorite => favorite.Listings).ToListAsync();
            foreach (var variable in favoritesList)
            {
                if(variable.Listings.Id == listingId && variable.Users.Id == userId)
                {
                    applicationDbContext.Favorites.Remove(variable);
                }
            }
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
