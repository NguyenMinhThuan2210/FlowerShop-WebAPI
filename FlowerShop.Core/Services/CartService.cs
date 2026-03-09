using FlowerShop.Core.DTOs.CartDTOs;
using FlowerShop.Core.Entities;
using FlowerShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
       
        public CartService(ICartRepository cartRepo) => _cartRepo = cartRepo;
        public async Task<bool> AddItemToCartAsync(Guid userId, AddToCartDto dto)
        {
            var cart = await _cartRepo.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _cartRepo.AddCart(cart);
            }
            var existingItem = cart.CartItems.FirstOrDefault(i => i.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                });
            }

            return await _cartRepo.SaveChangesAsync();
        }

        public async Task<CartDto?> GetCartAsync(Guid userId)
        {
            var cart = await _cartRepo.GetCartByUserIdAsync(userId);
            if(cart == null) return new CartDto { UserId = userId };
            return new CartDto
            {
                UserId = cart.UserId,
                Items = cart.CartItems.Select(i => new CartItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Price = i.Product.Price,
                    Quantity = i.Quantity
                }).ToList()
            };
        }
        public async Task<bool> UpdateItemQuantityAsync(Guid userId, int productId, int quantity)
        {
            var cart = await _cartRepo.GetCartByUserIdAsync(userId);
            if (cart == null) return false;

            var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item == null) return false;

            if (quantity <= 0)
            {
                cart.CartItems.Remove(item); 
            }
            else
            {
                item.Quantity = quantity;
            }

            return await _cartRepo.SaveChangesAsync();
        }

        public async Task<bool> RemoveItemAsync(Guid userId, int productId)
        {
            var cart = await _cartRepo.GetCartByUserIdAsync(userId);
            if (cart == null) return false;

            var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.CartItems.Remove(item);
                return await _cartRepo.SaveChangesAsync();
            }
            return false;
        }
    }
}
