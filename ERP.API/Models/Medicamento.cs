using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("medicamentos")]
    public class Medicamento
    {
        [Key]
        [Column("id_medicamento")]
        public int Id_Medicamento { get; set; }

        [Column("nombre")]
        [Required]
        [MaxLength(100)]
        public required string Nombre { get; set; }

        [Column("tipo")]
        [Required]
        [MaxLength(50)]
        public required string Tipo { get; set; }

        [Column("fecha_vencimiento")]
        public DateTime Fecha_vencimiento { get; set; }

        [Column("stock")]
        public int Stock { get; set; }
        [Column("estado")]
        public int estado { get; set; }
    }
}
