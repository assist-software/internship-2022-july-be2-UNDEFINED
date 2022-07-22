using Assist.July._2022.BE2.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace Assist.July._2022.BE2.Application.Dtos.ListingDtos
{
    public class PostListingRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Location { get; set; }
        public double Price { get; set; }
        public ListingStatus Status { get; set; }
        public string Images { get; set; }
        public string? Category { get; set; }
        public int ViewCounter { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
