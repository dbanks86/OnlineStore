using System;
using System.Collections.Generic;
using OnlineStoreDataAccessLayer.Interfaces;
using OnlineStoreModels;
using System.Linq;

namespace OnlineStoreDataAccessLayer.Repositories
{
    public class ShippingRepository : IShippingRepository
    {
        readonly OnlineStoreEntities dbContext;

        public ShippingRepository(OnlineStoreEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<ShippingOption> GetAllShippingOptions()
        {
            try
            {
                return dbContext.ShippingOptions.ToList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
                return null;
            }
        }

        public ShippingOption GetShippingOption(int id)
        {
            try
            {
                return dbContext.ShippingOptions.SingleOrDefault(shippingOption => shippingOption.ShippingOptionID == id);
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
                return null;
            }
        }

        public void AddShippingOption(ShippingOption shippingOption)
        {
            try
            {
                dbContext.ShippingOptions.Add(shippingOption);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
            }
        }

        public void UpdateShippingOption(ShippingOption shippingOption)
        {
            try
            {
                dbContext.Entry(shippingOption).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
            }
        }

        public void DeleteShippingOption(ShippingOption shippingOption)
        {
            try
            {
                dbContext.ShippingOptions.Remove(shippingOption);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorManager.LogException(ex);
            }
        }

        public void DeleteShippingOption(int id)
        {
            var shippingOption = GetShippingOption(id);
            this.DeleteShippingOption(shippingOption);
        }
    }
}
