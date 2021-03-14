using System.Threading.Tasks;
using Exchange.Rate.API.Controller.v1;
using Exchange.Rate.API.Services.Interfaces;
using Exchange.Rate.API.ViewModels.Request;
using Exchange.Rate.Domain.Contracts;
using Exchange.Rate.Unit.Tests.Builders;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Exchange.Rate.Unit.Tests.Controllers.v1
{
    [TraitAttribute("Exchange Rate Controller", "Unit tests for controller")]
    public class ExchangeRateControllerTest
    {
        private readonly Mock<IExchangeRateService> _exchangeRateServiceMock;

        public ExchangeRateControllerTest()
        {
            _exchangeRateServiceMock = new Mock<IExchangeRateService>();
        }

        [Fact(DisplayName = "Get quote foreign currency when response is success")]
        public async Task GetQuoteForeignCurrencyWhenResponseIsSuccessTest()
        {
            var request = new QuoteForeignCurrencyRequest
            {
                Amount = 10,
                Currency = Currency.USD
            };

            _exchangeRateServiceMock.Setup(setup => setup.GetQuoteForeignCurrencyAsync(request))
                .ReturnsAsync(ExchangeRateResponseBuilder.ExchangeRateResponse);

            var controller = new ExchangeRateController(_exchangeRateServiceMock.Object);
            var service = await controller.GetQuoteForeignCurrencyAsync(request);

            Assert.IsType<OkObjectResult>(service.Result);
        }

        [Fact(DisplayName = "Get foreign currency conversion when response is success")]
        public async Task GetForeignCurrencyConversionWhenResponseIsSuccessTest()
        {
            var request = new ForeignCurrencyConversionRequest
            {
                Amount = 10,
                Currency = Currency.USD,
                CustomerSegment = CustomerSegment.RETAIL
            };

            _exchangeRateServiceMock.Setup(setup => setup.GetForeignCurrencyConversionAsync(request))
                .ReturnsAsync(ExchangeRateResponseBuilder.ExchangeRateResponse);

            var controller = new ExchangeRateController(_exchangeRateServiceMock.Object);
            var service = await controller.GetForeignCurrencyConversionAsync(request);

            Assert.IsType<OkObjectResult>(service.Result);
        }
    }
}
