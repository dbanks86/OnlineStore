namespace OnlineStoreServices.DTOs
{
    /// <summary>
    /// Used in AJAX Requests to methods.
    /// Populates JSON objects with string properties.
    /// String properties are used to populate HTML element in UI
    /// </summary>
    public class UpdateShippingOptionDTO
    {
        /// <summary>
        /// Sum of each cart item price * cart item quantity
        /// </summary>
        public string SubTotal { get; set; }

        /// <summary>
        /// Percentage of subtotal + shipping price
        /// Currently using sales tax rate of Georgia
        /// When considering customer across the US, a sales tax API (such as taxjar) to calculate sales tax
        /// </summary>
        public string SalesTax { get; set; }

        /// <summary>
        /// subtotal + shipping price + sales tax
        /// </summary>
        public string Total { get; set; }

        /// <summary>
        /// Price of selected shipping option
        /// </summary>
        public string ShippingPrice { get; set; }
    }
}
