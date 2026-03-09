using FlowerShop.Core.Entities;
using FlowerShop.Core.Interfaces;
using FlowerShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) 
        { 
            _context = context;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string? searchTerm, int pageIndex, int pageSize)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm));
            }
            return await query
                .OrderBy(p => p.Id) 
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(); 
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Product product)
        {
           _context.Products.Update(product);
        }
    }
}
