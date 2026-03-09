using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Entities
{
    public class Cart : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
