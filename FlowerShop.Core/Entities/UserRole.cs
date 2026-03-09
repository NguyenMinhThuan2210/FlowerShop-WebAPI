using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Entities
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public virtual AppUser User { get; set; } = null!;
        public virtual AppRole Role { get; set; } = null!;
    }
}
