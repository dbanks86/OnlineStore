using OnlineStoreModels;
using OnlineStoreServices.Interfaces;
using OnlineStoreServices.Managers;
using System;

namespace OnlineStoreServices.DTOs
{
    /// <summary>
    /// DTO for a product along with any necessary additional information
    /// </summary>
    public class ProductDetailsDTO
    {
        public Product Product { get; set; }

        /// <summary>
        /// States whether or not product is in stock
        /// If in stock, states whether or not stock is low
        /// </summary>
        public string StockMessage { get; set; }
    }
}
