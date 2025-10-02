using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("sesiones")]
    public class Sesion
    {
        [Key]
        [Column("id_sesiones")]
        public int Id_Sesion { get; set; }

        [Column("id_usuario")]
        public int Id_Usuario { get; set; }

        [Column("fecha_ingreso")]
        public DateTime Fecha_Ingreso { get; set; }

        [Column("hora_ingreso")]
        public TimeSpan Hora_Ingreso { get; set; }

        // Relación con Usuario
        [ForeignKey("Id_Usuario")]
        public Usuario Usuario { get; set; }
    }
}
