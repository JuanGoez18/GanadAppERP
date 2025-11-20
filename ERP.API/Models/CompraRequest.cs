namespace ERP.API.Models
{
    public class CompraRequest
    {
        public List<int> GanadoIds { get; set; } = new();
        public int ClienteId { get; set; }
    }
}