using System;
using Exchange.Rate.Domain.Contracts;
using Exchange.Rate.Unit.Tests.Builders;
using Xunit;

namespace Exchange.Rate.Unit.Tests.Domain
{
    [TraitAttribute("Exchange Rate", "Unit tests for domain")]
    public class ExchangeRateTest
    {
        [Theory(DisplayName = "Calculate quote foreign currency")]
        [InlineData(10, 5.00)]
        [InlineData(50, 6.00)]
        [InlineData(100, 7.00)]
        public void CalculateQuoteForeignCurrencyTest(int amount, decimal foreignCurrencyConversionRate)
        {
            var exchangeRate = new ExchangeRate(amount, foreignCurrencyConversionRate);
            var calculate = exchangeRate.CalculateQuoteForeignCurrency();

            Assert.Equal(calculate, (amount * foreignCurrencyConversionRate));
        }

        [Theory(DisplayName = "Calculate foreign currency conversion when customer segment is Retail")]
        [InlineData(10, 5.00, CustomerSegment.RETAIL)]
        [InlineData(50, 6.00, CustomerSegment.RETAIL)]
        [InlineData(100, 7.00, CustomerSegment.RETAIL)]
        public void CalculateForeignCurrencyConversionWhenCustomerSegmentIsRetailTest(
            int amount,
            decimal foreignCurrencyConversionRate,
            CustomerSegment customerSegment
        )
        {
            var exchangeRatePerSegment = ExchangeRatePerSegmentBuilder.ExchangeRatePerSegment;
            var exchangeRate = new ExchangeRate(
                amount,
                foreignCurrencyConversionRate,
                customerSegment,
                exchangeRatePerSegment
            );
            var calculate = exchangeRate.CalculateForeignCurrencyConversion();

            Assert.Equal(
                calculate,
                (amount * foreignCurrencyConversionRate) * (1 + exchangeRatePerSegment.Retail)
            );
        }

        [Theory(DisplayName = "Calculate foreign currency conversion when customer segment is Personnalite")]
        [InlineData(10, 5.00, CustomerSegment.PERSONNALITE)]
        [InlineData(50, 6.00, CustomerSegment.PERSONNALITE)]
        [InlineData(100, 7.00, CustomerSegment.PERSONNALITE)]
        public void CalculateForeignCurrencyConversionWhenCustomerSegmentIsPersonnaliteTest(
            int amount,
            decimal foreignCurrencyConversionRate,
            CustomerSegment customerSegment
        )
        {
            var exchangeRatePerSegment = ExchangeRatePerSegmentBuilder.ExchangeRatePerSegment;
            var exchangeRate = new ExchangeRate(
                amount,
                foreignCurrencyConversionRate,
                customerSegment,
                exchangeRatePerSegment
            );
            var calculate = exchangeRate.CalculateForeignCurrencyConversion();

            Assert.Equal(
                calculate,
                (amount * foreignCurrencyConversionRate) * (1 + exchangeRatePerSegment.Personnalite)
            );
        }
        [Theory(DisplayName = "Calculate foreign currency conversion when customer segment is Private")]
        [InlineData(10, 5.00, CustomerSegment.PRIVATE)]
        [InlineData(50, 6.00, CustomerSegment.PRIVATE)]
        [InlineData(100, 7.00, CustomerSegment.PRIVATE)]
        public void CalculateForeignCurrencyConversionWhenCustomerSegmentIsPrivateTest(
            int amount,
            decimal foreignCurrencyConversionRate,
            CustomerSegment customerSegment
        )
        {
            var exchangeRatePerSegment = ExchangeRatePerSegmentBuilder.ExchangeRatePerSegment;
            var exchangeRate = new ExchangeRate(
                amount,
                foreignCurrencyConversionRate,
                customerSegment,
                exchangeRatePerSegment
            );
            var calculate = exchangeRate.CalculateForeignCurrencyConversion();

            Assert.Equal(
                calculate,
                (amount * foreignCurrencyConversionRate) * (1 + exchangeRatePerSegment.Private)
            );
        }

        [Fact(DisplayName = "Calculate foreign currency conversion when custom segment is null")]
        public void CalculateForeignCurrencyConversionWhenCustomSegmentIsNullTest()
        {
            var exchangeRatePerSegment = ExchangeRatePerSegmentBuilder.ExchangeRatePerSegment;
            var exchangeRate = new ExchangeRate(
                10,
                5.00m,
                null,
                exchangeRatePerSegment
            );

            Assert.Throws<ArgumentNullException>(() => exchangeRate.CalculateForeignCurrencyConversion());
        }

        [Fact(DisplayName = "Calculate foreign currency conversion when exchange rate per segment is null")]
        public void CalculateForeignCurrencyConversionWhenExchangeRatePerSegmentIsNullTest()
        {
            var exchangeRate = new ExchangeRate(
                10,
                5.00m,
                CustomerSegment.RETAIL
            );

            Assert.Throws<ArgumentNullException>(() => exchangeRate.CalculateForeignCurrencyConversion());
        }
    }
}
