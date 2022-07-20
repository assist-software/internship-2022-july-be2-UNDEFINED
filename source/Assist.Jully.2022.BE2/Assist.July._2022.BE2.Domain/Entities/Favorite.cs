namespace Assist.July._2022.BE2.Domain.Entities
{
    public record Favorite
    {
        public Guid Id;
        public Guid UserId { get; set; }
        public Guid ListingId { get; set; }

        public User? Users { get; set; }
        public Listing? Listings { get; set; }
    }
}
