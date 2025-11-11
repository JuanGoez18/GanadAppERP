using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("medicamentos")]
    public class Medicamento
    {
        [Key]
        [Column("id_medicamentos")]
        public int Id { get; set; }

        [Column("nombre")]
        [Required, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Column("tipo")]
        [Required, MaxLength(50)]
        public string Tipo { get; set; } = string.Empty;

        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        [Column("fecha_caducidad")]
        public DateTime FechaCaducidad { get; set; } = DateTime.UtcNow;

        [Column("stock")]
        public int Stock { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;

        [Column("id_proveedor")]
        public int? IdProveedor { get; set; }
    }
}
