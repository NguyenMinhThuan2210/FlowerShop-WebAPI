using FlowerShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FlowerShop.Infrastructure.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Quantity)
                .IsRequired();

            builder.HasIndex(p => new { p.CartId, p.ProductId })
                .IsUnique();

            builder.HasOne(p => p.Cart)
                 .WithMany(p => p.CartItems)
                 .HasForeignKey(p => p.CartId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
