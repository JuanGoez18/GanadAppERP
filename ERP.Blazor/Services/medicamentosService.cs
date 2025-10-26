using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ERP.Blazor.Models;

namespace ERP.Blazor.Services
{
    public class MedicamentosService
    {
        private readonly HttpClient _http;

        public MedicamentosService(HttpClient http)
        {
            _http = http;
        }

        // Obtener todos (backend devuelve activos, pero por seguridad filtramos)
        public async Task<List<MedicamentoDTO>> ObtenerMedicamentosAsync()
        {
            try
            {
                var lista = await _http.GetFromJsonAsync<List<MedicamentoDTO>>("api/medicamentos");
                return lista?.ToList() ?? new List<MedicamentoDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ObtenerMedicamentosAsync: {ex.Message}");
                return new List<MedicamentoDTO>();
            }
        }

        // Buscar por filtro (nombre/tipo)
        public async Task<List<MedicamentoDTO>> BuscarMedicamentosAsync(string filtro)
        {
            try
            {
                var lista = await _http.GetFromJsonAsync<List<MedicamentoDTO>>($"api/medicamentos/buscar?filtro={Uri.EscapeDataString(filtro)}");
                return lista?.ToList() ?? new List<MedicamentoDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BuscarMedicamentosAsync: {ex.Message}");
                return new List<MedicamentoDTO>();
            }
        }

        // Crear nuevo
        public async Task<bool> CrearMedicamentoAsync(MedicamentoDTO nuevo)
        {
            try
            {
                // Asegurar valores mínimos y usar UTC
                if (nuevo.FechaRegistro == default) nuevo.FechaRegistro = DateTime.UtcNow;
                if (nuevo.FechaCaducidad == default) nuevo.FechaCaducidad = DateTime.UtcNow.AddYears(5);
                nuevo.Estado = true;

                var resp = await _http.PostAsJsonAsync("api/medicamentos", nuevo);
                return resp.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error CrearMedicamentoAsync: {ex.Message}");
                return false;
            }
            
        }


        // Actualizar existente
        public async Task<bool> ActualizarMedicamentoAsync(MedicamentoDTO actualizado)
        {
            try
            {
                var resp = await _http.PutAsJsonAsync($"api/medicamentos/{actualizado.Id}", actualizado);
                return resp.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ActualizarMedicamentoAsync: {ex.Message}");
                return false;
            }
        }

        // Cambiar estado (borrado lógico). Si estado == false -> llamamos DELETE (que en backend hace estado=false)
        // Si estado == true -> reactivar: obtenemos, seteamos y PUT
        public async Task<bool> CambiarEstadoMedicamentoAsync(int id, bool estado)
        {
            try
            {
                if (!estado)
                {
                    // Llamar al endpoint DELETE (controller implementa borrado lógico)
                    var resp = await _http.DeleteAsync($"api/medicamentos/{id}");
                    return resp.IsSuccessStatusCode;
                }
                else
                {
                    // Reactivar: obtener, cambiar y actualizar
                    var med = await _http.GetFromJsonAsync<MedicamentoDTO>($"api/medicamentos/{id}");
                    if (med == null) return false;
                    med.Estado = true;
                    var resp = await _http.PutAsJsonAsync($"api/medicamentos/{id}", med);
                    return resp.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error CambiarEstadoMedicamentoAsync: {ex.Message}");
                return false;
            }
        }
    }
}
