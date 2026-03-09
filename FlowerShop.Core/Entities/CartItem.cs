using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Entities
{
    public class CartItem : BaseEntity
    {
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public virtual Cart Cart { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
