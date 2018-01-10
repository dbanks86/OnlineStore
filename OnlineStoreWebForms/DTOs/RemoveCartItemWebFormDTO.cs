using OnlineStoreServices.DTOs;

namespace OnlineStore.DTOs
{
    public class RemoveCartItemWebFormDTO : RemoveCartItemDTO
    {
        /// <summary>
        /// sets text of sub total label thats display current amount of cart items after cart item is removed
        /// </summary>
        public string SubTotalLabel { get; set; }

        /// <summary>
        /// Update cart link that displays current amount of cart items at top right corner of page after cart item is removed
        /// </summary>
        public string CartLabel { get; set; }
    }
}