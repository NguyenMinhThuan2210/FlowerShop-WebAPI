using FlowerShop.Core.Entities;
using FlowerShop.Core.Interfaces;
using FlowerShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context) 
        {
            _context = context;
        }
        public void AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
        {
            return await _context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >0 ;
        }
    }
}
