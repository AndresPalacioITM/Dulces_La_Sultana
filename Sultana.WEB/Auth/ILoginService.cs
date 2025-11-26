using Sultana.Shared.DTOs;
using Sultana.WEB.Services;


namespace Sultana.WEB.Auth
{

public interface ILoginService
{
    Task LoginAsync(TokenDTO token);
    Task LogoutAsync();
    Task<string?> GetTokenAsync();
}
public class LoginService : ILoginService
{
    private readonly IAuthenticationService _authService;
    private readonly JwtAuthenticationStateProvider _authStateProvider;

    public LoginService(
        IAuthenticationService authService,
        JwtAuthenticationStateProvider authStateProvider)
    {
        _authService = authService;
        _authStateProvider = authStateProvider;
    }

    public async Task LoginAsync(TokenDTO token)
    {
        await _authService.Login(token);
        _authStateProvider.NotifyLogin();
    }

    public async Task LogoutAsync()
    {
        await _authService.Logout();
        _authStateProvider.NotifyLogout();
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _authService.GetToken();
    }
}
}

