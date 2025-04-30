
namespace ECommerce.BalanceManagement.Application.Features.Balance.Models;

public class CompleteResponse
{
    public OrderResponse Order { get; set; }
    public BalanceResponse UpdatedBalance { get; set; }
}