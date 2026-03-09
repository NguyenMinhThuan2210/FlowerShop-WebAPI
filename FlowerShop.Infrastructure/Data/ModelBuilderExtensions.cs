using FlowerShop.Core.Entities;
using FlowerShop.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { Id = 1, Name = "Admin" },
                new AppRole { Id = 2, Name = "Customer" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Hoa Khai Trương", Description = "Hoa chúc mừng hồng phát" },
                new Category { Id = 2, Name = "Hoa Cưới", Description = "Hoa cho ngày trọng đại" },
                new Category { Id = 3, Name = "Hoa Sinh Nhật", Description = "Quà tặng ý nghĩa" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Bó Hoa Hồng Đỏ",
                    Price = 500000m,
                    Stock = 20,
                    Description = "Bó hoa hồng đỏ lãng mạn"
                },
                new Product
                {
                    Id = 2,
                    CategoryId = 3,
                    Name = "Giỏ Hoa Hướng Dương",
                    Price = 350000m,
                    Stock = 15,
                    Description = "Giỏ hoa tươi tắn như ánh nắng"
                }
            );
        }
    }
}
