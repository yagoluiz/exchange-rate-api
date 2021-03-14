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
        /// 
        /// </summary>
        /// <value></value>
        public decimal TotalCost { get; }
    }
}
