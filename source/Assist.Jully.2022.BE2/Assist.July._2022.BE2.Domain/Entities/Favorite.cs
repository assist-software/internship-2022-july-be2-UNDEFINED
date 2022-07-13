using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assist.July._2022.BE2.Domain
{
    public record Favorite
    {
        public Guid? UserId { get; set; }
        public Guid? ListingId { get; set; }
    }
}
