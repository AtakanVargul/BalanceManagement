using ECommerce.BalanceManagement.Domain.Entities.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.BalanceManagement.Domain.Entities;

[Table("Product", Schema = "ECommerce")]
public class Product : AuditableEntity, ITrackChange
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public string Category { get; set; }
    public int Stock { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}