
namespace ECommerce.BalanceManagement.Application.Common.Exceptions;

public class InsufficientBalanceLimitException : GenericException
{
    public InsufficientBalanceLimitException(string message)
        : base(ApiErrorCode.InsufficientBalanceLimit, message)
    {
    }

    public InsufficientBalanceLimitException()
        : base(ApiErrorCode.InsufficientBalanceLimit, $"Your available limit is not enough to complete this operation!")
    {
    }
}