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
                return RedirectToAction(Constants.CONTROLLER_ACTION_ERROR, Constants.CONTROLLER_NAME_ERROR);
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
                ProductViewModel productViewModel = new ProductViewModel();

                ProductDetailsDTO productDetailsDto = new ProductDetailsDTO();
                productDetailsDto.Product = services.ProductService.GetProduct(productId);

                var stockCount = productDetailsDto.Product.StockCount;

                if (stockCount > 5)
                {
                    productDetailsDto.StockMessage = "In Stock";
                    productViewModel.StockMessageCssClass = Constants.CSS_CLASS_STOCK_GREATER_THAN_FIVE;
                    productViewModel.QuantitiesList = new SelectList(Enumerable.Range(1, 4));
                }
                else if (stockCount == 0)
                {
                    productDetailsDto.StockMessage = "Out of Stock";
                    productViewModel.StockMessageCssClass = Constants.CSS_CLASS_STOCK_EQUALS_ZERO;
                }
                else
                {
                    productDetailsDto.StockMessage = string.Format("Only {0} in stock", stockCount);
                    productViewModel.StockMessageCssClass = Constants.CSS_CLASS_STOCK_INCLUSIVELY_BETWEEN_ONE_AND_FIVE;
                    productViewModel.QuantitiesList = new SelectList(Enumerable.Range(1, stockCount > 3 ? 4 : stockCount));
                }
                return View(productViewModel);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                return RedirectToAction(Constants.CONTROLLER_ACTION_ERROR, Constants.CONTROLLER_NAME_ERROR);
            }
        }
    }
}