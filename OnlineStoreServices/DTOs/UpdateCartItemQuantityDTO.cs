namespace OnlineStoreServices.DTOs
{
    public class UpdateCartItemQuantityDTO
    {
        /// <summary>
        /// Sum of each cart item price * each cart item quantity
        /// </summary>
        public string CartItemsCount { get; set; }

        /// <summary>
        /// Sum of each cart item price * cart item quantity
        /// </summary>
        public string SubTotal { get; set; }
    }
}
