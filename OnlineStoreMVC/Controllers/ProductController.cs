using OnlineStore.Managers;
using OnlineStore.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using OnlineStoreServices.Services;
using OnlineStoreServices.DTOs;

namespace OnlineStore.Controllers
{ 
    public class ProductController : Controller
    {
        private readonly Services services = new Services();

        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Return list of products containing string specified by user
        /// </summary>
        /// <param name="searchString">string to search products</param>
        /// <returns></returns>
        public ActionResult Browse(string searchString)
        {
            try
            {
                var products = services.ProductService.SearchProducts(searchString);
                return View(products);
            }
            catch(Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                return RedirectToAction(Constants.CONTROLLER_ACTION_INDEX, Constants.CONTROLLER_ERROR);
            }
        }

        /// <summary>
        /// Display page containing information of a product
        /// </summary>
        /// <param name="productId">id of product</param>
        /// <returns></returns>
        public ActionResult Details(int productId)
        {
            try
            {
                var product = services.ProductService.GetProduct(productId);

                ProductDetailsViewModel productDetailsViewModel = new ProductDetailsViewModel();
                productDetailsViewModel.ProductID = product.ProductID;
                productDetailsViewModel.ProductName = product.Name;
                productDetailsViewModel.Price = product.Price;
                productDetailsViewModel.StockCount = product.StockCount;

                var stockCount = productDetailsViewModel.StockCount;

                if (stockCount > 5)
                {
                    productDetailsViewModel.StockMessage = "In Stock";
                    productDetailsViewModel.StockMessageCssClass = Constants.CSS_CLASS_STOCK_GREATER_THAN_FIVE;
                    productDetailsViewModel.QuantitiesList = new SelectList(Enumerable.Range(1, 4));
                }
                else if (stockCount == 0)
                {
                    productDetailsViewModel.StockMessage = "Out of Stock";
                    productDetailsViewModel.StockMessageCssClass = Constants.CSS_CLASS_STOCK_EQUALS_ZERO;
                }
                else
                {
                    productDetailsViewModel.StockMessage = string.Format("Only {0} in stock", stockCount);
                    productDetailsViewModel.StockMessageCssClass = Constants.CSS_CLASS_STOCK_INCLUSIVELY_BETWEEN_ONE_AND_FIVE;
                    productDetailsViewModel.QuantitiesList = new SelectList(Enumerable.Range(1, stockCount > 3 ? 4 : stockCount));
                }
                return View(productDetailsViewModel);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                return RedirectToAction(Constants.CONTROLLER_ACTION_INDEX, Constants.CONTROLLER_ERROR);
            }
        }
    }
}