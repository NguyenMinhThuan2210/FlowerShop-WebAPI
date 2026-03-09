using FlowerShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        void Add (Category category);
        void Update (Category category);
        void Delete (Category category);
        Task<bool> SaveChangesAsync();
    }
}
