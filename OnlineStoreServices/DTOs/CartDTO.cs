using System.Collections.Generic;

namespace OnlineStoreServices.DTOs
{
    public class CartDTO<T> where T : CartItemDTO
    {
        /// <summary>
        /// List of products, along with quantites added by user
        /// </summary>
        public IEnumerable<T> CartItemDTOs { get; set; }

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
