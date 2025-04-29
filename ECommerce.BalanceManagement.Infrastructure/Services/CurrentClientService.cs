using ECommerce.BalanceManagement.Application.Common.Interfaces;

namespace ECommerce.BalanceManagement.Infrastructure.Services;

public class CurrentClientService : ICurrentClientService
{
    public string ClientToken { get; }
}