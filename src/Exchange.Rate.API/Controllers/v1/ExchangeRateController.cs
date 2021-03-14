using System.Threading.Tasks;
using Exchange.Rate.API.Services.Interfaces;
using Exchange.Rate.API.ViewModels.Request;
using Exchange.Rate.API.ViewModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exchange.Rate.API.Controller.v1
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/exchange-rates")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;

        public ExchangeRateController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        /// <summary>
        /// Get quote foreign currency 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /quote?amount=100&amp;currency=USD
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns the newly quote item</response>
        /// <response code="400">If external foreign exchange Rates API is null</response> 
        /// <response code="500">Internal server error</response>
        [HttpGet("quote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ExchangeRateResponse>> GetQuoteForeignCurrencyAsync(
            [FromQuery] QuoteForeignCurrencyRequest request
        )
        {
            return Ok(await _exchangeRateService.GetQuoteForeignCurrencyAsync(request));
        }

        /// <summary>
        /// Get foreign currency conversion
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /conversion?amount=100&amp;currency=USD&amp;customerSegment=RETAIL
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns the newly conversion item</response>
        /// <response code="400">If external foreign exchange Rates API is null</response> 
        /// <response code="500">Internal server error</response>
        [HttpGet("conversion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ExchangeRateResponse>> GetForeignCurrencyConversionAsync(
            [FromQuery] ForeignCurrencyConversionRequest request
        )
        {
            return Ok(await _exchangeRateService.GetForeignCurrencyConversionAsync(request));
        }
    }
}
