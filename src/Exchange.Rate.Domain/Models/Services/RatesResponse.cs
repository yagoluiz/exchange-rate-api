using System.Text.Json.Serialization;

namespace Exchange.Rate.Domain.Models.Services
{
    public class RatesResponse
    {
        [JsonPropertyName("BRL")]
        public decimal BrazilianReal { get; set; }
    }
}
