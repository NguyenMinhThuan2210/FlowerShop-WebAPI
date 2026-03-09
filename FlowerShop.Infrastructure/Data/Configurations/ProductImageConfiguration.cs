using FlowerShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerShop.Infrastructure.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure (EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.ImageUrl)
                .IsRequired()
                .HasColumnType("varchar(max)");

            builder.Property(p => p.IsMain)
                .HasDefaultValue(false);

            builder.HasOne(p => p.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
