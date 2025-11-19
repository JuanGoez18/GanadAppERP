using System.Net.Http.Json;

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
}