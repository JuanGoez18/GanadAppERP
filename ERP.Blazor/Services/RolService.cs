using System.Net.Http.Json;
using ERP.Blazor.Models;

namespace ERP.Blazor.Services
{
    public class RolService
    {
        private readonly HttpClient _http;

        public RolService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<RolDTO>> ObtenerRolesAsync()
        {
            var result = await _http.GetFromJsonAsync<List<RolDTO>>("api/Roles");
            return result ?? new List<RolDTO>();
        }
    }
}