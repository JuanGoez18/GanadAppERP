using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("veterinarios")]
    public class Veterinario
    {
        [Key]
        [Column("id_veterinarios")]
        public int IdVeterinarios { get; set; }

        [Column("nombre")]
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Column("apellido")]
        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Column("edad")]
        public int Edad { get; set; }

        [Column("cc")]
        [Required]
        [MaxLength(20)]
        public string CC { get; set; } = string.Empty;

        [Column("sexo")]
        [MaxLength(10)]
        public string Sexo { get; set; } = string.Empty;

        [Column("direccion")]
        [MaxLength(150)]
        public string Direccion { get; set; } = string.Empty;

        [Column("telefono")]
        [MaxLength(20)]
        public string? Telefono { get; set; } 

        [Column("entidad")]
        [MaxLength(100)]
        public string Entidad { get; set; } = string.Empty;

        [Column("estado")]
        public bool Estado { get; set; } = true;
    }
}


