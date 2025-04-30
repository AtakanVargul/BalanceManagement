using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.BalanceManagement.Application.Common.Exceptions;
using ECommerce.BalanceManagement.Application.Common.Interfaces;
using ECommerce.BalanceManagement.Application.Common.Mappings;
using ECommerce.BalanceManagement.Application.Common.Pagination;
using ECommerce.BalanceManagement.Application.Features.Product.Models;
using MediatR;

namespace ECommerce.BalanceManagement.Application.Features.Product.Queries.AllProducts;

public class AllProductsQuery : SearchQueryParams, IRequest<PaginatedList<AllProductsResponse>>
{
}

public class AllProductsQueryHandler : IRequestHandler<AllProductsQuery, PaginatedList<AllProductsResponse>>
{
    private readonly IRepository<Domain.Entities.Product> _productRepository;
    private readonly IMapper _mapper;

    public AllProductsQueryHandler(IRepository<Domain.Entities.Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AllProductsResponse>> Handle(AllProductsQuery query, CancellationToken cancellationToken)
    {
        var products = _productRepository.GetAll()
            ?? throw new NotFoundException(nameof(Domain.Entities.Product));

        return await products.ProjectTo<AllProductsResponse>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(query.Page, query.Size);
    }
}