using FlowerShop.Core.DTOs.CategoryDTOs;
using FlowerShop.Core.Entities;
using FlowerShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Services
{
    public class CategoryService : ICategoryService
    {
        public readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoryAsync()
        {
            var category = await _repository.GetAllAsync();
            return category.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            });
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return null;
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createDto)
        {
            var category = new Category
            {
                Name = createDto.Name,
                Description = createDto.Description
            };
            _repository.Add(category);
            await _repository.SaveChangesAsync();
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto updateDto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return false;
            category.Name = updateDto.Name;
            category.Description = updateDto.Description;
            _repository.Update(category);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return false;
            _repository.Delete(category);
            return await _repository.SaveChangesAsync();
        }
    }
}
