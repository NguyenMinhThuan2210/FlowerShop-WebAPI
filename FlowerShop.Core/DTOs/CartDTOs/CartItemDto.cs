using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.DTOs.CartDTOs
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity; 
    }
}
