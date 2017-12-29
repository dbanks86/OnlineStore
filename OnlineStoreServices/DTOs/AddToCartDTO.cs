namespace OnlineStoreServices.DTOs
{
    public class AddToCartDTO
    {
        /// <summary>
        /// ID of cart item product
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Name of cart item product
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Sum of each cart item price * each cart item quantity
        /// </summary>
        public int CartItemsCount { get; set; }

        /// <summary>
        /// Sum of each cart item price * cart item quantity
        /// </summary>
        public decimal SubTotal { get; set; }
    }
}
