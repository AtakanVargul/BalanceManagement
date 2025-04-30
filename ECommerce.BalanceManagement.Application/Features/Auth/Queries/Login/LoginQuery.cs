using AutoMapper;

using ECommerce.BalanceManagement.Application.Common.Interfaces;
using ECommerce.BalanceManagement.Application.Features.Auth.Models;

using MediatR;

namespace ECommerce.BalanceManagement.Application.Features.Auth.Queries.Login;

public class LoginQuery : IRequest<LoginResponse>
{
}

public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
{
    private readonly IJwtHelper _jwtHelper;

    public LoginQueryHandler(IJwtHelper jwtHelper)
    {
        _jwtHelper = jwtHelper;
    }

    public async Task<LoginResponse> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var token = await _jwtHelper.GenerateJwtTokenAsync();

        return new LoginResponse
        {
            Token = token
        };
    }
}