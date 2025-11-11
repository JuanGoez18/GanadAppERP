namespace ERP.Blazor.Models 
{
    public class GanadoDTO
    {
        public int IdGanado { get; set; }
        public string CodigoArete { get; set; } = string.Empty;
        public string Raza { get; set; } = string.Empty;
        public int Edad { get; set; }
        public double Peso { get; set; }
        public string EstadoSalud { get; set; } = string.Empty;
        public int Supervisor { get; set; } // 👈 este debe ser int, no string
        public bool Estado { get; set; } = true;
    }
}
