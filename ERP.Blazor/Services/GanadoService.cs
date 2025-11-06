using System.Net.Http.Json;
using ERP.Blazor.Models;

namespace ERP.Blazor.Services
{
    public class GanadoService
    {
        private readonly HttpClient _http;

        public GanadoService(HttpClient http)
        {
            _http = http;
        }

        // ✅ Obtener todos los registros de ganado
        public async Task<List<GanadoDTO>> ObtenerGanadoAsync()
        {
            var response = await _http.GetAsync("api/ganado");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<GanadoDTO>>() ?? new List<GanadoDTO>();
            }
            return new List<GanadoDTO>();
        }

        // ✅ Obtener un ganado por ID
        public async Task<GanadoDTO?> ObtenerGanadoPorIdAsync(int id)
        {
            var response = await _http.GetAsync($"api/ganado/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GanadoDTO>();
            }
            return null;
        }

        // ✅ Crear un nuevo ganado
        public async Task<bool> CrearGanadoAsync(GanadoDTO nuevoGanado)
        {
            var response = await _http.PostAsJsonAsync("api/ganado", nuevoGanado);
            return response.IsSuccessStatusCode;
        }

        // ✅ Actualizar un ganado existente
        public async Task<bool> ActualizarGanadoAsync(int id, GanadoDTO ganadoActualizado)
        {
            var response = await _http.PutAsJsonAsync($"api/ganado/{id}", ganadoActualizado);
            return response.IsSuccessStatusCode;
        }

        // ✅ Eliminación lógica (no se borra, solo se marca como inactivo)
        public async Task<bool> EliminarGanadoAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/ganado/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
