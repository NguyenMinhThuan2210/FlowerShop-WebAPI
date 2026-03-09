using FlowerShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FlowerShop.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrderDate)
                   .IsRequired();

            builder.Property(o => o.TotalAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(o => o.Status)
                   .IsRequired();

            builder.Property(o => o.Address)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(o => o.Phone)
                   .HasMaxLength(15)
                   .IsUnicode(false)
                   .IsRequired();

            builder.HasOne(o => o.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Voucher)
                   .WithMany(v => v.Orders)
                   .HasForeignKey(o => o.VoucherId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
