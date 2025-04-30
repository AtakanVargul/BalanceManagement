using ECommerce.BalanceManagement.Application.Common.Mappings;

namespace ECommerce.BalanceManagement.Application.Features.Balance.Models;

public class BalanceResponse : IMapFrom<Domain.Entities.Balance>
{
    public string UserId { get; set; }
    public decimal TotalBalance { get; set; }
    public decimal AvailableBalance { get; set; }
    public decimal BlockedBalance { get; set; }
    public string Currency { get; set; }
    public DateTime LastUpdated { get; set; }
}