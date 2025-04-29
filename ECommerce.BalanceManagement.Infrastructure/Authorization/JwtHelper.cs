using ECommerce.BalanceManagement.Application.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace ECommerce.BalanceManagement.Infrastructure.Authorization;

public class JwtHelper : IJwtHelper
{
    private readonly TokenProviderOptions _options;

    public JwtHelper(TokenProviderOptions options)
    {
        _options = options;
    }

    public Task<string> GenerateJwtTokenAsync(TimeSpan? expireIn = null)
    {
        List<string> claimChecker = new();
        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            notBefore: now,
            expires: now.Add(expireIn ?? _options.Expiration),
            signingCredentials: _options.SigningCredentials);

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return Task.FromResult(encodedJwt);
    }
}