using Microsoft.JSInterop;
using Sultana.WEB.Auth;

namespace Sultana.WEB.Auth;

public class LoginService(IJSRuntime js) : ILoginService
{
    private const string Key = "auth_token";

    public Task LoginAsync(string token) =>
        js.InvokeVoidAsync("localStorage.setItem", Key, token).AsTask();

    public Task LogoutAsync() =>
        js.InvokeVoidAsync("localStorage.removeItem", Key).AsTask();

    public Task<string?> GetTokenAsync() =>
        js.InvokeAsync<string?>("localStorage.getItem", Key).AsTask();
}
