using FlowerShop.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public int? VoucherId { get; set; }
        public virtual AppUser User { get; set; } = null!;
        public virtual Voucher? Voucher { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }
}
