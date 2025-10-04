using ERP.API.Data;
using ERP.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Sesioncontrolador : ControllerBase
    {
        private readonly AppDbContext _context;

        public Sesioncontrolador(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET: Listar todas las sesiones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sesion>>> GetSesiones()
        {
            return await _context.Sesiones
                                 .Include(s => s.Usuario) // Incluye datos del usuario
                                 .ToListAsync();
        }

        // ✅ GET: Sesion por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Sesion>> GetSesion(int id)
        {
            var sesion = await _context.Sesiones
                                       .Include(s => s.Usuario)
                                       .FirstOrDefaultAsync(s => s.id_sesion == id);

            if (sesion == null)
                return NotFound();

            return sesion;
        }

        // ✅ POST: Crear nueva sesión
        [HttpPost]
        public async Task<ActionResult<Sesion>> PostSesion(Sesion sesion)
        {
            _context.Sesiones.Add(sesion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSesion), new { id = sesion.id_sesion }, sesion);
        }

        // ✅ PUT: Actualizar sesión
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSesion(int id, Sesion sesion)
        {
            if (id != sesion.id_sesion)
                return BadRequest();

            _context.Entry(sesion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Sesiones.Any(e => e.id_sesion == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // ✅ DELETE: Eliminar sesión
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSesion(int id)
        {
            var sesion = await _context.Sesiones.FindAsync(id);
            if (sesion == null)
                return NotFound();

            _context.Sesiones.Remove(sesion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

