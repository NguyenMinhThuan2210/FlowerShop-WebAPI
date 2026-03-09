using FlowerShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(Guid userId);
        void AddCart(Cart cart);
        Task<bool> SaveChangesAsync();
    }
}
