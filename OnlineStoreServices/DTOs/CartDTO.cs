using System.Collections.Generic;

namespace OnlineStoreServices.DTOs
{
    public class CartDTO<T> where T : CartItemDTO
    {
        public IEnumerable<T> CartItemDTOs { get; set; }
        public int CartItemsCount { get; set; }
        public decimal SubTotal { get; set; }
    }
}
