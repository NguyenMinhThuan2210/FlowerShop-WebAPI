using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.DTOs.ProductDTOs
{
    public class ProductQueryParameters
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }
}
