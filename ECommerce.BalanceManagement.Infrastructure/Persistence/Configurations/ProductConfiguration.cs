using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ECommerce.BalanceManagement.Domain.Entities;

namespace ECommerce.BalanceManagement.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(u => u.Category).IsRequired();
        builder.Property(u => u.Currency).IsRequired();
        builder.Property(u => u.Description).HasMaxLength(1000);
        builder.Property(u => u.Name).IsRequired();
        builder.Property(u => u.Price).IsRequired().HasPrecision(18, 2);
        builder.Property(u => u.Stock).IsRequired();
    }
}