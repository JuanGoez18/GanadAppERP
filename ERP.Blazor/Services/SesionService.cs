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
            try
            {
                string json = JsonSerializer.Serialize(datos);
                using var doc = JsonDocument.Parse(json);

                // Si existe el campo "usuario", guardamos solo eso
                if (doc.RootElement.TryGetProperty("usuario", out var usuario))
                {
                    string usuarioJson = usuario.GetRawText();
                    await _js.InvokeVoidAsync("localStorage.setItem", "usuarioSesion", usuarioJson);
                }
                else
                {
                    // Si no, guarda todo
                    await _js.InvokeVoidAsync("localStorage.setItem", "usuarioSesion", json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando sesión: {ex.Message}");
            }
        }

        // Guarda directamente un JSON de usuario sin procesar
        public async Task GuardarSesionRawAsync(string usuarioJson)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", "usuarioSesion", usuarioJson);
        }

        // Obtiene los datos de sesión
        public async Task<T?> ObtenerSesionAsync<T>()
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", "usuarioSesion");
            if (string.IsNullOrEmpty(json))
                return default;

            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
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
