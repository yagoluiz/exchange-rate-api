using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Exchange.Rate.Domain.Contracts;
using Exchange.Rate.Domain.Interfaces.Services;
using Exchange.Rate.Domain.Models.Services;

namespace Exchange.Rate.Infra.Services
{
    public class ForeignExchangeRatesService : IForeignExchangeRatesService
    {
        private readonly HttpClient _httpClient;

        public ForeignExchangeRatesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ForeignExchangeRatesResponse> GetLatestExchangeRateAsync(Currency currency)
        {
            var response = await _httpClient.GetAsync($"latest?base={currency.ToString()}&symbols={Currency.BRL.ToString()}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var contentResponse = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<ForeignExchangeRatesResponse>(contentResponse);
        }
    }
}
