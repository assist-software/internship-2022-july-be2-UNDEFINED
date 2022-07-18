using Assist.July._2022.BE2.Application.Dtos.ListingDtos;
using Assist.July._2022.BE2.Application.Interfaces;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using AutoMapper;

namespace Assist.July._2022.BE2.Application.Services
{
    public class ListingService : IListingService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private IMapper mapper;

        public ListingService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }
        public void AddAsync(PostListingRequestDto request)
        {
            //Listing newListing = new Listing();
            Listing newListing = mapper.Map<Listing>(request);
            newListing.Id = Guid.NewGuid();
            
            
            applicationDbContext.Listings.Add(newListing);
            applicationDbContext.SaveChanges();
        }

        public ICollection<Listing> GetAllListings()
        {
            return applicationDbContext.Listings.ToList();
        }

        public void GetListingById(int id)
        {
            throw new NotImplementedException();
        }

        public void PutListing(ListingDto request)
        {
            throw new NotImplementedException();
        }

        public void DeleteListing()
        {
            throw new NotImplementedException();
        }
    }
}
