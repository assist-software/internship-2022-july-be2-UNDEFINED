using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Infrastructure.Contexts;
using Assist.July._2022.BE2.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using PagedList.EntityFramework;

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
        public async Task<IEnumerable<Listing>> GetSortedAsync(int pageNumber, int pageSize)
        {
            int PageSize, PageNumber, PageCount, TotalItemCount;
            //bool HasPreviousPage, HasNextPage, IsFirstPage, IsLastPage;
            //int FirstItemOnPage, LastItemOnPage;

            //PageSize = pageSize;
            //PageNumber = pageNumber;
            TotalItemCount = await applicationDbContext.Listings.CountAsync();
            //PageCount = TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;
            //HasPreviousPage = PageNumber > 1;
            //HasNextPage = PageNumber < PageCount;
            //IsFirstPage = PageNumber == 1;
            //IsLastPage = PageNumber >= PageCount;
            //FirstItemOnPage = (PageNumber - 1) * PageSize + 1;
            //var num = FirstItemOnPage + PageSize - 1;
            //LastItemOnPage = num > TotalItemCount ? TotalItemCount : num;


            if (TotalItemCount <= 0)
                return null;
            return pageNumber == 1 ? await applicationDbContext.Listings.Skip(0).Take(pageSize).ToListAsync() : await applicationDbContext.Listings.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

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
