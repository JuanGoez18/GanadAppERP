using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models;

[Table("usuarios", Schema = "public")]
public class Usuario
{
    [Key]
    [Column("id_usuarios")]
    public int Id { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; }

    [Column("correo")]
    public string Correo { get; set; }

    [Column("contraseña")]
    public string PasswordHash { get; set; }

    [Column("rol")]
    public int RoleId { get; set; }

    [ForeignKey("RoleId")]  //posibles cambios
    public Rol Role { get; set; }

    public ICollection<Sesion> Sesiones { get; set; }
}
