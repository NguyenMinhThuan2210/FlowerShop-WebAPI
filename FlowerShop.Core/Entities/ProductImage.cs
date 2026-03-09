using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Entities
{
    public class ProductImage : BaseEntity 
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsMain { get; set; } 
        public virtual Product? Product { get; set; }
    }
}
