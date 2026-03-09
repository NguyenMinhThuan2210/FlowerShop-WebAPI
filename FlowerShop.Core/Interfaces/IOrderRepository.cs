using FlowerShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<bool> SaveChangesAsync();
    }
}
