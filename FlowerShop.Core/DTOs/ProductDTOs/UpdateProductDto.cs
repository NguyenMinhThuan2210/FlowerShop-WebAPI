using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.DTOs.ProductDTOs
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
    }
}
