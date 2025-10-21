using System.Text.Json.Serialization;

namespace ERP.Blazor.Models
{
    public class RolDTO
    {
        [JsonPropertyName("id_Roles")]
        public int Id_Roles { get; set; }

        [JsonPropertyName("nombreRol")]
        public string NombreRol { get; set; } = string.Empty;
    }
}