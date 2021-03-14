using System;
using System.Text.Json.Serialization;

namespace Exchange.Rate.Domain.Models.Services
{
    public class ForeignExchangeRatesResponse
    {
        [JsonPropertyName("rates")]
        public RatesResponse Rates { get; set; }
        [JsonPropertyName("base")]
        public string Base { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}
