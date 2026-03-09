using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.DTOs.CategoryDTOs
{
    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
