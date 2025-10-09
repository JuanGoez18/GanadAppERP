using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuarios")]
        public int Id_Usuarios { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Column("edad")]
        public int Edad { get; set; }

        [Column("correo")]
        public string Correo { get; set; } = string.Empty;

        [Column("contraseña")]
        public string Contrasena { get; set; } = string.Empty;

        [Column("cc")]
        public string CC { get; set; } = string.Empty;

        [Column("estado_civil")]
        public string Estado_Civil { get; set; } = string.Empty;

        [Column("sexo")]
        public string Sexo { get; set; } = string.Empty;

        [Column("estado_cuenta")]
        public bool Estado_Cuenta { get; set; }

        [Column("rol")]
        public int Rol { get; set; }
    }
}