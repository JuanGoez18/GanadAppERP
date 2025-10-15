using System.Text.Json.Serialization;

namespace ERP.Blazor.Models
{
    public class UsuarioDTO
    {
        [JsonPropertyName("id_Usuarios")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [JsonPropertyName("edad")]
        public int Edad { get; set; }

        [JsonPropertyName("correo")]
        public string Correo { get; set; } = string.Empty;

        [JsonPropertyName("rolNombre")]
        public string? RolNombre { get; set; }

        [JsonPropertyName("estadoCuenta")]
        public bool EstadoCuenta { get; set; }

        [JsonPropertyName("rolId")]
        public int RolId { get; set; }
    }
}