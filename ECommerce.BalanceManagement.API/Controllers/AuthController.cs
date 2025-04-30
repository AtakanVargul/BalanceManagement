using ECommerce.BalanceManagement.Application.Features.Auth.Models;
using ECommerce.BalanceManagement.Application.Features.Auth.Queries.Login;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.BalanceManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ApiControllerBase
{
    [HttpGet("token")]
    public async Task<ActionResult<LoginResponse>> Login()
    {
        return await Mediator.Send(new LoginQuery());
    }
}