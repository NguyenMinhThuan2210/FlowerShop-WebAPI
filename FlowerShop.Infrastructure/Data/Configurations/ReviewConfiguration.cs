using FlowerShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerShop.Infrastructure.Data.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(r =>  r.Id);
            builder.Property(r => r.Rating)
                .IsRequired();

            builder.Property(r => r.Comment)
                .HasMaxLength(500);
            
            builder.HasOne(r => r.Product)
                .WithMany(r => r.Reviews)
                .HasForeignKey(r =>r.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.User)
                .WithMany(r => r.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
