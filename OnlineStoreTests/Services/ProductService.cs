using System.Collections.Generic;
using System.Linq;

namespace OnlineStoreTests.Services
{
    public class ProductService
    {
        readonly OnlineStoreMockEntities dbContext = new OnlineStoreMockEntities();

        public ProductService()
        {
            
        }

        /// <summary>
        /// delete products from database and adds default set of products
        /// </summary>
        public void AddInitialProducts()
        {
            //Add initial product to OnlineStore Mock database
            dbContext.AddInitialProducts();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return dbContext.Products.ToList();
        }

        public Product GetProduct(int id)
        {
            return dbContext.Products.SingleOrDefault(product => product.ProductID == id);
        }

        public void AddProduct(Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            dbContext.Entry(product).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = GetProduct(id);
            this.DeleteProduct(product);
        }

        public void DeleteProduct(Product product)
        {
            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
        }

        public IEnumerable<Product> SearchProducts(string searchString)
        {
            var products = GetAllProducts();
            return products.Where(p => p.Name.Contains(searchString));
        }
    }
}
