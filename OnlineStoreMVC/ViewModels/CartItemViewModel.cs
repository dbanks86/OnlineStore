using OnlineStoreServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.ViewModels
{
    public class CartItemViewModel : CartItemDTO
    {
        /// <summary>
        /// Sets css class of stock message based on stock count of product
        /// </summary>
        public string StockMessageCssClass { get; set; }
    }
}