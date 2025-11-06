using ERP.API.Data;
using ERP.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeterinariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VeterinariosController(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los veterinarios activos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veterinario>>> GetVeterinarios()
        {
            var veterinarios = await _context.Veterinarios
                .Where(v => v.Estado)
                .ToListAsync();

            return Ok(veterinarios);
        }

        // Obtener un veterinario por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Veterinario>> GetVeterinario(int id)
        {
            var veterinario = await _context.Veterinarios.FindAsync(id);

            if (veterinario == null || !veterinario.Estado)
                return NotFound();

            return Ok(veterinario);
        }

        // Crear un nuevo veterinario
        [HttpPost]
        public async Task<ActionResult<Veterinario>> CrearVeterinario(Veterinario veterinario)
        {
            try
            {
                _context.Veterinarios.Add(veterinario);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetVeterinario), new { id = veterinario.IdVeterinarios }, veterinario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear veterinario: {ex.Message}");
            }
        }

        // Actualizar un veterinario existente
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarVeterinario(int id, Veterinario veterinario)
        {
            if (id != veterinario.IdVeterinarios)
                return BadRequest("El ID no coincide.");

            var veterinarioExistente = await _context.Veterinarios.FindAsync(id);
            if (veterinarioExistente == null)
                return NotFound();

            veterinarioExistente.Nombre = veterinario.Nombre;
            veterinarioExistente.Apellido = veterinario.Apellido;
            veterinarioExistente.Edad = veterinario.Edad;
            veterinarioExistente.CC = veterinario.CC;
            veterinarioExistente.Sexo = veterinario.Sexo;
            veterinarioExistente.Direccion = veterinario.Direccion;
            veterinarioExistente.Telefono = veterinario.Telefono;
            veterinarioExistente.Entidad = veterinario.Entidad;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Eliminación lógica
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVeterinario(int id)
        {
            var veterinario = await _context.Veterinarios.FindAsync(id);
            if (veterinario == null)
                return NotFound();

            veterinario.Estado = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
