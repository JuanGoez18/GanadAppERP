using ERP.API.Data;
using ERP.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketplaceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MarketplaceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("ganado")]
        public async Task<ActionResult<IEnumerable<GanadoMarketplaceDto>>> GetGanadoParaVenta()
        {
            var data = await _context.Ganado
                .Where(g => g.Estado == true && g.ParaVenta == true)
                .Select(g => new GanadoMarketplaceDto
                {
                    IdGanado = g.IdGanado,
                    CodigoArete = g.CodigoArete,
                    Raza = g.Raza,
                    Edad = g.Edad,
                    Peso = g.Peso,
                    EstadoSalud = g.EstadoSalud,
                    Imagen = g.Imagen,
                    StockVenta = g.StockVenta,
                    PrecioUnitario = g.PrecioUnitario
                })
                .ToListAsync();

            return Ok(data);
        }
    }
}
