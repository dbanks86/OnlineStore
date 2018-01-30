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
        /// ID of order
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// Date and time in which order was placed
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Cart items of user
        /// </summary>
        public IEnumerable<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Date order is expected to arrived at shipping address provided by user
        /// </summary>
        public int ExpectedDeliveryDays { get; set; }

        /// <summary>
        /// Sum of each cart item price * cart item quantity
        /// </summary>
        public decimal SubTotal { get; set; }

        /// <summary>
        /// ID of selected shipping option
        /// </summary>
        public int ShippingOptionID { get; set; }

        /// <summary>
        /// Name of selected shipping option
        /// </summary>
        public string ShippingOptionName { get; set; }

        /// <summary>
        /// Price of selected shipping option
        /// </summary>
        public decimal? ShippingPrice { get; set; }

        /// <summary>
        /// Percentage of subtotal + shipping price
        /// Currently using sales tax rate of Georgia
        /// When considering customer across the US, a sales tax API (such as taxjar) to calculate sales tax
        /// </summary>
        public decimal SalesTax { get; set; }

        /// <summary>
        /// subtotal + shipping price + sales tax
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Tracking number of order
        /// </summary>
        public string TrackingNumber { get; set; }
    }
}
