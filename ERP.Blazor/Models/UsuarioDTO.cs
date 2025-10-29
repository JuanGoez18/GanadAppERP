using System.Text.Json.Serialization;

namespace ERP.Blazor.Models
{
    public class UsuarioDTO
    {
        [JsonPropertyName("id_Usuarios")]
        public int Id_Usuarios { get; set; }

        [JsonIgnore]
        public int Id
        {
            get => Id_Usuarios;
            set => Id_Usuarios = value;
        }

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

        [JsonPropertyName("estado_Cuenta")]
        public bool Estado_Cuenta { get; set; }

        [JsonIgnore]
        public bool EstadoCuenta
        {
            get => Estado_Cuenta;
            set => Estado_Cuenta = value;
        }

        [JsonPropertyName("rol")]
        public int Rol { get; set; }
    }
}
