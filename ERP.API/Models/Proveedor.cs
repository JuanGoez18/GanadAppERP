using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("proveedores")]
    public class Proveedor
    {
        [Key]
        [Column("id_proveedores")]
        public int IdProveedores { get; set; }

        [Column("nombre")]
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Column("apellido")]
        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; }

        [Column("edad")]
        public int Edad { get; set; }

        [Column("cc")]
        [Required]
        [MaxLength(20)]
        public string CC { get; set; }

        [Column("sexo")]
        [MaxLength(10)]
        public string Sexo { get; set; }

        [Column("direccion")]
        [MaxLength(150)]
        public string Direccion { get; set; }

        [Column("telefono")]
        [MaxLength(20)]
        public string Telefono { get; set; }

        [Column("entidad")]
        [MaxLength(100)]
        public string Entidad { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true; 
    }
}
