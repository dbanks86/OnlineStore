using OnlineStoreModels;
using OnlineStoreServices.Interfaces;
using OnlineStoreServices.Managers;
using System;

namespace OnlineStoreServices.DTOs
{
    /// <summary>
    /// DTO for a product along with any necessary additional information
    /// </summary>
    public class ProductDetailsDTO
    {
        private readonly IServices services;
        private readonly Lazy<Product> product;
        private readonly int productId;

        public ProductDetailsDTO(IServices services, int productId)
        {
            this.services = services;
            this.productId = productId;
            product = new Lazy<Product>(() => services.ProductService.GetProduct(productId));
        }

        /// <summary>
        /// Product in which details are shown for
        /// </summary>
        public Product Product
        {
            get
            {
                return this.product.Value;
            }
        }

        /// <summary>
        /// States whether or not product is in stock
        /// If in stock, states whether or not stock is low
        /// </summary>
        public string StockMessage
        {
            get
            {
                return ProductManager.GetProductStockMessage(Product.StockCount);
            }
        }
    }
}
