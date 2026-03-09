using FlowerShop.Core.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoryAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createDto);
        Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto updateDto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
