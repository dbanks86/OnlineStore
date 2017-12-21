using OnlineStoreDataAccessLayer.Repositories;
using OnlineStoreModels;
using OnlineStoreServices.Interfaces;
using System;
using System.Collections.Generic;

namespace OnlineStoreServices.Services
{
    public class ProductService : IProductService
    {
        readonly Repositories repositories;

        public ProductService(Repositories repositories)
        {
            this.repositories = repositories;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return repositories.ProductRepository.GetAllProducts();
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
                return repositories.ProductRepository.GetProduct(id);
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
                repositories.ProductRepository.AddProduct(product);
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
                repositories.ProductRepository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
            }
        }

        public void DeleteProduct(int id)
        {
            repositories.ProductRepository.DeleteProduct(id);
        }

        public void DeleteProduct(Product product)
        {
            try
            {
                repositories.ProductRepository.DeleteProduct(product);
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
                return repositories.ProductRepository.SearchProducts(searchString);
            }
            catch (Exception ex)
            {
                ErrorService.LogException(ex);
                return null;
            }
        }
    }
}
