using SmartyStreets.USStreetApi;
using System.Collections.Generic;
using OnlineStoreModels;

namespace OnlineStore.ViewModels
{
    public class OrderConfirmationViewModel
    {
        /// <summary>
        /// Contains order infomation returned from SmartyStreets address validation
        /// </summary>
        public Candidate Candidate { get; set; }

        /// <summary>
        /// Order that was created after order was placed by user
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Cart items of user
        /// </summary>
        public List<CartItem> CartItems { get; set; }

        /// <summary>
        /// Shipping option selected by user
        /// </summary>
        public ShippingOption ShippingOption { get; set; }
    }
}