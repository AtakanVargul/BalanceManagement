using AutoMapper;

using ECommerce.BalanceManagement.Application.Common.Exceptions;
using ECommerce.BalanceManagement.Application.Common.Interfaces;
using ECommerce.BalanceManagement.Application.Features.Balance.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BalanceManagement.Application.Features.Balance.Queries.GetBalance;

public class GetBalanceQuery : IRequest<BalanceResponse>
{
    public string UserToken { get; set; }
}

public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, BalanceResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Domain.Entities.Balance> _repository;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetBalanceQueryHandler(IMapper mapper,
        IRepository<Domain.Entities.Balance> repository,
        IHttpContextAccessor contextAccessor)
    {
        _mapper = mapper;
        _repository = repository;
        _contextAccessor = contextAccessor;
    }

    public async Task<BalanceResponse> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        var balance = await _repository.GetByIdAsync(request.UserToken);

        if (balance is null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Balance),request.UserToken);
        }

        return _mapper.Map<BalanceResponse>(balance);
    }
}