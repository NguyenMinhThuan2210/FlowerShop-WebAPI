using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.DTOs.OrderDTOs
{
    public class CheckoutDto
    {
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? VoucherCode { get; set; }
    }
}
