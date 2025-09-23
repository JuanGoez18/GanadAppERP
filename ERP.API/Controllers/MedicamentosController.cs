using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.API.Data;
using ERP.API.Models;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicamentosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MedicamentosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _context.Medicamentos.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.Medicamentos.FindAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Medicamento model)
        {
            _context.Medicamentos.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id_Medicamento }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Medicamento model)
        {
            if (id != model.Id_Medicamento) return BadRequest();
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Medicamentos.FindAsync(id);
            if (item == null) return NotFound();
            _context.Medicamentos.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
