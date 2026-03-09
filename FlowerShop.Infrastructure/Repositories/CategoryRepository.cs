using FlowerShop.Core.Entities;
using FlowerShop.Core.Interfaces;
using FlowerShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) 
        {
            _context = context;
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
