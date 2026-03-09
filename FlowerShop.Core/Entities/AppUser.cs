using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Entities
{
    public class AppUser
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool IsDeleted { get; set; } = false; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Role { get; set; } = "Customer";
        public virtual Cart? Cart { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
