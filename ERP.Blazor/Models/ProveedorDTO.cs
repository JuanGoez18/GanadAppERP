using System.Text.Json.Serialization;

namespace ERP.Blazor.Models
{
    public class ProveedorDTO
    {
        [JsonPropertyName("idProveedores")]
        public int IdProveedores { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [JsonPropertyName("edad")]
        public int Edad { get; set; }

        [JsonPropertyName("cc")]
        public string CC { get; set; } = string.Empty;

        [JsonPropertyName("sexo")]
        public string Sexo { get; set; } = string.Empty;

        [JsonPropertyName("telefono")]
        public string Telefono { get; set; } = string.Empty;

        [JsonPropertyName("entidad")]
        public string Entidad { get; set; } = string.Empty;

        [JsonPropertyName("direccion")]
        public string Direccion { get; set; } = string.Empty; 
        [JsonPropertyName("estado")]
        public bool Estado { get; set; } = true;
    }
}
