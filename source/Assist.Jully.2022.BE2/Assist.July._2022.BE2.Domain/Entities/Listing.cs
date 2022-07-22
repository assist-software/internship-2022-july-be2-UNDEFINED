﻿using Assist.July._2022.BE2.Domain.Enums;

namespace Assist.July._2022.BE2.Domain.Entities
{
    public record Listing
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? Location { get; set; }
        public double? Price { get; set; }
        public ListingStatus Status { get; set; }
        public string? Images { get; set; } 
        public string Category { get; set; }
        public int ViewCounter { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User? Author { get; set; }
        public Guid ApprovedBy { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]//-------------------------------------Temporary solution------------------------------
        public ICollection<Favorite>? FavoredBy { get; set; }
    }
}
