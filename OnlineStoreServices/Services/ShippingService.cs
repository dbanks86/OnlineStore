using OnlineStoreServices.Interfaces;
using System;
using System.Collections.Generic;
using OnlineStoreModels;
using OnlineStoreDataAccessLayer.Repositories;

namespace OnlineStoreServices.Services
{
    public class ShippingService : IShippingService
    {
        readonly Repositories repositories;

        public ShippingService(Repositories repositories)
        {
            this.repositories = repositories;
        }

        public IEnumerable<ShippingOption> GetAllShippingOptions()
        {
            try
            {
                return repositories.ShippingRepository.GetAllShippingOptions();
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
                return null;
            }
        }

        public ShippingOption GetShippingOption(int id)
        {
            try
            {
                return repositories.ShippingRepository.GetShippingOption(id);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
                return null;
            }
        }

        public void AddShippingOption(ShippingOption shippingOption)
        {
            try
            {
                repositories.ShippingRepository.AddShippingOption(shippingOption);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        public void UpdateShippingOption(ShippingOption shippingOption)
        {
            try
            {
                repositories.ShippingRepository.UpdateShippingOption(shippingOption);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        public void DeleteShippingOption(ShippingOption shippingOption)
        {
            try
            {
                repositories.ShippingRepository.DeleteShippingOption(shippingOption);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        public void DeleteShippingOption(int id)
        {
            repositories.ShippingRepository.DeleteShippingOption(id);
        }
    }
}
