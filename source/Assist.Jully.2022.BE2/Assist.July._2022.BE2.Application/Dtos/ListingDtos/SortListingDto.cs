using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assist.July._2022.BE2.Application.Dtos.ListingDtos
{
    public class SortListingDto
    {
        public string? sortOrder { get; set; }
        public string? locationFilter { get; set; }
        public string? priceRange { get; set; }
        public string? searchString { get; set; }
        public string? category { get; set; }
        public string? page { get; set; }
        public string? pageSize { get; set; }
    }
}
