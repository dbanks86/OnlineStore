using OnlineStoreServices.DTOs;

namespace OnlineStore.ViewModels
{
    public class UpdateCartItemQuantityViewModel : UpdateCartItemQuantityDTO
    {
        /// <summary>
        /// sets text of sub total label thats display current amount of cart items after cart item quantity is changed
        /// </summary>
        public string SubTotalLabel { get; set; }

        /// <summary>
        /// Update cart link that displays current amount of cart items at top right corner of page after cart item quantity is changed
        /// </summary>
        public string CartLabel { get; set; }
    }
}