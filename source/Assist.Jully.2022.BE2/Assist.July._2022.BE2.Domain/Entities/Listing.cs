using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public record Listing
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Location { get; set; }
        public double Price { get; set; }
        public Guid Author { get; set; }
        public Guid ApprovedBy { get; set; }
        public byte Status { get; set; }
        public string? Images { get; set; }
        public string? Category { get; set; }
        public int ViewCounter { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
