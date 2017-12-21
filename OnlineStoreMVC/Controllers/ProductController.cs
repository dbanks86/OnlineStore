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
                return RedirectToAction("Index", "Error");
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
                ProductViewModel model = new ProductViewModel();
                model.ProductDetailsDTO = new ProductDetailsDTO(services, productId);

                if (model.ProductDetailsDTO.Product.StockCount >= 4)
                {
                    model.QuantitiesList = new SelectList(Enumerable.Range(1, 4));
                }
                else
                {
                    model.QuantitiesList = new SelectList(Enumerable.Range(1, model.ProductDetailsDTO.Product.StockCount));
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                return RedirectToAction("Index", "Error");
            }
        }
    }
}