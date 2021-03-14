using System.Threading.Tasks;
using Exchange.Rate.Domain.Contracts;
using Exchange.Rate.Domain.Models.Services;
using Exchange.Rate.Infra.Services;
using Exchange.Rate.Integration.Tests.Clients;
using Xunit;

namespace Exchange.Rate.Integration.Tests
{
    [TraitAttribute("External Foreign Exchange Rates", "Integration tests for external Foreign Exchange Rates API")]
    public class ForeignExchangeRatesServiceTest
    {
        [Fact(DisplayName = "GET /latest Returns http status code 200")]
        public async Task GetLatestExchangeRatesWhenStatusCodeIsSuccessTest()
        {
            var client = ForeignExchangeRatesHttpClient.HttpClient;
            var service = new ForeignExchangeRatesService(client);

            var result = await service.GetLatestExchangeRateAsync(Currency.USD);

            Assert.IsType<ForeignExchangeRatesResponse>(result);
        }
    }
}
