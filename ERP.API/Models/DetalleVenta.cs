using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuProyecto.Modelos
{
    [Table("detalle_venta")]
    public class DetalleVenta
    {
        [Key]
        [Column("id_detalleventa")]
        public int IdDetalleVenta { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("nombre_producto")]
        public string NombreProducto { get; set; }

        [Column("precio_unitario")]
        public decimal PrecioUnitario { get; set; }

        [Column("subtotal")]
        public decimal Subtotal { get; set; }

        [Column("id_venta")]
        public int IdVenta { get; set; }
    }
}
