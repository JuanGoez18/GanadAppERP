using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.API.Data;
using ERP.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // => api/usuarios si la clase se llama UsuariosController
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsuariosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            var usuarios = await _context.Usuarios
                                         .Include(u => u.Rol)   // si quieres traer rol
                                         .ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await _context.Usuarios
                                        .Include(u => u.Rol)
                                        .FirstOrDefaultAsync(u => u.id_usuario == id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }
    }
}


