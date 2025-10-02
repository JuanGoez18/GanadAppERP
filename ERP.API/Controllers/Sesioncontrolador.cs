using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.API.Data;
using ERP.API.Models;

[ApiController]
[Route("api/[controller]")]
public class SesionesController : ControllerBase
{
    private readonly AppDbContext _context;
    public SesionesController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _context.Sesiones.Include(s => s.Usuario).ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var sesion = await _context.Sesiones.Include(s => s.Usuario)
                                            .FirstOrDefaultAsync(s => s.Id == id);
        return sesion == null ? NotFound() : Ok(sesion);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Sesion model)
    {
        _context.Sesiones.Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Sesion model)
    {
        if (id != model.Id) return BadRequest();
        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sesion = await _context.Sesiones.FindAsync(id);
        if (sesion == null) return NotFound();
        _context.Sesiones.Remove(sesion);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

