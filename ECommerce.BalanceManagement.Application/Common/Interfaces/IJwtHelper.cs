
namespace ECommerce.BalanceManagement.Application.Common.Interfaces;

public interface IJwtHelper
{
    Task<string> GenerateJwtTokenAsync(TimeSpan? expireIn = null);
}