using FluentValidation;

namespace ECommerce.BalanceManagement.Application.Features.Product.Queries.AllProducts;

public class AllProductsQueryValidator : AbstractValidator<AllProductsQuery>
{
    public AllProductsQueryValidator()
    {
    }
}