using System.Threading.Tasks;
using Exchange.Rate.Domain.Contracts;
using Exchange.Rate.Domain.Models.Services;

namespace Exchange.Rate.Domain.Interfaces.Services
{
    public interface IForeignExchangeRatesService
    {
        Task<ForeignExchangeRatesResponse> GetLatestExchangeRateAsync(Currency currency);
    }
}
