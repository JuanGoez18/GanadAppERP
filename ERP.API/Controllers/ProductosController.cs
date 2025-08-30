using Microsoft.AspNetCore.Mvc;
using ERP.API.Models;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private static readonly List<Producto> productos = new()
        {
            new Producto { Id = 1, Nombre = "Ganado Bovino", Precio = 150000m, Stock = 10 },
            new Producto { Id = 2, Nombre = "Vacuna Aftosa", Precio = 20000m, Stock = 50 },
            new Producto { Id = 3, Nombre = "Concentrado 40kg", Precio = 120500m, Stock = 100 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Producto>> Get()
        {
            return Ok(productos);
        }
    }
}