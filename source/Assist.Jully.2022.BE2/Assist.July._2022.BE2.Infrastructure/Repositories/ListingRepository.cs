using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using Assist.July._2022.BE2.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Assist.July._2022.BE2.Infrastructure.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ListingRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Listing>> GetAllAsync()
        {
            return await applicationDbContext.Listings.ToListAsync();
        }

        public async Task AddAsync(Listing listing)
        {
            applicationDbContext.Listings.Add(listing);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task PutAsync(Listing updatedListing)
        {
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<Listing> GetByIdAsync(Guid id)
        {
            var listing = await applicationDbContext.Listings.FindAsync(id);
            if (listing == null)
                return null;

            return listing;
        }
        public async Task DeleteAsync(Listing listing)
        {
            applicationDbContext.Listings.Remove(listing);
            await applicationDbContext.SaveChangesAsync();        
        }

    }
}
