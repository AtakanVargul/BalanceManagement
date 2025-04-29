using ECommerce.BalanceManagement.Application.Common.Models.Audit;

namespace ECommerce.BalanceManagement.Application.Common.Interfaces;

public interface IAuditLogService
{
    public Task AuditLogAsync(AuditLog auditLog);
}