using Assist.July._2022.BE2.Application.Dtos.ListingDtos;
using Assist.July._2022.BE2.Domain.Entities;

namespace Assist.July._2022.BE2.Application.Interfaces
{
    public interface IListingService
    {
        Task AddAsync(PostListingRequestDto request);
        Task<IEnumerable<Listing>> GetAllListingsAsync();
        Task<IEnumerable<Listing>> GetSortedListingsAsync(SortListingDto sortListingDto);
        Task<Listing> PutListingAsync(PostListingRequestDto request, Guid id);
        Task<Listing> GetListingByIdAsync(Guid id);
        Task<Listing> DeleteListingAsync(Guid id);
    }
}
