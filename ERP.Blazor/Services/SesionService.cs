using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;
using ERP.Blazor.Models;

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

                if (doc.RootElement.TryGetProperty("usuario", out var usuario))
                {
                    string usuarioJson = usuario.GetRawText();
                    await _js.InvokeVoidAsync("localStorage.setItem", "usuarioSesion", usuarioJson);
                }
                else
                {
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

        // Verifica si hay sesión activa
        public async Task<bool> HaySesionAsync()
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", "usuarioSesion");
            return !string.IsNullOrEmpty(json);
        }

        // Cierra la sesión
        public async Task CerrarSesionAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "usuarioSesion");
        }

        // ✅ Nuevo: Obtener el rol del usuario autenticado
        public async Task<string> ObtenerRolUsuarioAsync()
        {
            var usuario = await ObtenerSesionAsync<UsuarioDTO>();
            return usuario?.RolNombre ?? string.Empty;
        }

        // ✅ Nuevo: Obtener el ID del usuario autenticado
        public async Task<int> ObtenerIdUsuarioAsync()
        {
            var usuario = await ObtenerSesionAsync<UsuarioDTO>();
            return usuario?.Id_Usuarios ?? 0;
        }

        // ✅ Opcional: Obtener nombre completo del usuario
        public async Task<string> ObtenerNombreCompletoAsync()
        {
            var usuario = await ObtenerSesionAsync<UsuarioDTO>();
            if (usuario == null) return string.Empty;
            return $"{usuario.Nombre} {usuario.Apellido}".Trim();
        }
    }
}
