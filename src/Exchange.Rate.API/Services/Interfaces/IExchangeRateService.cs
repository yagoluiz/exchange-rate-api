using System.Threading.Tasks;
using Exchange.Rate.API.ViewModels.Request;
using Exchange.Rate.API.ViewModels.Response;

namespace Exchange.Rate.API.Services.Interfaces
{
    public interface IExchangeRateService
    {
        Task<ExchangeRateResponse> GetQuoteForeignCurrencyAsync(QuoteForeignCurrencyRequest request);
        Task<ExchangeRateResponse> GetForeignCurrencyConversionAsync(ForeignCurrencyConversionRequest request);
    }
}
