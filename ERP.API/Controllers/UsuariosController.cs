using Microsoft.AspNetCore.Mvc;
using ERP.API.Data;
using ERP.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        //🟦 ENDPOINT PARA REGISTRO DE USUARIO
        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] Usuario nuevoUsuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar si ya existe el correo
            var existente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == nuevoUsuario.Correo);

            if (existente != null)
                return Conflict("El correo ya está registrado.");

            // ⚠️ Aquí puedes agregar lógica extra: encriptar contraseña, validar datos, etc.

            nuevoUsuario.Estado_Cuenta = true; // Activo por defecto
            nuevoUsuario.Rol = 3;      // Rol por defecto

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario registrado correctamente" });
        }
    }
}