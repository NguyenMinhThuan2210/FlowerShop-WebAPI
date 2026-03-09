using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.DTOs.CategoryDTOs
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
