using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Entities
{
    public class AppRole : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
