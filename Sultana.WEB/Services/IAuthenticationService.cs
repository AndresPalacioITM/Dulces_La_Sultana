using Microsoft.AspNetCore.Components;
using Sultana.Shared.DTOs;

namespace Sultana.WEB.Services
{

    public interface IAuthenticationService
    {
        Task Login(TokenDTO token);
        Task Logout();
        Task<bool> IsAuthenticated();
        Task<string> GetToken();
    }
    public class AuthenticationService: IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task Login(TokenDTO token)
        {
            await _localStorageService.SetItemAsync("authToken", token.Token);
            await _localStorageService.SetItemAsync("tokenExpiration", token.Expiration);

            //Actualizar headers de HttpClient
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);
        }
        public async Task Logout()
        {
            //Eliminar token del almacenamiento local
            await _localStorageService.RemoveItemAsync("authToken");
            await _localStorageService.RemoveItemAsync("tokenExpiration");

            //Eliminar headers de HttpClient
            _httpClient.DefaultRequestHeaders.Authorization = null;

            //forzar recarga de la aplicación
            NavigationManager.NavigateTo("/", true);
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await GetToken();

            var isTokenExpired = await IsTokenExpired();
            return !string.IsNullOrEmpty(token) && !isTokenExpired;
        }
        public async Task<string> GetToken()
        {
            return await _localStorageService.GetItemAsync<string>("authToken");
        }
        private async Task<bool> IsTokenExpired()
        {
            var expiration = await _localStorageService.GetItemAsync<DateTime>("tokenExpiration");
            return expiration == null || expiration < DateTime.UtcNow;
        }
    }
}
