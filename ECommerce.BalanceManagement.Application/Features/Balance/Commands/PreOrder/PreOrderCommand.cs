using AutoMapper;
using ECommerce.BalanceManagement.Application.Common.Exceptions;
using ECommerce.BalanceManagement.Application.Common.Interfaces;
using ECommerce.BalanceManagement.Application.Features.Balance.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BalanceManagement.Application.Features.Balance.Commands.PreOrder;

public class PreOrderCommand : IRequest<PreOrderResponse>
{
    public string UserToken { get; set; }
    public string ProductId { get; set; }
    public int Amount { get; set; }
}

public class PreOrderCommandHandler : IRequestHandler<PreOrderCommand, PreOrderResponse>
{
    private readonly IRepository<Domain.Entities.Order> _orderRepository;
    private readonly IRepository<Domain.Entities.Balance> _balanceRepository;
    private readonly IRepository<Domain.Entities.Product> _productRepository;
    private readonly IMapper _mapper;

    public PreOrderCommandHandler(IRepository<Domain.Entities.Order> orderRepository,
       IRepository<Domain.Entities.Balance> balanceRepository,
       IRepository<Domain.Entities.Product> productRepository,
       IMapper mapper)
    {
        _orderRepository = orderRepository;
        _balanceRepository = balanceRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PreOrderResponse> Handle(PreOrderCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.ProductId) ?? throw new NotFoundException(nameof(Domain.Entities.Product), command.ProductId);

        if (product.Stock < command.Amount)
        {
            throw new InsufficientStockException();
        }

        product.Stock -= command.Amount;

        var order = new Domain.Entities.Order
        {
            ProductId = product.Id,
            Product = product,
            UserToken = command.UserToken,
            Status = Domain.Enums.OrderStatus.Blocked,
            Amount = command.Amount
        };

        var balance = await _balanceRepository.GetAll(x => x.UserToken == command.UserToken)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? throw new NotFoundException(nameof(Domain.Entities.Balance), command.UserToken);
        
        var totalPrice = product.Price * command.Amount;

        if (balance.AvailableBalance < totalPrice)
        {
            throw new InsufficientBalanceLimitException();
        }

        balance.BlockedBalance += totalPrice;
        balance.AvailableBalance -= totalPrice;

        await _orderRepository.AddAsync(order);
        await _balanceRepository.UpdateAsync(balance);
        await _productRepository.UpdateAsync(product);

        return _mapper.Map<PreOrderResponse>(order);
    }
}