using Assist.July._2022.BE2.Application.Dtos.ListingDtos;
using Assist.July._2022.BE2.Application.Helper;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using Assist.July._2022.BE2.Infrastructure.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace Assist.July._2022.BE2.Application.Services
{
    public class ListingService : IListingService
    {
        private IListingRepository listingRepo;
        private IMapper mapper;
        private IAzureStorage azure;
        private AzureSettings azureSettings;

        public ListingService(ApplicationDbContext applicationDbContext, IMapper mapper, IListingRepository listingRepo, IAzureStorage azure,
            IOptions<AzureSettings> azureSettings)
        {
            this.mapper = mapper;
            this.listingRepo = listingRepo;
            this.azure = azure;
            this.azureSettings = azureSettings.Value;
        }
        public async Task AddAsync(PostListingRequestDto request)
        {
            Listing newListing = mapper.Map<Listing>(request);
            newListing.Images = await azure.UploadAsync64(request.Images, newListing.Id.ToString());
            //if (response.Error == true)
            //{
            //    throw new KeyNotFoundException("Eroare la blob");
            //}

            await listingRepo.AddAsync(newListing);
        }
        public async Task<IEnumerable<Listing>> GetAllListingsAsync()
        {
            var listings = await listingRepo.GetAllAsync();

            if (!listings.Any())
            {
                throw new KeyNotFoundException("No Listings found");
            }

            return listings;
        }

        public async Task<IEnumerable<Listing>> GetSortedListingsAsync(SortListingDto sortListingDto)
        {
            int itemsPerPage = Int32.Parse(sortListingDto.pageSize ?? "10");
            int pageNumber = Int32.Parse(sortListingDto.page ?? "1");

            var listings = await listingRepo.GetSortedAsync(sortListingDto.sortOrder, sortListingDto.locationFilter, sortListingDto.priceRange, sortListingDto.searchString, sortListingDto.category, pageNumber, itemsPerPage);

            return listings;
        }
        public async Task<Listing> PutListingAsync(PostListingRequestDto request, Guid id)
        {
            var dbListing = await listingRepo.GetByIdAsync(id);

            if (dbListing == null)
            {
                return null;
            }

            mapper.Map(request, dbListing);
            dbListing.Images = await azure.UploadAsync64(request.Images, dbListing.Id.ToString());
            dbListing.UpdatedAt = DateTime.Now;
            await listingRepo.PutAsync(dbListing);

            return dbListing;
        }
        public async Task<Listing> GetListingByIdAsync(Guid id)
        {
            var dbListing = await listingRepo.GetByIdAsync(id);

            if (dbListing == null)
            {
                return null;
            }

            dbListing.Images += azureSettings.Key;

            return dbListing;
        }
        public async Task<Listing> DeleteListingAsync(Guid id)
        {
            var dbListing = await listingRepo.GetByIdAsync(id);

            if (dbListing == null)
            { 
                return null; 
            }

            await listingRepo.DeleteAsync(dbListing);

            return dbListing;
        }
    }
}
