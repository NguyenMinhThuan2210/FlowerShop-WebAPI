using FlowerShop.Core.DTOs.ProductDTOs;
using FlowerShop.Core.Entities;
using FlowerShop.Core.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createDto)
        {
            var product = new Product
            {
                Name = createDto.Name,
                Price = createDto.Price,
                Stock = createDto.Stock,
                Description = createDto.Description,
                CategoryId = createDto.CategoryId,
            };
            _repository.Add(product);
            await _repository.SaveChangesAsync();
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = "Không xác định"
            };
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if(product == null) return false;
            _repository.Delete(product);
            return await _repository.SaveChangesAsync();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return null;
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = product.Category != null ? product.Category.Name : "Không xác định"
            };
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(ProductQueryParameters parameters)
        {
            var products = await _repository.GetProductsAsync(
                parameters.SearchTerm,
                parameters.PageIndex,
                parameters.PageSize
            );
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                Description = p.Description,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : "Không xác định"
            });
        }

        public async Task<bool> UpdateProductAsync(int id, UpdateProductDto updateDto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return false;
            product.Name = updateDto.Name;
            product.Price = updateDto.Price;
            product.Stock = updateDto.Stock;
            product.Description = updateDto.Description;
            product.CategoryId = updateDto.CategoryId;
            _repository.Update(product);
            return await _repository.SaveChangesAsync();
             
        }
    }
}
