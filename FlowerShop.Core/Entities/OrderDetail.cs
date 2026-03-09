using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
