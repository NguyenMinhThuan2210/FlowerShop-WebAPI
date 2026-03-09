using FlowerShop.Core.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Interfaces
{
    public interface IOrderService
    {
        Task<string?> CheckoutAsync(Guid userId, CheckoutDto dto);
    }
}
