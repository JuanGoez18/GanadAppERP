using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("roles")]
    public class RolDTO
    {
        [Key]
        [Column("id_roles")]
        public int Id_Roles { get; set; }

        [Column("nombre_rol")]
        public string NombreRol { get; set; } = string.Empty;
    }
}