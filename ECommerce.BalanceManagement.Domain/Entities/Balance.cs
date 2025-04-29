using ECommerce.BalanceManagement.Domain.Entities.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.BalanceManagement.Domain.Entities;

[Table("Balance", Schema = "ECommerce")]
public class Balance : AuditableEntity, ITrackChange
{
    public string UserToken { get; set; }
    public decimal TotalBalance { get; set; }
    public decimal AvailableBalance { get; set; }
    public decimal BlockedBalance { get; set; }
    public string Currency { get; set; }
    public DateTime LastUpdated { get; set; }
}