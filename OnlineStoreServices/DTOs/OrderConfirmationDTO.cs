using OnlineStoreModels;
using SmartyStreets.USStreetApi;
using System;
using System.Collections.Generic;

namespace OnlineStoreServices.DTOs
{
    public class OrderConfirmationDTO
    {
        /// <summary>
        /// Contains order infomation returned from SmartyStreets address validation
        /// </summary>
        public Candidate Candidate { get; set; }

        /// <summary>
        /// First name of order
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Last name of order
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Date and time in which order was placed
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Cart items of user
        /// </summary>
        public IEnumerable<CartItem> CartItems { get; set; }

        /// <summary>
        /// Date order is expected to arrived at shipping address provided by user
        /// </summary>
        public int ExpectedDeliveryDays { get; set; }

        /// <summary>
        /// Name of selected shipping option
        /// </summary>
        public string ShippingOptionName { get; set; }

        /// <summary>
        /// Price of selected shipping option
        /// </summary>
        public decimal? ShippingPrice { get; set; }

        /// <summary>
        /// ID of selected shipping option
        /// </summary>
        public int ShippingOptionID { get; set; }
    }
}
