using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assist.July._2022.BE2.Domain.Entities
{
    public record Location
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public Guid listingId { get; set; }
        public virtual Listing listing { get; set; }
    }
}
