using System.Text.Json;

namespace TestAssignment.Services
{
    public class IPGeolocationService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "eadc3b14f21e4ab19e44029889c9b8cb";
        private const string ApiUrl = "https://api.ipgeolocation.io/ipgeo?apiKey={0}&ip={1}";

        public IPGeolocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetCountryByIPAsync(string ipAddress)
        {
            var url = string.Format(ApiUrl, ApiKey, ipAddress);
            var response = await _httpClient.GetStringAsync(url);

            using var doc = JsonDocument.Parse(response);
            return doc.RootElement.GetProperty("country_code2").GetString(); 
        }
    }
}
