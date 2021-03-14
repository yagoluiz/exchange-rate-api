using System.Threading.Tasks;
using Exchange.Rate.API.Services.Interfaces;
using Exchange.Rate.API.ViewModels.Request;
using Exchange.Rate.API.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace Exchange.Rate.API.Controller
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/exchange-rates")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRatesService;

        public ExchangeRateController(IExchangeRateService exchangeRatesService)
        {
            _exchangeRatesService = exchangeRatesService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("quote")]
        public async Task<ActionResult<ExchangeRateResponse>> GetQuoteForeignCurrencyAsync(
            [FromQuery] QuoteForeignCurrencyRequest request
        )
        {
            return Ok(await _exchangeRatesService.GetQuoteForeignCurrencyAsync(request));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("conversion")]
        public async Task<ActionResult<ExchangeRateResponse>> GetForeignCurrencyConversionAsync(
            [FromQuery] ForeignCurrencyConversionRequest request
        )
        {
            return Ok(await _exchangeRatesService.GetForeignCurrencyConversionAsync(request));
        }
    }
}
