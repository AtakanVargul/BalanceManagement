using AutoMapper;
using ECommerce.BalanceManagement.Application.Common.Exceptions;
using ECommerce.BalanceManagement.Application.Common.Interfaces;
using ECommerce.BalanceManagement.Application.Features.Balance.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BalanceManagement.Application.Features.Balance.Commands.Cancel;

public class CancelCommand : IRequest<CancelResponse>
{
    public string UserToken { get; set; }
    public string OrderId { get; set; }
}

public class CancelCommandHandler : IRequestHandler<CancelCommand, CancelResponse>
{
    private readonly IRepository<Domain.Entities.Order> _orderRepository;
    private readonly IRepository<Domain.Entities.Balance> _balanceRepository;
    private readonly IRepository<Domain.Entities.Product> _productRepository;
    private readonly IMapper _mapper;

    public CancelCommandHandler(IRepository<Domain.Entities.Order> orderRepository,
        IRepository<Domain.Entities.Balance> balanceRepository,
        IRepository<Domain.Entities.Product> productRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _balanceRepository = balanceRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<CancelResponse> Handle(CancelCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAll(x => x.Id == Guid.Parse(command.OrderId) && x.Status == Domain.Enums.OrderStatus.Blocked)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(nameof(Domain.Entities.Order),command.OrderId);

        order.LastModifiedBy = command.UserToken;
        order.Status = Domain.Enums.OrderStatus.Cancelled;

        var product = await _productRepository.GetByIdAsync(order.ProductId) ?? throw new NotFoundException(nameof(Domain.Entities.Product), order.ProductId);
        product.Stock += order.Amount;

        var balance = await _balanceRepository.GetAll(x => x.UserToken == command.UserToken)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? throw new NotFoundException(nameof(Domain.Entities.Product), order.ProductId);

        var totalPrice = product.Price * order.Amount;

        balance.BlockedBalance -= totalPrice;
        balance.TotalBalance += totalPrice;

        await _orderRepository.UpdateAsync(order);
        await _balanceRepository.UpdateAsync(balance);
        await _productRepository.UpdateAsync(product);

        return _mapper.Map<CancelResponse>(order);
    }
}