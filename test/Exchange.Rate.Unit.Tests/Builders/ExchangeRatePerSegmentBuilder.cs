using Bogus;
using Exchange.Rate.Domain.Contracts;

namespace Exchange.Rate.Unit.Tests.Builders
{
    public static class ExchangeRatePerSegmentBuilder
    {
        public static ExchangeRatePerSegment ExchangeRatePerSegment =>
            new Faker<ExchangeRatePerSegment>()
            .CustomInstantiator(faker => new ExchangeRatePerSegment
            {
                Retail = faker.Finance.Amount(0, 1),
                Personnalite = faker.Finance.Amount(0, 1),
                Private = faker.Finance.Amount(0, 1),
            }).Generate();
    }
}
