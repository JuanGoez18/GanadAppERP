using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuProyecto.Modelos
{
    [Table("ganado")]
    public class Ganado
    {
        [Key]
        [Column("id_ganado")]
        public int IdGanado { get; set; }

        [Column("codigo_arete")]
        [Required(ErrorMessage = "El código de arete es obligatorio")]
        public string CodigoArete { get; set; }

        [Column("raza")]
        [Required(ErrorMessage = "La raza es obligatoria")]
        public string Raza { get; set; }

        [Column("edad")]
        [Required(ErrorMessage = "La edad es obligatoria")]
        public int Edad { get; set; }

        [Column("peso")]
        [Required(ErrorMessage = "El peso es obligatorio")]
        public decimal Peso { get; set; }

        [Column("estado_salud")]
        [Required(ErrorMessage = "El estado de salud es obligatorio")]
        public string EstadoSalud { get; set; }

        [Column("supervisor")]
        [Required(ErrorMessage = "Debe asignarse un supervisor al registro del ganado")]
        public int Supervisor { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;

        // ¿Está disponible para Marketplace?
        [Column("para_venta")]
        public bool ParaVenta { get; set; } = false;

        [Column("vendido")]
        public bool Vendido { get; set; } = false;

        [Column("precio_unitario")]
        public decimal PrecioUnitario { get; set; } = 0;

        [Column("imagen")]
        public string? Imagen { get; set; }
    }
}
