using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("roles", Schema = "public")]
    public class Rol
    {
        [Key]
        [Column("id_roles")]
        public int Id { get; set; }

        [Column("nombre_rol")]
        public string Nombre { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }

}
