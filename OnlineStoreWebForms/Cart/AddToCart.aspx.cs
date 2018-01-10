using OnlineStore.Managers;
using OnlineStoreModels;
using OnlineStoreServices.Services;
using System;
using System.IO;
using System.Linq;

namespace OnlineStore.ShoppingCart
{
    public partial class AddToCart : System.Web.UI.Page
    {
        Services services = new Services();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //get user storing user information
                    var userCookie = CookieManager.GetUserCookie();

                    if (userCookie != null)
                    {
                        //email stored in cookie
                        var userEmail = userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL];

                        //get cart items of user
                        var cartItems = services.CartService.GetUserCartItems(userEmail, Constants.DATABASE_TABLE_PRODUCTS);

                        //count of cart items (sum of quantities of all cart items)
                        var cartItemsCount = cartItems.Sum(cartItem => cartItem.Quantity);

                        //set cart item count label
                        cartItemsCountLabel.Text = string.Format("({0} {1}):", cartItemsCount, cartItemsCount == 1 ? "item" : "items");

                        //set subtotal info
                        subTotalLabel.Text = string.Format("{0:c}", cartItems.Sum(ci => ci.Product.Price * ci.Quantity));
                    }     
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                Server.Transfer(Constants.PAGE_GENERIC_ERROR);
            }
        }

        protected void cartButton_Click(object sender, EventArgs e)
        {
            PageManager.RedirectToCartPage();
        }

        protected void checkoutButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Checkout/Checkout.aspx");
        }
    }
}