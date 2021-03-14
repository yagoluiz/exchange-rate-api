using System.ComponentModel.DataAnnotations;
using Exchange.Rate.Domain.Contracts;

namespace Exchange.Rate.API.ViewModels.Request
{
    public class ForeignCurrencyConversionRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Required]
        public int Amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Required]
        public Currency Currency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Required]
        public CustomerSegment CustomerSegment { get; set; }
    }
}
