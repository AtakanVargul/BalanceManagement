
namespace ECommerce.BalanceManagement.Domain.Entities.BaseModels;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}