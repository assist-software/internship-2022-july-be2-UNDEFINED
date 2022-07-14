namespace Assist.July._2022.BE2.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public byte Gender { get; set; }
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public Guid UserActivities { get; set; }
        public string? Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public byte Role { get; set; }
    }
    public class UserAccess
    {
        public string? Mail { get; set; }
        public string? Password { get; set; }
    }
}
