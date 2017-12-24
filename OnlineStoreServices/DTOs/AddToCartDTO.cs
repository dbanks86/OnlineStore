namespace OnlineStoreServices.DTOs
{
    public class AddToCartDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CartItemsCount { get; set; }
        public decimal SubTotal { get; set; }
    }
}
