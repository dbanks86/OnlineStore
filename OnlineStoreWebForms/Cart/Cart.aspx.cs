using OnlineStore.DTOs;
using OnlineStore.Managers;
using OnlineStoreServices.Services;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineStore.Account
{
    public partial class Cart : System.Web.UI.Page
    {
        //declared static to allow usage in methods declared with WebMethod attribute
        private static Services services = new Services();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //email stored in cookie
                    var userEmail = CookieManager.GetEmailFromUserCookie();

                    //get cart items of user
                    var cartItems = services.CartService.GetUserCartItems(userEmail, Constants.DATABASE_TABLE_PRODUCTS);

                    if (cartItems.Any())
                    {
                        var cartItemsWebFormDtos = cartItems.Select(cartItem => CartManager.GetNewCartItemWebFormDTO(cartItem));

                        //count of cart items (sum of quantities of all cart items)
                        var cartItemsCount = cartItemsWebFormDtos.Sum(cartItem => cartItem.Quantity);

                        rptCartItems.DataSource = cartItemsWebFormDtos;
                        rptCartItems.DataBind();

                        var subTotal = cartItemsWebFormDtos.Sum(cartItem => cartItem.Price * cartItem.Quantity);

                        //set subtotal info
                        subTotalTd.InnerText = string.Format("{0:C}", subTotal);
                        lblCartItemCount.Text = string.Format("({0} {1})", cartItemsCount, cartItemsCount == 1 ? "item" : "items");
                    }
                    else
                    {
                        subTotalTd.InnerText = string.Format("{0:C}", 0);
                        lblCartItemCount.Text = string.Format("({0} {1})", 0, "items");
                        btnCheckout.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                Server.Transfer(Constants.PAGE_GENERIC_ERROR);
            }
        }

        protected void rptCartItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                //Repeater rows will be of type 'CartItem' since repeater was databinded to query returing 'CartItem' objects
                var cartItem = (CartItemWebFormDTO)e.Item.DataItem;

                var stockLabel = e.Item.FindControl("stockLabel") as Label;
                if (stockLabel != null)
                {
                    stockLabel.Attributes["ProductID"] = cartItem.ProductID.ToString();

                    stockLabel.Text = cartItem.StockMessage;
                    stockLabel.CssClass = cartItem.StockMessageCssClass;
                }

                //set drop down quantities of each cart item
                var quantityDropDownList = e.Item.FindControl("quantityDropDownList") as DropDownList;
                if (quantityDropDownList != null)
                {

                    quantityDropDownList.DataSource = Enumerable.Range(1, cartItem.StockCount > 50 ? 50 : cartItem.StockCount);
                    quantityDropDownList.SelectedValue = cartItem.Quantity.ToString();
                    quantityDropDownList.DataBind();
                }

                //set product id of product to be deleted if delete button is clicked
                var deleteButton = e.Item.FindControl("deleteCartItemButton") as Button;
                if (deleteButton != null)
                {
                    deleteButton.Attributes.Add("cartItemId", cartItem.CartItemID.ToString());
                }
                //set product id of product to be displayed on product details page on link button that appears after cart item has been deleted
                var cartItemRemovedLinkButton = e.Item.FindControl("cartItemRemovedLinkButton") as LinkButton;
                if (cartItemRemovedLinkButton != null)
                {
                    cartItemRemovedLinkButton.Attributes.Add("productId", cartItem.ProductID.ToString());
                }

                //set product id of product to be displayed on product details page once product name is clicked
                var cartItemProductLinkButton = e.Item.FindControl("cartItemProductNameLinkButton") as LinkButton;
                if (cartItemProductLinkButton != null)
                {
                    cartItemProductLinkButton.Attributes.Add("productId", cartItem.ProductID.ToString());
                }

                //set product id of product to be displayed on product details page once product image is clicked
                var productImageButton = e.Item.FindControl("ProductImageButton") as ImageButton;
                if (productImageButton != null)
                {
                    productImageButton.Attributes.Add("productId", cartItem.ProductID.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                Server.Transfer(Constants.PAGE_GENERIC_ERROR);
            }
        }

        protected void cartItemRemovedLinkButton_Click(object sender, EventArgs e)
        {
            var linkButton = sender as LinkButton;
            PageManager.RedirectToProductDetailsPage(linkButton.Attributes["productId"]);
        }

        protected void cartItemProductNameLinkButton_Click(object sender, EventArgs e)
        {
            var linkButton = sender as LinkButton;
            PageManager.RedirectToProductDetailsPage(linkButton.Attributes["productId"]);
        }

        protected void ProductImageButton_Click(object sender, ImageClickEventArgs e)
        {
            var imageButton = sender as ImageButton;
            PageManager.RedirectToProductDetailsPage(imageButton.Attributes["productId"]);
        }

        /// <summary>
        /// 
        /// NOTE: WebMethods being call with AJAX, along with JQuery, MUST be public AND static
        /// </summary>
        /// <param name="cartItemId"></param>
        /// <returns></returns>
        [WebMethod]
        public static RemoveCartItemWebFormDTO RemoveCartItem(int cartItemId)
        {
            try
            {
                //get cart item to remove
                var cartItem = services.CartService.GetCartItem(cartItemId);

                //create DTO to be passed to AJAX call
                RemoveCartItemWebFormDTO removeCartItemWebFormDTO = new RemoveCartItemWebFormDTO();
                removeCartItemWebFormDTO.CartItemID = cartItemId;

                //remove cart item from database
                services.CartService.DeleteCartItem(cartItem);

                //get cookie storing user information
                var userCookie = CookieManager.GetUserCookie();

                if (userCookie != null)
                {
                    var userEmail = userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL];

                    //Get cart items of user after cart item is removed
                    var userCartItems = services.CartService.GetUserCartItems(userEmail, Constants.DATABASE_TABLE_PRODUCTS);

                    //set viewmodel properties based on remaining count of items after product is removed from cart
                    if (userCartItems.Any())
                    {
                        int cartItemsCount = userCartItems.Sum(ci => ci.Quantity);

                        removeCartItemWebFormDTO.CartItemsCount = cartItemsCount;
                        removeCartItemWebFormDTO.SubTotal = string.Format("{0:C}", userCartItems.Sum(ci => ci.Product.Price * ci.Quantity));
                        removeCartItemWebFormDTO.SubTotalLabel = string.Format("({0} {1})", cartItemsCount, cartItemsCount == 1 ? "item" : "items");
                        removeCartItemWebFormDTO.CartLabel = string.Format("Cart ({0})", cartItemsCount);
                    }
                    else
                    {
                        removeCartItemWebFormDTO.CartItemsCount = 0;
                        removeCartItemWebFormDTO.SubTotal = string.Format("{0:C}", 0);
                        removeCartItemWebFormDTO.SubTotalLabel = string.Format("({0} {1})", 0, "items");
                        removeCartItemWebFormDTO.CartLabel = string.Format("Cart ({0})", 0);
                    }

                    //update cookie after user interacts with cart
                    CookieManager.SetUserCookie(userEmail, userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_FIRSTNAME]);

                    //return JSON object to be used for AJAX
                    return removeCartItemWebFormDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(HttpContext.Current.Request.PhysicalPath));
                HttpContext.Current.Server.Transfer(Constants.PAGE_GENERIC_ERROR);
                return null;
            }
        }

        protected void quantityDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //get dropdown of cart item sent to this event handler
                var dropDownList = (DropDownList)sender;

                //get repeater row of dropdown
                var repeaterRow = dropDownList.NamingContainer;

                //get product id from hidden field of cart item
                var productIdHiddenField = repeaterRow.FindControl("productIdHiddenField") as HiddenField;

                //get cart item from database
                var cartItem = services.CartService.GetCartItem(int.Parse(productIdHiddenField.Value));

                //set cart item quantity to quantity selected by user
                cartItem.Quantity = int.Parse(dropDownList.SelectedValue);

                //update cart item in database
                services.CartService.UpdateCartItem(cartItem);

                //get cookie storing user email
                var userCookie = CookieManager.GetUserCookie();

                if (userCookie != null)
                {
                    //get cart items after cart item quantity is updated
                    var userCartItems = services.CartService.GetUserCartItems(userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL], Constants.DATABASE_TABLE_PRODUCTS);
                    var cartItemsCount = userCartItems.Sum(ci => ci.Quantity);

                    //update subtotal based on selected quantity
                    lblCartItemCount.Text = string.Format("({0} {1})", cartItemsCount, cartItemsCount == 1 ? "item" : "items");
                    subTotalTd.InnerText = string.Format("{0:C}", userCartItems.Sum(ci => ci.Product.Price * ci.Quantity));

                    //update cart link text on top right of page
                    ((LinkButton)this.Master.FindControl("cartLinkButton")).Text = string.Format("Cart ({0})", userCartItems.Sum(ci => ci.Quantity));

                    //update cookie storing user information
                    CookieManager.SetUserCookie(userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL], userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_FIRSTNAME]);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                Server.Transfer(Constants.PAGE_GENERIC_ERROR);
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Checkout/Checkout.aspx");
        }
    }
}