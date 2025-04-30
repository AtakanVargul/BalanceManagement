
namespace ECommerce.BalanceManagement.Application.Features.Balance.Models;

public class CancelResponse
{
    public OrderResponse Order { get; set; }
    public BalanceResponse UpdatedBalance { get; set; }
}