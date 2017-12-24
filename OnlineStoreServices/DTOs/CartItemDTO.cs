namespace OnlineStoreServices.DTOs
{
    public class CartItemDTO
    {
        public int CartItemID { get; set; }
        public int Quantity { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockCount { get; set; }

        /// <summary>
        /// States whether or not product is in stock
        /// If in stock, states whether or not stock is low
        /// </summary>
        public string StockMessage { get; set; }
    }
}
