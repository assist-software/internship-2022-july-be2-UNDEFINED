using Assist.July._2022.BE2.Domain.Entities;

namespace Assist.July._2022.BE2.Infrastructure.Interfaces
{
    public interface IListingRepository
    {
        Task<IEnumerable<Listing>> GetAllAsync();
        Task<IEnumerable<Listing>> GetSortedAsync(string? sortOrder, string? locationFilter, string? priceRange, string? searchString, int pageNumber, int pageSize);
        Task AddAsync(Listing listing);
        Task PutAsync(Listing updatedListing);
        Task<Listing> GetByIdAsync(Guid id);
        Task DeleteAsync(Listing listing);
    }
}
