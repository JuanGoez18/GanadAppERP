using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuProyecto.Modelos
{
    [Table("ventas")]
    public class Venta
    {
        [Key]
        [Column("id_ventas")]
        public int IdVentas { get; set; }

        [Column("fecha_venta")]
        public DateTime FechaVenta { get; set; }

        [Column("precio_total")]
        public decimal PrecioTotal { get; set; }

        [Column("codigo_arete")]
        public string CodigoArete { get; set; }

        [Column("cliente")]
        public int Cliente { get; set; }
    }
}
