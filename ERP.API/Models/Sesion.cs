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
        public int id_sesion { get; set; }

        [Column("id_usuario")]
        public int id_usuario { get; set; }

        [Column("fecha_ingreso")]
        public DateTime Fecha_Ingreso { get; set; }

        [Column("hora_ingreso")]
        public TimeSpan Hora_Ingreso { get; set; }

        // Relación con Usuario
        [ForeignKey("id_usuario")]
        public Usuario Usuario { get; set; }
    }
}
