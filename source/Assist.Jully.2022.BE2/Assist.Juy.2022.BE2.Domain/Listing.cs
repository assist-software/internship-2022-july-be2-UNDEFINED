using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assist.Juy._2022.BE2.Domain
{
    public enum Category
    {
        category1,
        category2
    }
    public class Listing
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public decimal Price { get; set; }
        public Guid Author { get; set; }
        public Guid ApprovedBy { get; set; }
        public string Status { get; set; }
        [Required]
        public string Images { get; set; }
        public int ViewCounter { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
