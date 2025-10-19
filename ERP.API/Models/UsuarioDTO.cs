using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("usuarios")] 
    public class UsuarioDTO
    {
        [Key]
        [Column("id_usuarios")]
        public int Id_Usuarios { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Column("correo")]
        public string Correo { get; set; } = string.Empty;

        [Column("rol")]
        public string Rol { get; set; } = string.Empty;
    }
}