using System.Collections.Generic;
using System.Web.Mvc;
using OnlineStoreModels;
using OnlineStoreServices.DTOs;

namespace OnlineStore.ViewModels
{
    public class CartViewModel : CartDTO<CartItemViewModel>
    {
        public Dictionary<int, SelectList> QuantitiesList { get; set; }
    }
}