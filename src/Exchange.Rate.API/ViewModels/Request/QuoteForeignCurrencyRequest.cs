using System.ComponentModel.DataAnnotations;
using Exchange.Rate.Domain.Contracts;

namespace Exchange.Rate.API.ViewModels.Request
{
    public class QuoteForeignCurrencyRequest
    {
        /// <summary>
        /// Purchase amount
        /// </summary>
        [Required]
        public int Amount { get; set; }

        /// <summary>
        /// Currency abbreviation
        /// </summary>
        [Required]
        public Currency Currency { get; set; }
    }
}
