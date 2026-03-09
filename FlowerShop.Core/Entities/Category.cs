using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public virtual ICollection<Product> Products { get; set;} = new List<Product>();
    }
}
