using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Entities
{
    public class Voucher :BaseEntity
    {
        public string Code { get; set; } = string.Empty; 
        public decimal DiscountPercent { get; set; } 
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
