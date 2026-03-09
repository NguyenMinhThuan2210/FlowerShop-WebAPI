using FlowerShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerShop.Infrastructure.Data.Configurations
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure (EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("AppRoles");

            builder.HasKey(u => u.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
