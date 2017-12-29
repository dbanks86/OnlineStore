using OnlineStore.Managers;
using OnlineStore.ViewModels;
using SmartyStreets;
using SmartyStreets.USStreetApi;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using OnlineStoreServices.Services;
using OnlineStoreModels;
using OnlineStoreServices.DTOs;

namespace OnlineStore.Controllers
{
    public class CheckoutController : Controller
    {
        readonly OnlineStoreEntities storeDB = new OnlineStoreEntities();
        readonly Services services = new Services();
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Display checkout page with cart information
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderInfo()
        {
            try
            {
                //get 
                var userCookie = CookieManager.GetUserCookie();

                //ensure that a user is logged in before an order is placed
                if (userCookie == null || !Regex.IsMatch(userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL].ToString(), Constants.REGEX_PATTERN_EMAIL))
                {
                    return RedirectToAction("Login", "Account", new { returnUrl = Request.Url.PathAndQuery });
                }

                //use email stored in cookie to create new CheckoutViewModel instance and pass it into view
                return View(GetNewCheckoutViewModel(userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL]));
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                return RedirectToAction(Constants.CONTROLLER_ERROR, Constants.CONTROLLER_ACTION_INDEX);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderInfo(CheckoutViewModel checkoutViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userCookie = CookieManager.GetUserCookie();

                    var userEmail = userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL];

                    //SmartyStreets address validation
                    #region SmartyStreets Address Validation
                    var client = new ClientBuilder(Constants.SMARTY_STREETS_AUTHENICATION_ID, Constants.SMARTY_STREETS_AUTHENICATION_TOKEN).BuildUsStreetApiClient();

                    var lookup = new Lookup
                    {
                        Street = string.IsNullOrWhiteSpace(checkoutViewModel.AptSuiteEtc) ? checkoutViewModel.Address : string.Format("{0} {1}", checkoutViewModel.Address, checkoutViewModel.AptSuiteEtc),
                        City = checkoutViewModel.City,
                        State = checkoutViewModel.State,
                        ZipCode = checkoutViewModel.ZipCode
                    };

                    client.Send(lookup);

                    var candidates = lookup.Result;

                    if (candidates.Count == 0)
                    {
                        ModelState.AddModelError("addessError", "Invalid address");
                        return View(checkoutViewModel);
                    }
                    #endregion

                    var candidateReturnFromAddressValidation = candidates[0];

                    //create order using model information
                    Order order = new Order();
                    order.Email = userEmail;
                    order.FirstName = checkoutViewModel.FirstName;
                    order.LastName = checkoutViewModel.LastName;
                    order.Address = candidateReturnFromAddressValidation.DeliveryLine1;
                    order.City = candidateReturnFromAddressValidation.Components.CityName;
                    order.State = candidateReturnFromAddressValidation.Components.State;
                    order.ZipCode = candidateReturnFromAddressValidation.LastLine.Split(' ')[2];  //Zip code returned from SmartyStreets address validation

                    var userCartItems = services.CartService.GetUserCartItems(userEmail, Constants.DATABASE_TABLE_PRODUCTS);
                    order.SubTotal = Math.Round(userCartItems.Sum(ci => ci.Product.Price * ci.Quantity), 2);

                    order.ShippingOptionID = checkoutViewModel.ShippingOptionID;
                    var shippingOption = services.ShippingService.GetShippingOption(checkoutViewModel.ShippingOptionID);

                    //calculate sales tax and order total based on whether or not intital selected shipping option is free
                    if (shippingOption.Price.HasValue)
                    {
                        order.SalesTax = Math.Round((order.SubTotal + shippingOption.Price.Value) * (decimal).07, 2);
                        order.Total = order.SubTotal + order.SalesTax.Value + shippingOption.Price.Value;
                    }
                    else
                    {
                        order.SalesTax = Math.Round(order.SubTotal * (decimal).07, 2);
                        order.Total = order.SubTotal + order.SalesTax.Value;
                    }

                    order.OrderDate = DateTime.Now;
                    services.OrderService.AddOrder(order);

                    foreach (var cartItem in userCartItems)
                    {
                        //create order detail, relating to newly created order, of cart item
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderID = order.OrderID;
                        orderDetail.ProductID = cartItem.ProductID;
                        orderDetail.ProductPrice = cartItem.Product.Price;
                        orderDetail.Quantity = cartItem.Quantity;

                        //add order detail to database
                        services.OrderDetailService.AddOrderDetail(orderDetail);

                        //remove cart items from database once order detail for cart items is created
                        services.CartService.DeleteCartItem(cartItem);
                    }

                    //set properties of order confirmation dto
                    OrderConfirmationDTO orderConfirmationDto = new OrderConfirmationDTO();
                    orderConfirmationDto.Candidate = candidateReturnFromAddressValidation;
                    orderConfirmationDto.FirstName = checkoutViewModel.FirstName;
                    orderConfirmationDto.LastName = checkoutViewModel.LastName;
                    orderConfirmationDto.OrderDate = DateTime.Now;
                    orderConfirmationDto.CartItems = userCartItems;
                    orderConfirmationDto.ShippingOptionID = shippingOption.ShippingOptionID;
                    orderConfirmationDto.ShippingOptionName = shippingOption.Name;
                    orderConfirmationDto.ShippingPrice = shippingOption.Price;
                    orderConfirmationDto.ExpectedDeliveryDays = shippingOption.ExpectedDeliveryDays;

                    //redirect to order confirmation view
                    return View("OrderConfirmation", orderConfirmationDto);
                }
                else //re-display page with validation errors
                {
                    var userCookie = CookieManager.GetUserCookie();

                    //use email stored in cookie to create new CheckoutViewModel instance and pass it into view
                    return View(GetNewCheckoutViewModel(userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL]));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                return RedirectToAction(Constants.CONTROLLER_ACTION_INDEX, Constants.CONTROLLER_ERROR);
            }
        }

        [HttpPost]
        public ActionResult UpdateShippingOption(int shippingOptionId)
        {
            try
            {
                var userEmail = CookieManager.GetEmailFromUserCookie();

                //Get cart items of user with cart item quantity updated
                var userCartItems = services.CartService.GetUserCartItems(userEmail, Constants.DATABASE_TABLE_PRODUCTS);
                var shippingOption = services.ShippingService.GetShippingOption(shippingOptionId);

                //create viewmodel for associated view and pass into view
                UpdateShippingOptionDTO updateShippingOptionDto = new UpdateShippingOptionDTO();

                decimal subTotal = userCartItems.Sum(ci => ci.Product.Price * ci.Quantity);
                decimal? shippingOptionPrice;
                decimal? salesTax;
                decimal total;

                if (shippingOption.Price.HasValue)
                {
                    shippingOptionPrice = shippingOption.Price;
                    salesTax = (subTotal + shippingOptionPrice) * (decimal).07;
                    total = subTotal + salesTax.Value + shippingOptionPrice.Value;
                    updateShippingOptionDto.ShippingPrice = string.Format("{0:C}", shippingOptionPrice);
                }
                else
                {
                    shippingOptionPrice = null;
                    salesTax = subTotal * (decimal).07;
                    total = subTotal + salesTax.Value;
                    updateShippingOptionDto.ShippingPrice = "FREE";
                }

                updateShippingOptionDto.SubTotal = string.Format("{0:C}", subTotal);
                updateShippingOptionDto.SalesTax = string.Format("{0:C}", salesTax);
                updateShippingOptionDto.Total = string.Format("{0:C}", total);
                updateShippingOptionDto.ShippingPrice = string.Format("{0:C}", shippingOptionPrice);

                return Json(updateShippingOptionDto);
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
        public ActionResult CheckoutHeaderCenter()
        {
            try
            {
                var userCookie = CookieManager.GetUserCookie();
                if (userCookie != null)
                {
                    var userEmail = userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL];

                    var cart = services.CartService.GetUserCartItems(userEmail);

                    ViewData["CartCount"] = cart.Sum(ci => ci.Quantity);
                }
                else
                {
                    ViewData["CartCount"] = 0;
                }

                return PartialView("CheckoutHeaderCenter");
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                return null;
            }
        }

        public CheckoutViewModel GetNewCheckoutViewModel(string userEmail, int shippingOptionId = Constants.INITIAL_SELECTED_SHIPPING_OPTION_ID)
        {
            //get cart items of user
            var userCartItems = services.CartService.GetUserCartItems(userEmail, Constants.DATABASE_TABLE_PRODUCTS).Select(cartItem => CartManager.GetNewCartItemViewModel(cartItem));

            //assign properties of view model that will be passed into view
            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
            checkoutViewModel.CartItemDTOs = userCartItems;
            checkoutViewModel.CartItemsCount = userCartItems.Sum(ci => ci.Quantity);
            checkoutViewModel.SubTotal = Math.Round(userCartItems.Sum(ci => ci.Price * ci.Quantity), 2);
            checkoutViewModel.ShippingOptions = services.ShippingService.GetAllShippingOptions().OrderBy(so => so.ExpectedDeliveryDays);
            checkoutViewModel.ShippingOptionID = shippingOptionId;

            var selectedShippingOption = checkoutViewModel.ShippingOptions.SingleOrDefault(so => so.ShippingOptionID == checkoutViewModel.ShippingOptionID);

            //calculate sales tax and order total based on whether or not intital selected shipping option is free
            if (selectedShippingOption.Price.HasValue)
            {
                checkoutViewModel.SelectedShippingOptionPrice = Math.Round(selectedShippingOption.Price.Value, 2);
                checkoutViewModel.SalesTax = Math.Round((checkoutViewModel.SubTotal + selectedShippingOption.Price.Value) * (decimal).07, 2);
                checkoutViewModel.OrderTotal = checkoutViewModel.SubTotal + checkoutViewModel.SalesTax.Value + checkoutViewModel.SelectedShippingOptionPrice.Value;
            }
            else
            {
                checkoutViewModel.SelectedShippingOptionPrice = null;
                checkoutViewModel.SalesTax = Math.Round((checkoutViewModel.SubTotal) * (decimal).07, 2);
                checkoutViewModel.OrderTotal = checkoutViewModel.SubTotal + checkoutViewModel.SalesTax.Value;
            }
            return checkoutViewModel;
        }
    }
}