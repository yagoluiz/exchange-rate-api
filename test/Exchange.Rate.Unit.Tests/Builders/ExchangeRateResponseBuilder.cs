using Bogus;
using Exchange.Rate.API.ViewModels.Response;

namespace Exchange.Rate.Unit.Tests.Builders
{
    public static class ExchangeRateResponseBuilder
    {
        public static ExchangeRateResponse ExchangeRateResponse =>
            new Faker<ExchangeRateResponse>()
            .CustomInstantiator(faker => new ExchangeRateResponse(
                totalCost: faker.Finance.Amount(1, 1000)
            )).Generate();
    }
}
