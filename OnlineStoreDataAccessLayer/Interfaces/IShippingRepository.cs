using OnlineStoreModels;
using System.Collections.Generic;

namespace OnlineStoreDataAccessLayer.Interfaces
{
    interface IShippingRepository
    {
        IEnumerable<ShippingOption> GetAllShippingOptions();
        ShippingOption GetShippingOption(int id);
        void AddShippingOption(ShippingOption shippingOption);
        void UpdateShippingOption(ShippingOption shippingOption);
        void DeleteShippingOption(ShippingOption shippingOption);
        void DeleteShippingOption(int id);
    }
}
