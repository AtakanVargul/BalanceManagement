using ECommerce.BalanceManagement.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace ECommerce.BalanceManagement.Application.Common.Models.Audit;

public class AuditLogService : IAuditLogService
{
    private readonly ILogger<AuditLogService> _logger;

    public AuditLogService(ILogger<AuditLogService> logger)
    {
        _logger = logger;
    }

    public Task AuditLogAsync(AuditLog auditLog)
    {
        _logger.LogError($"ExceptionOnSendAuditLog detail:\n{auditLog}");
        return Task.CompletedTask;
    }
}