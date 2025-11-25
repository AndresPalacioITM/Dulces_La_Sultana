using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Sultana.WEB.Auth;
using Sultana.Shared.DTOs;

namespace Sultana.WEB.AutenticationProviders
{
    public class AuthenticationProviderJWT : AuthenticationStateProvider, ILoginService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;
        private const string TokenKey = "TOKEN_KEY";
        private readonly AuthenticationState _anonymous =
            new(new ClaimsPrincipal(new ClaimsIdentity()));

        public AuthenticationProviderJWT(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var token = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", TokenKey);
            if (string.IsNullOrWhiteSpace(token))
                return _anonymous;

            return BuildAuthenticationState(token);
        }

        private AuthenticationState BuildAuthenticationState(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            return jwt.Claims;
        }

        // ILoginService
        public async Task LoginAsync(TokenDTO token)
        {
            if (token == null || string.IsNullOrWhiteSpace(token.Token)) 
            {
                throw new ArgumentException("Token no puede ser nulo o vacio");
            }
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
            var authState = BuildAuthenticationState(token.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
        }

        public async Task<string?> GetTokenAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", TokenKey);
            return string.IsNullOrWhiteSpace(token) ? null : token;
        }
    }
}

