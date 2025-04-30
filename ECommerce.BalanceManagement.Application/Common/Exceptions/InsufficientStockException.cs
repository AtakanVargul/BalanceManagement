
namespace ECommerce.BalanceManagement.Application.Common.Exceptions;

public class InsufficientStockException : GenericException
{
    public InsufficientStockException(string message)
        : base(ApiErrorCode.InsufficientStock, message)
    {
    }

    public InsufficientStockException()
        : base(ApiErrorCode.InsufficientStock, $"The requested quantity exceeds available stock!")
    {
    }
}