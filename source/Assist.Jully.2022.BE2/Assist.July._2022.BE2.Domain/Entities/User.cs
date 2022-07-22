using Assist.July._2022.BE2.Domain.Enums;

namespace Assist.July._2022.BE2.Domain.Entities
{
    public record User
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Gender Gender { get; set; }
        public string? Phone { get; set; }
        public Role Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public UserActivity? UserActivities { get; set; }
        public string? Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        

        public ICollection<Listing>? Listings { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
    }
}
