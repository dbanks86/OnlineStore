using OnlineStoreDataAccessLayer.Interfaces;
using OnlineStoreModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStoreDataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly OnlineStoreEntities dbContext;

        public ProductRepository(OnlineStoreEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return dbContext.Products.ToList();
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
                return null;
            }
        }

        public Product GetProduct(int id)
        {
            try
            {
                return dbContext.Products.SingleOrDefault(product => product.ProductID == id);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
                return null;
            }
        }

        public void AddProduct(Product product)
        {
            try
            {
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                dbContext.Entry(product).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        public void DeleteProduct(int id)
        {
            var product = GetProduct(id);
            this.DeleteProduct(product);
        }

        public void DeleteProduct(Product product)
        {
            try
            {
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        public IEnumerable<Product> SearchProducts(string searchString)
        {
            try
            {
                return dbContext.Products.Where(p => p.Name.Contains(searchString)).ToList();
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
                return null;
            }
        }
    }
}
