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
    public async Task<ActionResult<PreOrderResponse>> Create([FromBody] PreOrderCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("{id}/complete")]
    public async Task<ActionResult<CompleteResponse>> Complete([FromRoute] string id, [FromBody] CompleteCommand command)
    {
        return await Mediator.Send(new CompleteCommand());
    }

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult<CompleteResponse>> Cancel([FromRoute] string id, [FromBody] CompleteCommand command)
    {
        return await Mediator.Send(command);
    }
}