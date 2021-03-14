using Bogus;
using Exchange.Rate.Domain.Contracts;
using Exchange.Rate.Domain.Models.Services;

namespace Exchange.Rate.Unit.Tests.Builders
{
    public static class ForeignExchangeRatesResponseBuilder
    {
        public static ForeignExchangeRatesResponse ForeignExchangeRatesResponse(Currency currency) =>
            new Faker<ForeignExchangeRatesResponse>()
            .CustomInstantiator(faker => new ForeignExchangeRatesResponse
            {
                Rates = new RatesResponse
                {
                    BrazilianReal = faker.Finance.Amount(1, 1000)
                },
                Base = currency.ToString(),
                Date = faker.Date.Past()
            }).Generate();
    }
}
