using ECommerce.BalanceManagement.Application.Features.Balance.Commands.Cancel;
using ECommerce.BalanceManagement.Application.Features.Balance.Commands.Complete;
using ECommerce.BalanceManagement.Application.Features.Balance.Commands.PreOrder;
using ECommerce.BalanceManagement.Application.Features.Balance.Models;

using Microsoft.AspNetCore.Mvc;

namespace ECommerce.BalanceManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BalanceController : ApiControllerBase
{

    [HttpPost("preorder")]
    public async Task<ActionResult<PreOrderResponse>> Create([FromBody] PreOrderRequest request)
    {
        return await Mediator.Send(new PreOrderCommand { Amount = request.Amount, ProductId = request.ProductId, UserToken = _userToken });
    }

    [HttpPost("complete")]
    public async Task<ActionResult<CompleteResponse>> Complete([FromBody] CompleteRequest request)
    {
        return await Mediator.Send(new CompleteCommand { OrderId = request.OrderId, UserToken = _userToken });
    }

    [HttpPost("cancel")]
    public async Task<ActionResult<CancelResponse>> Cancel([FromBody] CancelRequest request)
    {
        return await Mediator.Send(new CancelCommand { OrderId = request.OrderId, UserToken = _userToken });
    }
}