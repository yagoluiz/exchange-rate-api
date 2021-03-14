using System.Threading.Tasks;
using Exchange.Rate.API.Services;
using Exchange.Rate.API.ViewModels.Request;
using Exchange.Rate.API.ViewModels.Response;
using Exchange.Rate.Domain.Contracts;
using Exchange.Rate.Domain.Interfaces.Notifications;
using Exchange.Rate.Domain.Interfaces.Services;
using Exchange.Rate.Domain.Models.Services;
using Exchange.Rate.Unit.Tests.Builders;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Exchange.Rate.Unit.Tests.Services
{
    [TraitAttribute("Exchange Rate Service", "Unit tests for service")]
    public class ExchangeRateServiceTest
    {
        private readonly Mock<IForeignExchangeRatesService> _foreignExchangeRatesServiceMock;
        private readonly Mock<IDomainNotification> _domainNotificationMock;
        private readonly Mock<IOptions<ExchangeRatePerSegment>> _exchangeRatePerSegmentMock;

        public ExchangeRateServiceTest()
        {
            _foreignExchangeRatesServiceMock = new Mock<IForeignExchangeRatesService>();
            _domainNotificationMock = new Mock<IDomainNotification>();
            _exchangeRatePerSegmentMock = new Mock<IOptions<ExchangeRatePerSegment>>();
        }

        [Fact(DisplayName = "Get quote foreign currency when response is not null")]
        public async Task GetQuoteForeignCurrencyWhenResponseIsNotNullTest()
        {
            var request = new QuoteForeignCurrencyRequest
            {
                Amount = 10,
                Currency = Currency.USD
            };

            _foreignExchangeRatesServiceMock.Setup(
                setup => setup.GetLatestExchangeRateAsync(request.Currency)
            ).ReturnsAsync(ForeignExchangeRatesResponseBuilder.ForeignExchangeRatesResponse(request.Currency));

            var service = new ExchangeRateService(
                _foreignExchangeRatesServiceMock.Object,
                _domainNotificationMock.Object,
                _exchangeRatePerSegmentMock.Object
            );
            var method = await service.GetQuoteForeignCurrencyAsync(request);

            Assert.IsType<ExchangeRateResponse>(method);
        }

        [Fact(DisplayName = "Get quote foreign currency when external foreign exchange rates is null")]
        public async Task GetQuoteForeignCurrencyWhenExternalForeignExchangeRatesIsNullTest()
        {
            var request = new QuoteForeignCurrencyRequest
            {
                Amount = 10,
                Currency = Currency.USD
            };
            ForeignExchangeRatesResponse response = null;

            _foreignExchangeRatesServiceMock.Setup(
                setup => setup.GetLatestExchangeRateAsync(request.Currency)
            ).ReturnsAsync(response);

            var service = new ExchangeRateService(
                _foreignExchangeRatesServiceMock.Object,
                _domainNotificationMock.Object,
                _exchangeRatePerSegmentMock.Object
            );
            var method = await service.GetQuoteForeignCurrencyAsync(request);

            Assert.Null(method);
        }

        [Fact(DisplayName = "Get foreign currency conversion when response is not null")]
        public async Task GetForeignCurrencyConversionWhenResponseIsNotNullTest()
        {
            var request = new ForeignCurrencyConversionRequest
            {
                Amount = 10,
                Currency = Currency.USD,
                CustomerSegment = CustomerSegment.RETAIL
            };

            _foreignExchangeRatesServiceMock.Setup(
                setup => setup.GetLatestExchangeRateAsync(request.Currency)
            ).ReturnsAsync(ForeignExchangeRatesResponseBuilder.ForeignExchangeRatesResponse(request.Currency));

            _exchangeRatePerSegmentMock.Setup(setup => setup.Value)
                .Returns(ExchangeRatePerSegmentBuilder.ExchangeRatePerSegment);

            var service = new ExchangeRateService(
                _foreignExchangeRatesServiceMock.Object,
                _domainNotificationMock.Object,
                _exchangeRatePerSegmentMock.Object
            );
            var method = await service.GetForeignCurrencyConversionAsync(request);

            Assert.IsType<ExchangeRateResponse>(method);
        }

        [Fact(DisplayName = "Get foreign currency conversion when external foreign exchange rates is null")]
        public async Task GetForeignCurrencyConversionWhenExternalForeignExchangeRatesIsNullTest()
        {
            var request = new ForeignCurrencyConversionRequest
            {
                Amount = 10,
                Currency = Currency.USD,
                CustomerSegment = CustomerSegment.RETAIL
            };
            ForeignExchangeRatesResponse response = null;

            _foreignExchangeRatesServiceMock.Setup(
                setup => setup.GetLatestExchangeRateAsync(request.Currency)
            ).ReturnsAsync(response);

            var service = new ExchangeRateService(
                _foreignExchangeRatesServiceMock.Object,
                _domainNotificationMock.Object,
                _exchangeRatePerSegmentMock.Object
            );
            var method = await service.GetForeignCurrencyConversionAsync(request);

            Assert.Null(method);
        }
    }
}
