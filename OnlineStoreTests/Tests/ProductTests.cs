using NUnit.Framework;
using OnlineStoreTests.Services;
using System.Linq;

namespace OnlineStoreTests.Tests
{
    /// <summary>
    /// A test case body is divided into three sections "AAA".
    /// "AAA" denotes the Arrange, Act, and Assert.
    /// Arrange: In Arrange section, we will initialize everything which we are required to run the test case. It includes any dependencies and data needed.
    /// Act: In Act section, we called the business logic method which behavior we want to test.
    /// Assert: Specify the criteria for passing the test case. If these criteria passed, that means test case is passed else failed.
    /// </summary>
    [TestFixture]
    public class ProductTests
    {
        [TestCase]
        public void When_Products_Expects_AllProductsContainingText_C()
        {
            //Arrange
            ProductService productService = new ProductService();

            //Act
            productService.AddInitialProducts();
            var products = productService.SearchProducts("C");

            //Assert
            Assert.AreEqual(products.Count(), 5);
        }

        [TestCase]
        public void When_Products_Expects_NoProductsFoundByText()
        {
            //Arrange
            ProductService productService = new ProductService();

            //Act
            productService.AddInitialProducts();
            var products = productService.SearchProducts("a390r3jf0f32");

            //Assert
            Assert.AreEqual(products.Count(), 0);
        }

        [TestCase]
        public void When_Products_Expects_OneProductAdded()
        {
            //Arrange
            ProductService productService = new ProductService();

            //Act
            productService.AddInitialProducts();

            var firstProductCount = productService.GetAllProducts().Count();

            Product product = new Product();
            product.Name = "Product1";
            product.Price = 4.99m;
            product.StockCount = 10;
            product.Enabled = true;

            productService.AddProduct(product);

            var secondProductCount = productService.GetAllProducts().Count();

            //Assert
            Assert.IsTrue(secondProductCount == firstProductCount + 1);
        }

        [TestCase]
        public void When_Products_Expects_OneProductDeleted()
        {
            //Arrange
            ProductService productService = new ProductService();

            //Act
            productService.AddInitialProducts();

            //delete product from database
            productService.DeleteProduct(1);

            //attempt to get product after deletion of product
            var product = productService.GetProduct(1);

            //Assert
            Assert.IsNull(product);
        }

        [TestCase]
        public void When_Products_Expects_OneProductUpdated()
        {
            //Arrange
            ProductService productService = new ProductService();

            //Act
            productService.AddInitialProducts();

            var product = productService.GetProduct(1);

            product.Name = "Some other name";

            //update product name in database
            productService.UpdateProduct(product);

            //get product after updating product
            product = productService.GetProduct(1);

            //Assert
            Assert.AreEqual(product.Name, "Some other name");
        }

        [TestCase]
        public void When_Products_Expects_NoProductsFoundById()
        {
            //Arrange
            ProductService productService = new ProductService();

            //Act
            productService.AddInitialProducts();

            var product = productService.GetProduct(-1);

            //Assert
            Assert.IsNull(product);
        }
    }
}
