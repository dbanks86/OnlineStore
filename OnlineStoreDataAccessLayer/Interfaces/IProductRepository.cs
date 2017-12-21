using OnlineStoreModels;
using System.Collections.Generic;

namespace OnlineStoreDataAccessLayer.Interfaces
{
    interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        void DeleteProduct(Product product);
        IEnumerable<Product> SearchProducts(string searchString);
    }
}
