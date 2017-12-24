using OnlineStore.Managers;
using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using OnlineStoreServices.Services;
using OnlineStoreModels;
using OnlineStoreServices.DTOs;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        readonly Services services = new Services();

        // GET: ShoppingCart
        /// <summary>
        /// Display page of shopping cart of user
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                //get cart
                var userEmail = CookieManager.GetEmailFromUserCookie();

                var cartViewModel = new CartViewModel
                {
                    QuantitiesList = new Dictionary<int, SelectList>()
                };

                cartViewModel.CartItemDTOs = services.CartService.GetUserCartItems(userEmail, Constants.DATABASE_TABLE_PRODUCTS).Select(cartItem => CartManager.GetNewCartItemViewModel(cartItem));
                cartViewModel.CartItemsCount = cartViewModel.CartItemDTOs.Sum(cartItemDto => cartItemDto.Quantity);
                cartViewModel.SubTotal = cartViewModel.CartItemDTOs.Sum(cartItemDto => cartItemDto.Price * cartItemDto.Quantity);

                foreach (var cartItemDto in cartViewModel.CartItemDTOs)
                {
                    //create quantity drop down for each cart item and pre-select cart item quantity stored in database
                    cartViewModel.QuantitiesList[cartItemDto.CartItemID] = new SelectList(Enumerable.Range(1, 50), cartItemDto.Quantity);
                }
                return View(cartViewModel);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                return RedirectToAction(Constants.CONTROLLER_ACTION_INDEX, Constants.CONTROLLER_ERROR);
            }
        }

        /// <summary>
        /// Adds product to cart
        /// </summary>
        /// <returns></returns>
        public ActionResult AddToCart(ProductDetailsViewModel model)
        {
            try
            {
                var userEmail = CookieManager.GetEmailFromUserCookie();
                
                AddToCartDTO addToCartViewModel;

                //This would mean that the user has logged in from the AddToCart page (using login link on top right of page).
                //Therefore, the ProductViewModel parameter quantity value will be zero in this exceptional case.
                //Just create AddToCartViewModel object and return View passing in AddToCartViewModel object without modify cart of user logging in
                if (model.SelectedQuantity == 0)
                {
                    var userCartItems = services.CartService.GetUserCartItems(userEmail);

                    //create associated viewmodel and pass into view
                    addToCartViewModel = new AddToCartDTO();
                    addToCartViewModel.ProductID = model.ProductID;
                    addToCartViewModel.ProductName = model.Name;
                    addToCartViewModel.CartItemsCount = userCartItems.Count();
                    addToCartViewModel.SubTotal = userCartItems.Sum(ci => ci.Product.Price * ci.Quantity);
                    return View(addToCartViewModel);
                }

                // Get the matching cart and product instances
                var cartItem = services.CartService.GetCartItem(userEmail, model.ProductID);

                if (cartItem == null)
                {
                    // Create a new cart item if no cart item exists
                    cartItem = new CartItem
                    {
                        ProductID = model.ProductID,
                        Email = userEmail,
                        Quantity = model.SelectedQuantity,
                        DateCreated = DateTime.Now,
                    };

                    services.CartService.AddCartItem(cartItem);
                }
                else
                {
                    // If the item does exist in the cart, 
                    // then increase cart item quantity by quantity parameter amount
                    cartItem.Quantity += model.SelectedQuantity;
                    services.CartService.UpdateCartItem(cartItem);
                }

                var cartItems = services.CartService.GetUserCartItems(userEmail, Constants.DATABASE_TABLE_PRODUCTS);

                //create associated viewmodel and pass into view
                addToCartViewModel = new AddToCartDTO();
                addToCartViewModel.ProductID = cartItem.ProductID;
                addToCartViewModel.ProductName = cartItem.Product.Name;
                addToCartViewModel.CartItemsCount = cartItems.Count();
                addToCartViewModel.SubTotal = cartItems.Sum(ci => ci.Product.Price * ci.Quantity);

                return View(addToCartViewModel);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                return RedirectToAction(Constants.CONTROLLER_ACTION_INDEX, Constants.CONTROLLER_ERROR);
            }
        }

        /// <summary>
        /// Remove cart items for shopping cart
        /// </summary>
        /// <param name="productId">id of product</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveFromCart(int productId)
        {
            try
            {
                var userEmail = CookieManager.GetEmailFromUserCookie();
                var cartItem = services.CartService.GetCartItem(userEmail, productId);

                RemoveCartItemViewModel removeCartItemViewModel = new RemoveCartItemViewModel();
                removeCartItemViewModel.ProductID = productId;

                //NOTE: cartItem.Product becomes null once cartItem is removed from storeDB.CartItems ("storeDB.CartItems.Remove(cartItem)" is executed)
                //Therefore, any property using cartItem.Product MUST be set before "storeDB.CartItems.Remove(cartItem)" is executed
                services.CartService.DeleteCartItem(cartItem);

                //Get cart items of user after cart item is removed
                var userCartItems = services.CartService.GetUserCartItems(userEmail, Constants.DATABASE_TABLE_PRODUCTS);

                //set viewmodel properties based on remaining count of items after product is removed from cart
                if (userCartItems.Any())
                {
                    removeCartItemViewModel.CartItemsCount = 0;
                    removeCartItemViewModel.SubTotal = string.Format("{0:C}", 0);
                    removeCartItemViewModel.SubTotalLabel = string.Format("Subtotal ({0} {1})", 0, "items");
                    removeCartItemViewModel.CartLabel = string.Format("Cart ({0})", 0);
                }
                else
                {
                    int cartItemsCount = userCartItems.Sum(ci => ci.Quantity);

                    removeCartItemViewModel.CartItemsCount = cartItemsCount;
                    removeCartItemViewModel.SubTotal = string.Format("{0:C}", userCartItems.Sum(ci => ci.Product.Price * ci.Quantity));
                    removeCartItemViewModel.SubTotalLabel = string.Format("Subtotal ({0} {1})", cartItemsCount, cartItemsCount == 1 ? "item" : "items");
                    removeCartItemViewModel.CartLabel = string.Format("Cart ({0})", cartItemsCount);
                }

                //return JSON object to be used for AJAX
                return Json(removeCartItemViewModel);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                return RedirectToAction(Constants.CONTROLLER_ACTION_INDEX, Constants.CONTROLLER_ERROR);
            }
        }

        /// <summary>
        /// Updates quantity of cart item
        /// </summary>
        /// <param name="productId">id of product</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateCartItemQuantity(int productId, int quantity)
        {
            try
            {
                var userEmail = CookieManager.GetEmailFromUserCookie();
                var cartItem = services.CartService.GetCartItem(userEmail, productId);

                //update quantity of of cart item to quantity parameter value
                cartItem.Quantity = quantity;
                services.CartService.UpdateCartItem(cartItem);

                //Get cart items of user with cart item quantity updated
                var userCartItems = services.CartService.GetUserCartItems(userEmail, Constants.DATABASE_TABLE_PRODUCTS);
                var cartItemsCount = userCartItems.Sum(ci => ci.Quantity);
                var subTotal = userCartItems.Sum(ci => ci.Product.Price * ci.Quantity);

                //create viewmodel for associated view and pass into view
                UpdateCartItemQuantityViewModel updateCartItemQuantityViewModel = new UpdateCartItemQuantityViewModel();
                updateCartItemQuantityViewModel.CartItemsCount = cartItemsCount.ToString();
                updateCartItemQuantityViewModel.SubTotal = string.Format("{0:C}", subTotal);
                updateCartItemQuantityViewModel.SubTotalLabel = string.Format("({0} {1})", cartItemsCount, cartItemsCount == 1 ? "item": "items");
                updateCartItemQuantityViewModel.CartLabel = string.Format("Cart ({0})", cartItemsCount);

                return Json(updateCartItemQuantityViewModel);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                return RedirectToAction(Constants.CONTROLLER_ACTION_INDEX, Constants.CONTROLLER_ERROR);
            }
        }

        /// <summary>
        /// Get cart count of cart link at top navigation bar
        /// </summary>
        /// <returns>Partial view of cart link at top navigation bar</returns>
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            try
            {
                var userCookie = CookieManager.GetUserCookie();
                if (userCookie != null)
                {
                    var userEmail = userCookie[Constants.USER_EMAIL_COOKIE_KEY];

                    var cart = services.CartService.GetUserCartItems(userEmail);

                    ViewData["CartCount"] = cart.Sum(ci => ci.Quantity);
                }
                else
                {
                    ViewData["CartCount"] = 0;
                }
                
                return PartialView("CartSummary");
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                return null;
            }
        }
    }
}