using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.API.Data;
using ERP.API.Models;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;
    public UsuariosController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _context.Usuarios.Include(u => u.Role).ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var usuario = await _context.Usuarios
                                     .Include(u => u.Role)
                                     .FirstOrDefaultAsync(u => u.Id == id);
        return usuario == null ? NotFound() : Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Usuario model)
    {
        _context.Usuarios.Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Usuario model)
    {
        if (id != model.Id) return BadRequest();
        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
