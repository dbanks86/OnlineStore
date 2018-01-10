using OnlineStoreServices.DTOs;

namespace OnlineStore.DTOs
{
    public class CartItemWebFormDTO : CartItemDTO
    {
        /// <summary>
        /// Sets css class of stock message based on stock count of product
        /// </summary>
        public string StockMessageCssClass { get; set; }
    }
}