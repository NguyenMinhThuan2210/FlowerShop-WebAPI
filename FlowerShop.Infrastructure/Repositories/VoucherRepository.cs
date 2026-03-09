using FlowerShop.Core.Entities;
using FlowerShop.Core.Interfaces;
using FlowerShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Infrastructure.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly AppDbContext _context;
        public VoucherRepository(AppDbContext context) => _context = context;

        public async Task<Voucher?> GetByCodeAsync(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(v => v.Code == code);
        }
    }
}
