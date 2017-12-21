using OnlineStoreModels;
using System.Collections.Generic;

namespace OnlineStoreServices.Interfaces
{
    interface IProductService
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
