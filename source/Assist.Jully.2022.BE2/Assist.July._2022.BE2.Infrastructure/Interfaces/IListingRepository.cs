using Assist.July._2022.BE2.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assist.July._2022.BE2.Infrastructure.Interfaces
{
    public interface IListingRepository
    {
        Task<IEnumerable<Listing>> GetAllAsync();
        Task AddAsync(Listing listing);
        Task PutAsync(Listing updatedListing);
        Task<Listing> GetByIdAsync(Guid id);
        Task DeleteAsync(Listing listing);
    }
}
