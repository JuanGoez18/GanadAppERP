using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("usuarios", Schema = "public")]
    public class Usuario
    {
        [Key]
        [Column("id_usuarios")]
        public int id_usuario { get; set; }

        [Column("nombre")]
        public string nombre { get; set; }

        [Column("correo")]
        public string correo { get; set; }

        [Column("contraseña")]
        public string PasswordHash { get; set; }

        [Column("id_roles")]
        public int id_roles { get; set; }       // ✅ FK a tabla roles

        [ForeignKey("id_roles")]
        public Rol Rol { get; set; }            // ✅ navegación a Rol

        public ICollection<Sesion> Sesiones { get; set; } // ✅ relación con Sesiones
    }
}

