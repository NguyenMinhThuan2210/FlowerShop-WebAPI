using FlowerShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(string? searchTerm, int pageIndex, int pageSize);
        Task<Product?> GetByIdAsync(int id);
        void Add(Product product); 
        void Update(Product product);
        void Delete(Product product);
        Task<bool> SaveChangesAsync();

    }
}
