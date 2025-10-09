using Microsoft.AspNetCore.Mvc;
using ERP.API.Data;
using ERP.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DnsClient;

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

            //Validar dominio con MX Lookup
            bool dominioValido = await DominioTieneMXAsync(nuevoUsuario.Correo);
            if (!dominioValido)
            {
                return BadRequest("El dominio del correo no existe, verifica que esté bien escrito.");
            }

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
        // Validar dominio de correo
        private async Task<bool> DominioTieneMXAsync(string correo)
        {
            try
            {
                var dominio = correo.Split('@')[1];
                var lookup = new LookupClient();
                var resultado = await lookup.QueryAsync(dominio, QueryType.MX);
                return resultado.Answers.MxRecords().Any();
            }
            catch
            {
                return false;
            }
        }

        //🟦 ENDPOINT PARA LOGIN DE USUARIO
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            if (string.IsNullOrWhiteSpace(login.Correo) || string.IsNullOrWhiteSpace(login.Contrasena))
                return BadRequest("Correo y contraseña son obligatorios.");

            // Buscar usuario por correo
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == login.Correo);

            if (usuario == null)
                return Unauthorized("Correo o contraseña incorrectos.");

            // Verificar contraseña
            var hasher = new PasswordHasher<Usuario>();
            var resultado = hasher.VerifyHashedPassword(usuario, usuario.Contrasena, login.Contrasena);

            if (resultado == PasswordVerificationResult.Failed)
                return Unauthorized("Correo o contraseña incorrectos.");

            // ✅ Login exitoso
            return Ok(new
            {
                mensaje = "Inicio de sesión correcto",
                usuario = new
                {
                    usuario.Id_Usuarios,
                    usuario.Nombre,
                    usuario.Correo,
                    usuario.Rol
                }
            });
        }
    }
}