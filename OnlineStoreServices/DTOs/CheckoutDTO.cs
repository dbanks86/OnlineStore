using OnlineStoreModels;
using System.Collections.Generic;

namespace OnlineStoreServices.DTOs
{
    public class CheckoutDTO<T> : CartDTO<T> where T : CartItemDTO
    {
        /// <summary>
        /// Percentage of subtotal + shipping price
        /// Currently using sales tax rate of Georgia
        /// When considering customer across the US, a sales tax API (such as taxjar) to calculate sales tax
        /// </summary>
        public decimal? SalesTax { get; set; }

        /// <summary>
        /// subtotal + shipping price + sales tax
        /// </summary>
        public decimal OrderTotal { get; set; }

        /// <summary>
        /// List of all available shipping options
        /// </summary>
        public IEnumerable<ShippingOption> ShippingOptions { get; set; }

        /// <summary>
        /// Expected date for order to arrive at shipping address
        /// </summary>
        public string ArrivalDate { get; set; }
    }
}
