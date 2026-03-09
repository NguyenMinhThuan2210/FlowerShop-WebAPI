using FlowerShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerShop.Infrastructure.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure (EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(u => u.UserName)
                   .IsUnique();

            builder.Property(u => u.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasIndex(u => u.Email)
                   .IsUnique();

            builder.Property(u => u.PasswordHash)
                   .HasColumnType("nvarchar(max)")
                   .IsRequired();

            builder.Property(u => u.FullName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(15)
                   .IsUnicode(false);

            builder.Property(u => u.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasMany(u => u.Reviews)
                   .WithOne(r => r.User)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
