using System.Net.Http.Json;
using Sultana.Shared.Entities;

namespace Sultana.WEB.Services
{
    public class AlertasService
    {
        private readonly HttpClient _httpClient;

        public AlertasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Alerta>> GetAlertasProveedorAsync(int proveedorId)
        {
            return await _httpClient.GetFromJsonAsync<List<Alerta>>($"api/alertas/proveedor/{proveedorId}")
                   ?? new List<Alerta>();
        }
    }
}
