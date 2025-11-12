namespace Sultana.WEB.Auth;

public interface ILoginService
{
    Task LoginAsync(string token);
    Task LogoutAsync();
    Task<string?> GetTokenAsync();
}
