using Assist.July._2022.BE2.Application.Dtos.FavoriteDtos;
using Assist.July._2022.BE2.Application.Dtos.ListingDtos;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using Assist.July._2022.BE2.Infrastructure.Interfaces;
using AutoMapper;

namespace Assist.July._2022.BE2.Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private IFavoriteRepository favoriteRepo;
        private IUserRepository userRepository;
        private IListingRepository listingRepository;
        private IMapper mapper;

        public FavoriteService(IFavoriteRepository favoriteRepo, IMapper mapper, IUserRepository userRepository, IListingRepository listingRepository)
        {
            this.favoriteRepo = favoriteRepo;
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.listingRepository = listingRepository;
        }

        public async Task PostAsync(PostFavoriteDto request)
        {
            Favorite newFavorite = new Favorite();
            User user = await userRepository.GetByIdAsync(request.UserId);
            Listing listing = await listingRepository.GetByIdAsync(request.ListingId);
            newFavorite.Users = user;
            newFavorite.Listings = listing;

            await favoriteRepo.PostAsync(newFavorite);
        }
        public async Task<IEnumerable<Listing>> GetAsync(Guid userId)//imi returneaza o lista cu toate favoritele unui user
        {
            return await favoriteRepo.GetAllListingsByUserIdAsync(userId);
        }

        public async Task<Favorite> DeleteAsync(Guid id)
        {
            var dbFavorite = await favoriteRepo.GetFavoriteByIdAsync(id);

            if(dbFavorite == null)
            {
                return null;
            }

            await favoriteRepo.DeleteAsync(dbFavorite);

            return dbFavorite;
        }
    }
}
