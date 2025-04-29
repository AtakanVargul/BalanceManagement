using ECommerce.BalanceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BalanceManagement.Infrastructure.Persistence.Configurations;

public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
{
    public void Configure(EntityTypeBuilder<Balance> builder)
    {
        builder.Property(u => u.AvailableBalance).IsRequired().HasPrecision(18, 2);
        builder.Property(u => u.BlockedBalance).IsRequired().HasPrecision(18, 2);
        builder.Property(u => u.TotalBalance).IsRequired().HasPrecision(18, 2);
        builder.Property(u => u.Currency).IsRequired();
        builder.Property(u => u.UserToken).IsRequired();
    }
}