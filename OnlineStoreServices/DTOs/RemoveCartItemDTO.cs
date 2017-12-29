namespace OnlineStoreServices.DTOs
{
    public class RemoveCartItemDTO
    {
        /// <summary>
        /// Sum of each cart item price * each cart item quantity
        /// </summary>
        public int CartItemsCount { get; set; }

        /// <summary>
        /// Sum of each cart item price * cart item quantity
        /// </summary>
        public string SubTotal { get; set; }

        /// <summary>
        /// ID of product removed from cart
        /// </summary>
        public int ProductID { get; set; }
    }
}
