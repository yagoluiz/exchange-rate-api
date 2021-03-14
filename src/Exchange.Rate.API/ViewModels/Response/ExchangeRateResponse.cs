using System;

namespace Exchange.Rate.API.ViewModels.Response
{
    public class ExchangeRateResponse
    {
        public ExchangeRateResponse(decimal totalCost)
        {
            TotalCost = Decimal.Round(totalCost, 2);
        }

        /// <summary>
        /// Total cost (rounded to 2 decimal places)
        /// </summary>
        public decimal TotalCost { get; }
    }
}
