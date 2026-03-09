using FlowerShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FlowerShop.Infrastructure.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure (EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Quantity)
                .IsRequired();

            builder.Property(o => o.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.Order)
                   .WithMany(o => o.OrderDetails)
                   .HasForeignKey(o => o.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Product)
                   .WithMany(p => p.OrderDetails)
                   .HasForeignKey(o => o.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
