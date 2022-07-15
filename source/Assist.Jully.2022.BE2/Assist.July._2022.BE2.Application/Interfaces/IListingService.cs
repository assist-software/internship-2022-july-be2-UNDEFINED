using Assist.July._2022.BE2.Application.Dtos.ListingDtos;
using Assist.July._2022.BE2.Domain.Entities;

namespace Assist.July._2022.BE2.Application.Interfaces
{
    public interface IListingService
    {
        // conventie de nume : AddAsync , GetByIdAsync , DeleteAsync.
        void PostNewListing(PostListingRequestDto entity); // create listing
        ICollection<Listing> GetAllListings(); // return all listings
        void GetListingById(int id);  // return listing by id
        void PutListing(ListingDto entity); // update listing
        void DeleteListing(); // delete listing
        
    }
}
