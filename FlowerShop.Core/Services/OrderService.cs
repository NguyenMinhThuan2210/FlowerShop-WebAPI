using FlowerShop.Core.DTOs.OrderDTOs;
using FlowerShop.Core.Entities;
using FlowerShop.Core.Enums;
using FlowerShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;
        private readonly IVoucherRepository _voucherRepo;

        public OrderService(IOrderRepository orderRepo,ICartRepository cartRepo,IProductRepository productRepo, IVoucherRepository voucherRepo)
        {
            _orderRepo = orderRepo;
            _cartRepo = cartRepo;
            _productRepo = productRepo;
            _voucherRepo = voucherRepo;
        }

        public async Task<string?> CheckoutAsync(Guid userId, CheckoutDto dto)
        {
            var cart = await _cartRepo.GetCartByUserIdAsync(userId);
            if (cart == null || !cart.CartItems.Any())
                return "Giỏ hàng của bạn đang trống";
            //begin transaction
            await _orderRepo.BeginTransactionAsync();

            try
            {
                decimal totalAmount = 0;
                var orderDetails = new List<OrderDetail>();
                foreach(var item in cart.CartItems)
                {
                    var product  = await _productRepo.GetByIdAsync(item.ProductId);
                    if(product == null)
                        throw new Exception($"Sản phẩm {item.ProductId} không tồn tại.");

                    if(product.Stock < item.Quantity)
                        throw new Exception($"Hoa '{product.Name}' không đủ số lượng (Còn {product.Stock}).");

                    totalAmount += product.Price * item.Quantity;
                    product.Stock -= item.Quantity;
                    _productRepo.Update(product);

                    orderDetails.Add(new OrderDetail
                    {
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price, 
                        CreatedAt = DateTime.UtcNow
                    }); 
                }
                int? appliedVoucherId = null;

                if (!string.IsNullOrWhiteSpace(dto.VoucherCode))
                {
                    var voucher = await _voucherRepo.GetByCodeAsync(dto.VoucherCode);

                    if (voucher == null || !voucher.IsActive || voucher.ExpiryDate < DateTime.UtcNow)
                    {
                        throw new Exception("Mã giảm giá không hợp lệ hoặc đã hết hạn!");
                    }

                    var discountAmount = totalAmount * (voucher.DiscountPercent / 100);
                    totalAmount -= discountAmount;

                    appliedVoucherId = voucher.Id;
                }
                var newOrder = new Order
                {
                    UserId = userId,
                    Address = dto.Address,
                    Phone = dto.Phone,
                    TotalAmount = totalAmount,
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending,
                    OrderDetails = orderDetails,
                    CreatedAt = DateTime.UtcNow
                };

                _orderRepo.AddOrder(newOrder);
                cart.CartItems.Clear();

                await _orderRepo.SaveChangesAsync();
                await _orderRepo.CommitTransactionAsync();

                return null;
            }
            catch(Exception ex)
            {
                await _orderRepo.RollbackTransactionAsync();
                return ex.Message;
            }
        }
    }
}
