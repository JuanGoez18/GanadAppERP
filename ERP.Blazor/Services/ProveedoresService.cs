using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ERP.Blazor.Models;

namespace ERP.Blazor.Services
{
    public class ProveedoresService
    {
        private readonly HttpClient _http;

        public ProveedoresService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProveedorDTO>> ObtenerProveedoresAsync()
        {
            try
            {
                var lista = await _http.GetFromJsonAsync<List<ProveedorDTO>>("api/proveedores");
                return lista?.ToList() ?? new List<ProveedorDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ObtenerProveedoresAsync: {ex}");
                return new List<ProveedorDTO>();
            }
        }

        public async Task<List<ProveedorDTO>> BuscarProveedoresAsync(string filtro)
        {
            try
            {
                var lista = await _http.GetFromJsonAsync<List<ProveedorDTO>>($"api/proveedores/buscar?filtro={Uri.EscapeDataString(filtro)}");
                return lista?.ToList() ?? new List<ProveedorDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BuscarProveedoresAsync: {ex}");
                return new List<ProveedorDTO>();
            }
        }

        // Crear: devuelve (ok, mensajeError)
        public async Task<(bool ok, string error)> CrearProveedorAsync(ProveedorDTO nuevo)
        {
            try
            {
                nuevo.Estado = true;
                var resp = await _http.PostAsJsonAsync("api/proveedores", nuevo);

                if (resp.IsSuccessStatusCode)
                    return (true, string.Empty);

                // Leer detalle de error en body (si existe)
                var detalle = await resp.Content.ReadAsStringAsync();
                Console.WriteLine($"CrearProveedorAsync failed: {(int)resp.StatusCode} - {detalle}");
                return (false, $"Error servidor: {(int)resp.StatusCode} - {detalle}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception CrearProveedorAsync: {ex}");
                return (false, ex.Message);
            }
        }

        // Actualizar
        public async Task<(bool ok, string error)> ActualizarProveedorAsync(ProveedorDTO actualizado)
        {
            try
            {
                var resp = await _http.PutAsJsonAsync($"api/proveedores/{actualizado.IdProveedores}", actualizado);
                if (resp.IsSuccessStatusCode)
                    return (true, string.Empty);

                var detalle = await resp.Content.ReadAsStringAsync();
                Console.WriteLine($"ActualizarProveedorAsync failed: {(int)resp.StatusCode} - {detalle}");
                return (false, $"Error servidor: {(int)resp.StatusCode} - {detalle}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception ActualizarProveedorAsync: {ex}");
                return (false, ex.Message);
            }
        }

        // Cambiar estado (DELETE lógico)
        public async Task<(bool ok, string error)> CambiarEstadoProveedorAsync(int id, bool estado)
        {
            try
            {
                if (!estado)
                {
                    var resp = await _http.DeleteAsync($"api/proveedores/{id}");
                    if (resp.IsSuccessStatusCode) return (true, string.Empty);
                    var detalle = await resp.Content.ReadAsStringAsync();
                    Console.WriteLine($"CambiarEstadoProveedorAsync delete failed: {(int)resp.StatusCode} - {detalle}");
                    return (false, $"Error servidor: {(int)resp.StatusCode} - {detalle}");
                }
                else
                {
                    // Reactivar (obtener, setear, put)
                    var prov = await _http.GetFromJsonAsync<ProveedorDTO>($"api/proveedores/{id}");
                    if (prov == null) return (false, "Proveedor no encontrado para reactivar.");
                    prov.Estado = true;
                    var resp = await _http.PutAsJsonAsync($"api/proveedores/{id}", prov);
                    if (resp.IsSuccessStatusCode) return (true, string.Empty);
                    var detalle = await resp.Content.ReadAsStringAsync();
                    Console.WriteLine($"CambiarEstadoProveedorAsync reactivate failed: {(int)resp.StatusCode} - {detalle}");
                    return (false, $"Error servidor: {(int)resp.StatusCode} - {detalle}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception CambiarEstadoProveedorAsync: {ex}");
                return (false, ex.Message);
            }
        }
    }
}
