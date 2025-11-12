using Microsoft.AspNetCore.Components.Authorization;
using Sultana.WEB.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sultana.WEB.Auth;

public class JwtAuthenticationStateProvider(ILoginService loginService) : AuthenticationStateProvider
{
    private readonly AuthenticationState _anonymous =
        new(new ClaimsPrincipal(new ClaimsIdentity()));

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await loginService.GetTokenAsync();
        if (string.IsNullOrWhiteSpace(token))
            return _anonymous;

        var claims = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
        var identity = new ClaimsIdentity(claims, "jwt");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void NotifyLogin() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    public void NotifyLogout() => NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
}

