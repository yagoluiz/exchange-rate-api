using System;

namespace Exchange.Rate.Domain.Contracts
{
    public class ExchangeRate
    {
        private readonly int _amount;
        private readonly decimal _foreignCurrencyConversionRate;
        private readonly CustomerSegment? _customerSegment;
        private readonly ExchangeRatePerSegment _exchangeRatePerSegment;

        public ExchangeRate(
            int amount,
            decimal foreignCurrencyConversionRate,
            CustomerSegment? customerSegment = null,
            ExchangeRatePerSegment exchangeRatePerSegment = null
        )
        {
            _amount = amount;
            _foreignCurrencyConversionRate = foreignCurrencyConversionRate;
            _customerSegment = customerSegment;
            _exchangeRatePerSegment = exchangeRatePerSegment;
        }

        public decimal CalculateQuoteForeignCurrency() => _amount * _foreignCurrencyConversionRate;

        public decimal CalculateForeignCurrencyConversion()
        {
            if (_customerSegment == null)
            {
                throw new ArgumentNullException(nameof(_customerSegment));
            }

            return (_amount * _foreignCurrencyConversionRate) * (1 + GetExchangeRatePerSegment());
        }

        private decimal GetExchangeRatePerSegment()
        {
            if (_exchangeRatePerSegment == null)
            {
                throw new ArgumentNullException(nameof(_exchangeRatePerSegment));
            }

            var exchangeRate = _customerSegment switch
            {
                CustomerSegment.RETAIL => _exchangeRatePerSegment.Retail,
                CustomerSegment.PRIVATE => _exchangeRatePerSegment.Private,
                CustomerSegment.PERSONNALITE => _exchangeRatePerSegment.Personnalite,
                _ => throw new ArgumentException(nameof(_customerSegment))
            };

            return exchangeRate;
        }
    }
}
