using System.Net.Http.Json;
using ERP.Blazor.Models;

namespace ERP.Blazor.Services
{
    public class AdminUsuarioService
    {
        private readonly HttpClient _http;

        public AdminUsuarioService(HttpClient http)
        {
            _http = http;
        }

        // 📋 Obtener todos los usuarios
        public async Task<List<UsuarioDTO>> ObtenerUsuariosAsync()
        {
            var result = await _http.GetFromJsonAsync<List<UsuarioDTO>>("api/AdminUsuarios");
            return result ?? new List<UsuarioDTO>();
        }

        // 📋 Obtener lista de roles
        public async Task<List<RolDTO>> ObtenerRolesAsync()
        {
            var result = await _http.GetFromJsonAsync<List<RolDTO>>("api/AdminUsuarios/roles");
            return result ?? new List<RolDTO>();
        }

        // 🔍 Buscar usuarios (opcional)
        public async Task<List<UsuarioDTO>> BuscarUsuariosAsync(string filtro)
        {
            var result = await _http.GetFromJsonAsync<List<UsuarioDTO>>($"api/AdminUsuarios/buscar?filtro={filtro}");
            return result ?? new List<UsuarioDTO>();
        }

        // ➕ Crear usuario
        public async Task CrearUsuarioAsync(UsuarioDTO usuario)
        {
            await _http.PostAsJsonAsync("api/AdminUsuarios", usuario);
        }

        // ✏️ Actualizar usuario
        public async Task ActualizarUsuarioAsync(UsuarioDTO usuario)
        {
            await _http.PutAsJsonAsync($"api/AdminUsuarios/{usuario.Id}", usuario);
        }

        // 🔄 Cambiar estado (activo/inactivo)
        public async Task CambiarEstadoAsync(int id, bool nuevoEstado)
        {
            await _http.PutAsync($"api/AdminUsuarios/{id}/estado/{nuevoEstado}", null);
        }

        // 🗑️ Eliminar usuario
        public async Task EliminarUsuarioAsync(int id)
        {
            await _http.DeleteAsync($"api/AdminUsuarios/{id}");
        }
    }
}


