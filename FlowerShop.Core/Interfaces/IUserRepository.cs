using FlowerShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsByUsernameAsync(string username);
        Task<AppUser?> GetByUsernameAsync(string username);
        void Add(AppUser user);
        Task<bool> SaveChangesAsync();
    }
}
