using System.Threading.Tasks;
using Exchange.Rate.Domain.Contracts;
using Exchange.Rate.Domain.Models.Services;
using Exchange.Rate.Infra.Services;
using Exchange.Rate.Integration.Tests.Clients;
using Xunit;

namespace Exchange.Rate.Integration.Tests
{
    [TraitAttribute("Foreign Exchange Rates", "Integration tests for Foreign Exchange Rates API resource")]
    public class ForeignExchangeRatesServiceTest
    {
        [Fact(DisplayName = "GET /latest Returns http status code 200")]
        public async Task GetLatestExchangeRatesAsyncWhenStatusCodeIsSuccessTest()
        {
            var httpClient = ForeignExchangeRatesHttpClient.HttpClient;
            var foreignExchangeRatesService = new ForeignExchangeRatesService(httpClient);

            var result = await foreignExchangeRatesService.GetLatestExchangeRateAsync(Currency.USD);

            Assert.IsType<ForeignExchangeRatesResponse>(result);
        }
    }
}
