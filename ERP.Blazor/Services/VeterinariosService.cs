using System.Net.Http.Json;
using ERP.Blazor.Models;

namespace ERP.Blazor.Services
{
    public class VeterinariosService
    {
        private readonly HttpClient _http;

        public VeterinariosService(HttpClient http)
        {
            _http = http;
        }

        // Obtener todos los veterinarios activos
        public async Task<List<VeterinarioDTO>> ObtenerVeterinariosAsync()
        {
            var response = await _http.GetFromJsonAsync<List<VeterinarioDTO>>("api/veterinarios");
            return response ?? new List<VeterinarioDTO>();
        }

        // Obtener veterinario por ID
        public async Task<VeterinarioDTO?> ObtenerVeterinarioPorIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<VeterinarioDTO>($"api/veterinarios/{id}");
        }

        // Crear nuevo veterinario
        public async Task<bool> CrearVeterinarioAsync(VeterinarioDTO veterinario)
        {
            var response = await _http.PostAsJsonAsync("api/veterinarios", veterinario);
            return response.IsSuccessStatusCode;
        }

        // Actualizar veterinario existente
        public async Task<bool> ActualizarVeterinarioAsync(VeterinarioDTO veterinario)
        {
            var response = await _http.PutAsJsonAsync($"api/veterinarios/{veterinario.IdVeterinarios}", veterinario);
            return response.IsSuccessStatusCode;
        }

        // Eliminación lógica (cambia estado a false)
        public async Task<bool> EliminarVeterinarioAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/veterinarios/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
