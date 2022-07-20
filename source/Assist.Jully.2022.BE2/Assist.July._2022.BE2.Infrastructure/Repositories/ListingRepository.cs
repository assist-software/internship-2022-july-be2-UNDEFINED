using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using Assist.July._2022.BE2.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<Listing>> GetSortedAsync(string? sortOrder, string? locationFilter, string? priceRange, string? searchString, string? category, int pageNumber, int pageSize)
        {
            int TotalItemCount;
            string[] range = null;
            if (priceRange != null)
               range = priceRange.Split(" - ");

            TotalItemCount = await applicationDbContext.Listings.CountAsync();

            if (TotalItemCount <= 0)
            {
                return null;
            }

            if (sortOrder == "Cresc")
            {
                return await applicationDbContext.Listings
                    .Where(x => x.Location == (locationFilter ?? x.Location))
                    .Where(x => priceRange != null ? x.Price <= Int32.Parse(range[0]) && x.Price >= Int32.Parse(range[1]) : x.Price == x.Price)
                    .Where(x => x.Category == (category ?? x.Category))
                    .OrderBy(x => x.Price)
                    .Skip(pageNumber == 1 ? 0 : (pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            } else if(sortOrder == "Desc")
            {
                return await applicationDbContext.Listings
                    .Where(x => x.Location == (locationFilter ?? x.Location))
                    .Where(x => priceRange != null ? x.Price <= Int32.Parse(range[0]) && x.Price > Int32.Parse(range[1]) : x.Price == x.Price)
                    .Where(x => x.Category == (category ?? x.Category))
                    .OrderByDescending(x => x.Price)
                    .Skip(pageNumber == 1 ? 0 : (pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }else
                return await applicationDbContext.Listings
                    .Where(x => x.Location == (locationFilter ?? x.Location))
                    .Where(x => priceRange != null ? x.Price >= Int32.Parse(range[0]) && x.Price <= Int32.Parse(range[1]) : x.Price == x.Price)
                    //.Where(x => x.Category == (category ?? x.Category))
                    .Skip(pageNumber == 1 ? 0 : (pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

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
            {
                return null;
            }

            return listing;
        }
        public async Task DeleteAsync(Listing listing)
        {
            applicationDbContext.Listings.Remove(listing);

            await applicationDbContext.SaveChangesAsync();        
        }

    }
}
