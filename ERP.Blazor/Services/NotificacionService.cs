using Microsoft.JSInterop;
using System.Text.Json;

namespace ERP.Blazor.Services
{
    public class NotificacionService
    {
        private readonly IJSRuntime _js;
        private const string STORAGE_KEY = "notificaciones";

        public NotificacionService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<List<string>> ObtenerAsync()
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", STORAGE_KEY);
            if (string.IsNullOrEmpty(json)) return new List<string>();
            return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }

        // Guarda lista (asegura unicidad antes de persistir)
        public async Task GuardarAsync(List<string> notificaciones)
        {
            var unica = notificaciones.Distinct().ToList();
            var json = JsonSerializer.Serialize(unica);
            await _js.InvokeVoidAsync("localStorage.setItem", STORAGE_KEY, json);
        }

        // Agrega un mensaje si no existe ya (evita duplicados)
        public async Task AgregarAsync(string mensaje)
        {
            var lista = await ObtenerAsync();
            if (!lista.Contains(mensaje))
            {
                lista.Add(mensaje);
                await GuardarAsync(lista);
            }
        }

        // Elimina mensaje si existe
        public async Task EliminarAsync(string mensaje)
        {
            var lista = await ObtenerAsync();
            if (lista.Remove(mensaje))
                await GuardarAsync(lista);
        }

        public async Task LimpiarAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", STORAGE_KEY);
        }

        // Guarda la fecha (yyyy-MM-dd) para una clave (ej. notificaciones_generadas_hoy)
        public async Task GuardarUltimaNotificacionFechaAsync(string clave)
        {
            string hoy = DateTime.Now.ToString("yyyy-MM-dd");
            await _js.InvokeVoidAsync("localStorage.setItem", clave, hoy);
        }

        public async Task<string?> ObtenerUltimaNotificacionFechaAsync(string clave)
        {
            return await _js.InvokeAsync<string>("localStorage.getItem", clave);
        }

        // Devuelve true si la clave tiene la fecha de hoy
        public async Task<bool> NotificacionYaMostradaHoyAsync(string clave)
        {
            string hoy = DateTime.Now.ToString("yyyy-MM-dd");
            var fechaGuardada = await _js.InvokeAsync<string>("localStorage.getItem", clave);
            return fechaGuardada == hoy;
        }

        // Guardar/leer bandera (true/false)
        public async Task GuardarBanderaAsync(string clave, bool valor)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", clave, valor ? "true" : "false");
        }

        public async Task<bool> ObtenerBanderaAsync(string clave)
        {
            var valor = await _js.InvokeAsync<string>("localStorage.getItem", clave);
            return valor == "true";
        }

        public async Task EliminarClaveAsync(string clave)
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", clave);
        }
    }
}
