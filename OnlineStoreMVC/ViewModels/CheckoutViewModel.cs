using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineStoreModels;
using OnlineStoreServices.DTOs;

namespace OnlineStore.ViewModels
{
    /// <summary>
    /// ViewModel for the Checkout page
    /// </summary>
    public class CheckoutViewModel : CheckoutDTO<CartItemViewModel>
    {
        /// <summary>
        /// First name of order
        /// </summary>
        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of order
        /// </summary>
        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Street address of order
        /// </summary>
        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Street Address")]
        public string Address { get; set; }

        /// <summary>
        /// Unit number of residence (if one exist)
        /// </summary>
        [Display(Name = "Apt, Suite, etc (optional)")]
        public string AptSuiteEtc { get; set; }

        /// <summary>
        /// City of order
        /// </summary>
        [Required(ErrorMessage = "{0} required")]
        public string City { get; set; }

        /// <summary>
        /// State of order
        /// </summary>
        [Required(ErrorMessage = "{0} required")]
        public string State { get; set; }

        /// <summary>
        /// Zip Code of order
        /// </summary>
        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Price of selected shipping option
        /// </summary>
        public decimal? SelectedShippingOptionPrice { get; set; }

        /// <summary>
        /// ID of selected shipping option
        /// Can be used to specify initial selected shipping option on GET request of page
        /// </summary>
        public int ShippingOptionID { get; set; }
    }
}