using FlowerShop.Core.DTOs.CartDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Interfaces
{
    public interface ICartService
    {
        Task<CartDto?> GetCartAsync(Guid userId);
        Task<bool> AddItemToCartAsync(Guid userId, AddToCartDto dto);
        Task<bool> UpdateItemQuantityAsync(Guid userId, int productId, int quantity);
        Task<bool> RemoveItemAsync(Guid userId, int productId);
    }
}
