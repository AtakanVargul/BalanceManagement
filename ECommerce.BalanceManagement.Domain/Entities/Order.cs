using ECommerce.BalanceManagement.Domain.Entities.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.BalanceManagement.Domain.Entities;

[Table("Order", Schema = "ECommerce")]
public class Order : AuditableEntity, ITrackChange
{
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
    public string UserToken { get; set; }
    public int Amount { get; set; }
    public OrderStatus Status { get; set; }
}