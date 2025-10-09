using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace ERP.Blazor.Services
{
    public class SesionService
    {
        private readonly IJSRuntime _js;

        public SesionService(IJSRuntime js)
        {
            _js = js;
        }

        // Guarda datos de sesión en localStorage
        public async Task GuardarSesionAsync(object datos)
        {
            string json = JsonSerializer.Serialize(datos);
            await _js.InvokeVoidAsync("localStorage.setItem", "usuarioSesion", json);
        }

        // Obtiene los datos de sesión
        public async Task<T?> ObtenerSesionAsync<T>()
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", "usuarioSesion");
            if (string.IsNullOrEmpty(json))
                return default;

            return JsonSerializer.Deserialize<T>(json);
        }

        // Elimina la sesión
        public async Task CerrarSesionAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "usuarioSesion");
        }

        // Verifica si hay sesión activa
        public async Task<bool> HaySesionAsync()
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", "usuarioSesion");
            return !string.IsNullOrEmpty(json);
        }
    }
}
