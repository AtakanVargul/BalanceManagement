using ECommerce.BalanceManagement.Application.Common.Mappings;

namespace ECommerce.BalanceManagement.Application.Features.Product.Models;

public class AllProductsResponse : IMapFrom<Domain.Entities.Product>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public string Category { get; set; }
    public int Stock { get; set; }

}