using OnlineStoreModels;
using System.Collections.Generic;

namespace OnlineStoreServices.Interfaces
{
    interface IShippingService
    {
        IEnumerable<ShippingOption> GetAllShippingOptions();
        ShippingOption GetShippingOption(int id);
        void AddShippingOption(ShippingOption shippingOption);
        void UpdateShippingOption(ShippingOption shippingOption);
        void DeleteShippingOption(ShippingOption shippingOption);
        void DeleteShippingOption(int id);
    }
}
