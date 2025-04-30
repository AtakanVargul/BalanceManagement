using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.BalanceManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected string UserToken => HttpContext?.Request?.Headers["Authorization"].ToString() ?? string.Empty;

    protected ISender Mediator =>
        HttpContext.RequestServices.GetRequiredService<ISender>();
}