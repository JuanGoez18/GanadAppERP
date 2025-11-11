using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.API.Models;
using ERP.API.Data;
using TuProyecto.Modelos;

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GanadoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GanadoController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 GET: api/Ganado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ganado>>> GetGanados()
        {
            var ganados = await _context.Ganado
                .Where(g => g.Estado == true)
                .ToListAsync();

            return Ok(ganados);
        }

        // 🔹 GET: api/Ganado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ganado>> GetGanado(int id)
        {
            var ganado = await _context.Ganado.FindAsync(id);

            if (ganado == null || ganado.Estado == false)
                return NotFound(new { mensaje = "El ganado no existe o fue eliminado." });

            return Ok(ganado);
        }

        // 🔹 POST: api/Ganado
        [HttpPost]
        public async Task<ActionResult> CrearGanado(Ganado ganado)
        {
            try
            {
                // Se asegura que se cree activo por defecto
                ganado.Estado = true;

                _context.Ganado.Add(ganado);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetGanado), new { id = ganado.IdGanado }, ganado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = $"Error al crear el ganado: {ex.Message}" });
            }
        }

        // 🔹 PUT: api/Ganado/5
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarGanado(int id, Ganado ganado)
        {
            if (id != ganado.IdGanado)
                return BadRequest(new { mensaje = "El ID no coincide." });

            var ganadoExistente = await _context.Ganado.FindAsync(id);
            if (ganadoExistente == null || ganadoExistente.Estado == false)
                return NotFound(new { mensaje = "El ganado no existe o fue eliminado." });

            ganadoExistente.CodigoArete = ganado.CodigoArete;
            ganadoExistente.Raza = ganado.Raza;
            ganadoExistente.Edad = ganado.Edad;
            ganadoExistente.Peso = ganado.Peso;
            ganadoExistente.EstadoSalud = ganado.EstadoSalud;
            ganadoExistente.Supervisor = ganado.Supervisor;

            _context.Entry(ganadoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Ganado actualizado correctamente." });
        }

        // 🔹 DELETE lógico: api/Ganado/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarGanado(int id)
        {
            var ganado = await _context.Ganado.FindAsync(id);
            if (ganado == null)
                return NotFound(new { mensaje = "El ganado no existe." });

            // Borrado lógico
            ganado.Estado = false;

            _context.Entry(ganado).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Ganado eliminado correctamente (borrado lógico)." });
        }

        // 🔹 PUT: api/Ganado/reactivar/5
        [HttpPut("reactivar/{id}")]
        public async Task<ActionResult> ReactivarGanado(int id)
        {
            var ganado = await _context.Ganado.FindAsync(id);
            if (ganado == null)
                return NotFound(new { mensaje = "El ganado no existe." });

            ganado.Estado = true;

            _context.Entry(ganado).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Ganado reactivado correctamente." });
        }
    }
}
