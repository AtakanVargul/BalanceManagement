using ECommerce.BalanceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BalanceManagement.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(u => u.UserToken).IsRequired();
        builder.Property(u => u.ProductId).IsRequired();
        builder.Property(u => u.Status).IsRequired();
    }
}