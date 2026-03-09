using FlowerShop.Core.Entities;
using FlowerShop.Core.Interfaces;
using FlowerShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(AppUser user)
        {
           _context.AppUsers.Add(user);
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _context.AppUsers.AnyAsync(u => u.UserName == username);
        }

        public async Task<AppUser?> GetByUsernameAsync(string username)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
