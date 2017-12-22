using System.Collections.Generic;
using System.Web.Mvc;
using OnlineStoreServices.DTOs;

namespace OnlineStore.ViewModels
{
    /// <summary>
    /// ViewModel for the Product Details page
    /// </summary>
    public class ProductDetailsViewModel : ProductDetailsDTO
    {
        /// <summary>
        /// Quantity selected by user
        /// </summary>
        public int SelectedQuantity { get; set; }

        /// <summary>
        /// List of possible quantities to select from
        /// </summary>
        public IEnumerable<SelectListItem> QuantitiesList { get; set; }

        /// <summary>
        /// Sets css class of stock message based on stock count of product
        /// </summary>
        public string StockMessageCssClass { get; set; }
    }
}