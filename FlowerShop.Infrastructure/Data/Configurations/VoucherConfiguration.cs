using FlowerShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerShop.Infrastructure.Data.Configurations
{
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Vouchers");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(v => v.Code)
                .IsUnique();
            builder.Property(v => v.DiscountPercent)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(v => v.ExpiryDate)
                .IsRequired();

            builder.Property(v => v.IsActive)
                .IsRequired();
        }
    }
}
