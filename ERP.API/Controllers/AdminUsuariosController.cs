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

        // ✅ Obtener un usuario por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioPorId(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound(new { mensaje = "Usuario no encontrado." });

            return Ok(usuario);
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
            if (usuario == null)
                return BadRequest("Cuerpo vacío.");

            if (id != usuario.Id_Usuarios && usuario.Id_Usuarios != 0)
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo.");

            var usuarioExistente = await _context.Usuarios.FindAsync(id);
            if (usuarioExistente == null)
                return NotFound(new { mensaje = "Usuario no encontrado." });

            // Actualiza solo campos permitidos
            usuarioExistente.Nombre = usuario.Nombre ?? usuarioExistente.Nombre;
            usuarioExistente.Apellido = usuario.Apellido ?? usuarioExistente.Apellido;
            usuarioExistente.Correo = usuario.Correo ?? usuarioExistente.Correo;
            usuarioExistente.CC = usuario.CC ?? usuarioExistente.CC;
            usuarioExistente.Edad = usuario.Edad != 0 ? usuario.Edad : usuarioExistente.Edad;
            usuarioExistente.Estado_Civil = usuario.Estado_Civil ?? usuarioExistente.Estado_Civil;
            usuarioExistente.Sexo = usuario.Sexo ?? usuarioExistente.Sexo;
            usuarioExistente.Rol = usuario.Rol != 0 ? usuario.Rol : usuarioExistente.Rol;

            // Si vienen una nueva contraseña, hashearla (no aceptar cadena vacía)
            if (!string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                var hasher = new PasswordHasher<Usuario>();
                usuarioExistente.Contrasena = hasher.HashPassword(usuarioExistente, usuario.Contrasena);
            }

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException dbEx)
            {
                // Dev: retornar mensaje para depuración. En producción cambia por mensaje genérico.
                return StatusCode(500, new { mensaje = "Error al actualizar usuario (DB).", detalle = dbEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error inesperado al actualizar usuario.", detalle = ex.Message });
            }
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
