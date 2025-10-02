using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.API.Data;
using ERP.API.Models;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly AppDbContext _context;
    public RolesController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _context.Roles.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var rol = await _context.Roles.FindAsync(id);
        return rol == null ? NotFound() : Ok(rol);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Rol model)
    {
        _context.Roles.Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Rol model)
    {
        if (id != model.Id) return BadRequest();
        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol == null) return NotFound();
        _context.Roles.Remove(rol);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
