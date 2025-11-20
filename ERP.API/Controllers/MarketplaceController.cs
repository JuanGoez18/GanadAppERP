using ERP.API.Data;
using ERP.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuProyecto.Modelos;
using ERP.API.Models;

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
                .Where(g => g.Estado == true && g.ParaVenta == true && g.Vendido == false)
                .Select(g => new GanadoMarketplaceDto
                {
                    IdGanado = g.IdGanado,
                    CodigoArete = g.CodigoArete,
                    Raza = g.Raza,
                    Edad = g.Edad,
                    Peso = g.Peso,
                    EstadoSalud = g.EstadoSalud,
                    Imagen = g.Imagen,
                    PrecioUnitario = g.PrecioUnitario
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpPost("comprar")]
        public async Task<IActionResult> ProcesarCompra([FromBody] CompraRequest request)
        {
            if (request == null || request.GanadoIds == null || !request.GanadoIds.Any())
                return BadRequest(new { mensaje = "No se enviaron ganados para comprar." });

            // Verificar que el cliente sea un id válido (>0)
            if (request.ClienteId <= 0)
                return BadRequest(new { mensaje = "Cliente inválido." });

            // Obtener los ganados solicitados y verificar que no estén ya vendidos
            var ganados = await _context.Ganado
                .Where(g => request.GanadoIds.Contains(g.IdGanado) && g.Vendido == false)
                .ToListAsync();

            if (ganados.Count != request.GanadoIds.Count)
                return BadRequest(new { mensaje = "Uno o más ganados no existen o ya fueron vendidos." });

            // Crear la venta (guardamos primero para obtener el Id)
            var venta = new Venta
            {
                FechaVenta = DateTime.UtcNow,
                Cliente = request.ClienteId,
                PrecioTotal = 0m,
                CodigoArete = string.Join(",", ganados.Select(g => g.CodigoArete)) // opcional
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync(); // aquí se genera venta.IdVentas

            // Insertar detalles y marcar ganado como vendido, acumulando el total
            foreach (var g in ganados)
            {
                var detalle = new DetalleVenta
                {
                    Cantidad = 1,
                    NombreProducto = g.CodigoArete ?? g.Raza,
                    PrecioUnitario = g.PrecioUnitario,
                    Subtotal = g.PrecioUnitario,
                    IdVenta = venta.IdVentas
                };

                _context.DetalleVenta.Add(detalle);

                // marcar ganado vendido
                g.Vendido = true;
                g.ParaVenta = false;

                // acumular total
                venta.PrecioTotal += g.PrecioUnitario;
            }

            // Guardar cambios (detalles + actualización de ganados + venta actualizada)
            await _context.SaveChangesAsync();

            return Ok(new { Exito = true, mensaje = "Compra procesada exitosamente.", idVenta = venta.IdVentas });
        }
    }
}
