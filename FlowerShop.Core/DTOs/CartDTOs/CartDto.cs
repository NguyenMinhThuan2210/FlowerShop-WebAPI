using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.DTOs.CartDTOs
{
    public class CartDto
    {
        public Guid UserId { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
        public decimal GrandTotal => Items.Sum(x => x.TotalPrice);
    }
}
