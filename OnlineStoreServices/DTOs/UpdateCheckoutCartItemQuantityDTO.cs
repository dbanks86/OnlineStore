namespace OnlineStoreServices.DTOs
{
    public class UpdateCheckoutCartItemQuantityDTO : UpdateCartItemQuantityDTO
    {
        /// <summary>
        /// cart item price * cart item quantity
        /// </summary>
        public string CartItemPrice { get; set; }

        /// <summary>
        /// Percentage of subtotal + shipping price
        /// Currently using sales tax rate of Georgia
        /// When considering customer across the US, a sales tax API (such as taxjar) to calculate sales tax
        /// </summary>
        public string SalesTax { get; set; }

        /// <summary>
        /// subtotal + shipping price + sales tax
        /// </summary>
        public string OrderTotal { get; set; }
    }
}
