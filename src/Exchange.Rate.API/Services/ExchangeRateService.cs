using System.Threading.Tasks;
using Exchange.Rate.API.Services.Interfaces;
using Exchange.Rate.API.ViewModels.Request;
using Exchange.Rate.API.ViewModels.Response;
using Exchange.Rate.Domain.Contracts;
using Exchange.Rate.Domain.Interfaces.Notifications;
using Exchange.Rate.Domain.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace Exchange.Rate.API.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IForeignExchangeRatesService _foreignExchangeRatesService;
        private readonly IDomainNotification _domainNotification;
        private readonly ExchangeRatePerSegment _exchangeRatePerSegment;

        public ExchangeRateService(
            IForeignExchangeRatesService foreignExchangeRatesService,
            IDomainNotification domainNotification,
            IOptions<ExchangeRatePerSegment> exchangeRatePerSegment)
        {
            _foreignExchangeRatesService = foreignExchangeRatesService;
            _domainNotification = domainNotification;
            _exchangeRatePerSegment = exchangeRatePerSegment.Value;
        }

        public async Task<ExchangeRateResponse> GetQuoteForeignCurrencyAsync(QuoteForeignCurrencyRequest request)
        {
            var foreignExchangeRate = await _foreignExchangeRatesService.GetLatestExchangeRateAsync(request.Currency);

            if (foreignExchangeRate == null)
            {
                _domainNotification.AddNotification("QuoteForeignCurrency", "Foreign Exchange Rates API request error");

                return null;
            }

            var exchangeRate = new ExchangeRate(request.Amount, foreignExchangeRate.Rates.BrazilianReal);
            var calculateQuoteForeignCurrency = exchangeRate.CalculateQuoteForeignCurrency();

            return new ExchangeRateResponse(calculateQuoteForeignCurrency);
        }

        public async Task<ExchangeRateResponse> GetForeignCurrencyConversionAsync(ForeignCurrencyConversionRequest request)
        {
            var foreignExchangeRate = await _foreignExchangeRatesService.GetLatestExchangeRateAsync(request.Currency);

            if (foreignExchangeRate == null)
            {
                _domainNotification.AddNotification("ForeignCurrencyConversion", "Foreign Exchange Rates API request error");

                return null;
            }

            var exchangeRate = new ExchangeRate(
                request.Amount,
                foreignExchangeRate.Rates.BrazilianReal,
                request.CustomerSegment,
                _exchangeRatePerSegment
            );
            var calculateForeignCurrencyConversion = exchangeRate.CalculateForeignCurrencyConversion();

            return new ExchangeRateResponse(calculateForeignCurrencyConversion);
        }
    }
}
