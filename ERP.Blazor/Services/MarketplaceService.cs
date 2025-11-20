using System.Net.Http.Json;
using ERP.Blazor.Models;

public class MarketplaceService
{
    private readonly HttpClient _http;

    public MarketplaceService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<GanadoMarketplace>> ObtenerGanadoMarketplace()
    {
        return await _http.GetFromJsonAsync<List<GanadoMarketplace>>(
            "api/Marketplace/ganado"
        );
    }

    public async Task<RespuestaCompra> ProcesarCompraAsync(object request)
    {
        var resp = await _http.PostAsJsonAsync("api/marketplace/comprar", request);
        if (!resp.IsSuccessStatusCode)
        {
            return new RespuestaCompra { Exito = false, Mensaje = "Error al procesar la compra." };
        }

        var data = await resp.Content.ReadFromJsonAsync<RespuestaCompra>();
        return data ?? new RespuestaCompra { Exito = true };
    }
}