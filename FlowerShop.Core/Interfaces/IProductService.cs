using FlowerShop.Core.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(ProductQueryParameters parameters);
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateProductDto createDto);
        Task<bool> UpdateProductAsync(int id, UpdateProductDto updateDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
