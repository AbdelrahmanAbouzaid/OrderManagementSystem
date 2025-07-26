

namespace Domain.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }

    }
}


public enum UserRole
{
    Admin,
    Customer
}