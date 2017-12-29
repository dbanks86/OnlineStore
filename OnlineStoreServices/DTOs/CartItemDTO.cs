namespace OnlineStoreServices.DTOs
{
    public class CartItemDTO
    {
        /// <summary>
        /// ID of cart item
        /// </summary>
        public int CartItemID { get; set; }

        /// <summary>
        /// quantity of cart item
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// ID of cart item product
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Name of cart item product
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Price of cart item product
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Remaining amount of product available to purchase
        /// </summary>
        public int StockCount { get; set; }

        /// <summary>
        /// States whether or not product is in stock
        /// If in stock, states whether or not stock is low
        /// </summary>
        public string StockMessage { get; set; }
    }
}
