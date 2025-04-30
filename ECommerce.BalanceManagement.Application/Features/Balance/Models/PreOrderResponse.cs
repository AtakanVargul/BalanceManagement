
namespace ECommerce.BalanceManagement.Application.Features.Balance.Models;

public class PreOrderResponse
{
    public OrderResponse PreOrder { get; set; }
    public BalanceResponse UpdatedBalance { get; set; }
}