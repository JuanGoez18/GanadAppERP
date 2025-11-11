using ERP.API.Data;
using ERP.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicamentosController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todos los medicamentos activos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicamento>>> GetMedicamentos()
        {
            var lista = await _context.Medicamentos
                .Where(m => m.Estado == true)
                .ToListAsync();

            return Ok(lista);
        }

        // 🔹 Obtener un medicamento por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicamento>> GetMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);

            if (medicamento == null || !medicamento.Estado)
                return NotFound();

            return Ok(medicamento);
        }

        // 🔹 Registrar medicamento
        [HttpPost]
        public async Task<ActionResult> RegistrarMedicamento([FromBody] Medicamento med)
        {
            if (med == null)
                return BadRequest("Datos inválidos.");

            med.FechaRegistro = DateTime.UtcNow;;
            med.Estado = true; // Por defecto activo

            _context.Medicamentos.Add(med);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Medicamento registrado correctamente." });
        }

        // 🔹 Actualizar medicamento
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarMedicamento(int id, [FromBody] Medicamento med)
        {
            var existente = await _context.Medicamentos.FindAsync(id);
            if (existente == null)
                return NotFound();

            // Actualizar solo los campos permitidos
            existente.Nombre = med.Nombre;
            existente.Tipo = med.Tipo;
            existente.FechaCaducidad = med.FechaCaducidad;
            existente.Stock = med.Stock;
            existente.IdProveedor = med.IdProveedor;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Medicamento actualizado correctamente." });
        }

        // 🔹 Eliminación lógica (no se borra, solo cambia estado)
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarMedicamento(int id)
        {
            var med = await _context.Medicamentos.FindAsync(id);
            if (med == null)
                return NotFound();

            med.Estado = false; // Borrado lógico
            await _context.SaveChangesAsync();

            return Ok(new { message = "Medicamento desactivado correctamente (borrado lógico)." });
        }
    }
}

