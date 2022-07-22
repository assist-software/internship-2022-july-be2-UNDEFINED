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

        public async Task<FavoriteDto> PostAsync(FavoriteDto request)
        {
            Favorite newFavorite = new Favorite();
            User user = await userRepository.GetByIdAsync(request.UserId);
            Listing listing = await listingRepository.GetByIdAsync(request.ListingId);
            newFavorite.Users = user;
            newFavorite.Listings = listing;

            FavoriteDto response = new FavoriteDto();
            response.ListingId = request.ListingId;
            response.UserId = request.UserId;

            await favoriteRepo.PostAsync(newFavorite); 
            
            return response;
        }
        public async Task<IEnumerable<Listing>> GetAsync(Guid userId)
        {
            return await favoriteRepo.GetAllListingsByUserIdAsync(userId);
        }

        public async Task DeleteAsync(Guid id)
        {
            var dbFavorite = await favoriteRepo.GetFavoriteByIdAsync(id);

            await favoriteRepo.DeleteAsync(dbFavorite);
        }

        public async Task DeleteByUserAndListingIdAsync(Guid userId, Guid listingId)
        {
            await favoriteRepo.DeleteByUserAndListingIdAsync(userId, listingId);
        }
    }
}
