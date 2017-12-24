namespace OnlineStoreServices.DTOs
{
    public class RemoveCartItemDTO
    {
        public string SubTotal { get; set; }
        public int CartItemsCount { get; set; }

        /// <summary>
        /// ID of product removed from cart
        /// </summary>
        public int ProductID { get; set; }
    }
}
