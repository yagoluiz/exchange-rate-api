using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Exchange.Rate.Integration.Tests.Clients
{
    public static class ForeignExchangeRatesHttpClient
    {
        private static HttpClient _httpClient;

        public static HttpClient HttpClient
        {
            get
            {
                if (_httpClient == null)
                {
                    _httpClient = new HttpClient();
                    _httpClient.BaseAddress = new Uri("https://api.exchangeratesapi.io/");
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                return _httpClient;
            }
        }
    }
}
