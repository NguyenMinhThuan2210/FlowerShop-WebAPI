using FlowerShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Interfaces
{
    public interface IVoucherRepository
    {
        Task<Voucher?> GetByCodeAsync(string code);
    }
}
