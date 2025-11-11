using System;
using System.Text.Json.Serialization;

namespace ERP.Blazor.Models
{
    public class MedicamentoDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("tipo")]
        public string Tipo { get; set; } = string.Empty;

        [JsonPropertyName("fechaRegistro")]
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;


        [JsonPropertyName("fechaCaducidad")]
        public DateTime FechaCaducidad { get; set; } = DateTime.UtcNow;


        [JsonPropertyName("stock")]
        public int Stock { get; set; }

        [JsonPropertyName("idProveedor")]
        public int? IdProveedor { get; set; }

        [JsonPropertyName("estado")]
        public bool Estado { get; set; } = true;
    }
}

