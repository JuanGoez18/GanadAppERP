using Microsoft.AspNetCore.Mvc;
using ERP.API.Models;
using ERP.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminUsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminUsuariosController(AppDbContext context)
        {
            _context = context;
        }

        //optener usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        //Crear usurio nuevo
        [HttpPost]
        public async Task<ActionResult<Usuario>> CrearUsuario([FromBody] Usuario usuario)
        {
            string contrasenaGenerica = "ganadapp";

            // Encripta (hashea) la contraseña
            var hasher = new PasswordHasher<Usuario>();
            usuario.Contrasena = hasher.HashPassword(usuario, contrasenaGenerica);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok(usuario);
        }

        //Editar usurio
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarUsuario(int id, [FromBody] Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(id);
            if (usuarioExistente == null)
                return NotFound();

            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Apellido = usuario.Apellido;
            usuarioExistente.Correo = usuario.Correo;
            usuarioExistente.Rol = usuario.Rol;
            usuarioExistente.Estado_Cuenta = usuario.Estado_Cuenta;

            if (!string.IsNullOrEmpty(usuario.Contrasena))
                usuarioExistente.Contrasena = usuario.Contrasena;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Eliminar usurio ❗no usar solo borrado logico XD❗
        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }*/

        [HttpGet("roles")]
        public async Task<ActionResult<IEnumerable<RolDTO>>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }

        //Cambiar el estado de la cuenta (borrado logico)
        [HttpPut("{id}/estado/{nuevoEstado}")]
        public async Task<IActionResult> CambiarEstado(int id, bool nuevoEstado)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound(new { mensaje = "Usuario no encontrado." });

            usuario.Estado_Cuenta = nuevoEstado;
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"Estado de cuenta actualizado a {(nuevoEstado ? "Activo" : "Inactivo")}" });
        }
    }
}
