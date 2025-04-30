using ECommerce.BalanceManagement.Application.Common.Pagination;
using ECommerce.BalanceManagement.Application.Features.Product.Models;
using ECommerce.BalanceManagement.Application.Features.Product.Queries.AllProducts;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.BalanceManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<AllProductsResponse>>> AllProducts(AllProductsQuery query)
    {
        return await Mediator.Send(new AllProductsQuery ());
    }
}