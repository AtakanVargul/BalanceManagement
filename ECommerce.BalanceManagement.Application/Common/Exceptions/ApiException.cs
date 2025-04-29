
namespace ECommerce.BalanceManagement.Application.Common.Exceptions;

public class ApiException : Exception
{
    public readonly string Code;

    public ApiException(string code, string message)
        : base(message)
    {
        Code = code;
    }
}