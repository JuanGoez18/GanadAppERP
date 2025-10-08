using Microsoft.AspNetCore.Mvc;
using ERP.API.Data;
using ERP.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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

            if (!System.Text.RegularExpressions.Regex.IsMatch(nuevoUsuario.Correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return BadRequest("El formato del correo es inválido.");

            // Verificar si ya existe el correo
            var existente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == nuevoUsuario.Correo);

            if (existente != null)
                return Conflict("El correo ya está registrado.");

            // Verificar si ya contraseña es segura
            if (nuevoUsuario.Contrasena.Length < 7 ||
                 !nuevoUsuario.Contrasena.Any(char.IsLetter) ||
                 !nuevoUsuario.Contrasena.Any(char.IsDigit))
            {
                return BadRequest("La contraseña no cumple con los requisitos mínimos.");
            }

            // Encriptar contraseña
            var hasher = new PasswordHasher<Usuario>();
            nuevoUsuario.Contrasena = hasher.HashPassword(nuevoUsuario, nuevoUsuario.Contrasena);

            nuevoUsuario.Estado_Cuenta = true;
            nuevoUsuario.Rol = 3;

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario registrado correctamente" });
        }
    }
}