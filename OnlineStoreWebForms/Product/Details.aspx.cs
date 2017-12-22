using OnlineStore.Managers;
using OnlineStoreModels;
using OnlineStoreServices.DTOs;
using OnlineStoreServices.Services;
using System;
using System.IO;
using System.Linq;

namespace OnlineStore.ProductController
{
    public partial class Details : System.Web.UI.Page
    {
        Services services = new Services();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Product product = services.ProductService.GetProduct(int.Parse(Request.QueryString["ProductID"]));

                    if (product != null)
                    {
                        var stockCount = product.StockCount;

                        priceLabel.Text = string.Format("{0:c}", product.Price);

                        if (stockCount > 5)
                        {
                            stockLabel.Text = "In Stock";
                            stockLabel.CssClass = Constants.CSS_CLASS_STOCK_GREATER_THAN_FIVE;
                            ddlQuantity.DataSource = Enumerable.Range(1, 4);
                            ddlQuantity.DataBind();
                        }
                        else if (stockCount == 0)
                        {
                            stockLabel.Text = "Out of Stock";
                            stockLabel.CssClass = Constants.CSS_CLASS_STOCK_EQUALS_ZERO;
                            btnAddToCart.Enabled = false;
                            btnAddToCart.Visible = false;
                            ddlQuantity.Enabled = false;
                        }
                        else
                        {
                            stockLabel.Text = string.Format("Only {0} in stock", stockCount);
                            stockLabel.CssClass = Constants.CSS_CLASS_STOCK_INCLUSIVELY_BETWEEN_ONE_AND_FIVE;
                            ddlQuantity.DataSource = Enumerable.Range(1, product.StockCount < 4 ? product.StockCount : 4);
                            ddlQuantity.DataBind();
                        }

                        //Add attribute runat="server" to generic HTML control and use innerText property to set inner text
                        //Useful when translating HTML code produced from MVC
                        productTitleSpan.InnerText = product.Name;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                Server.Transfer(Constants.PAGE_GENERIC_ERROR);
            } 
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            try
            {
                var productId = int.Parse(Request.QueryString["ProductID"]);

                var userEmail = CartManager.GetCart();

                // Get the matching cart and product instances
                var cartItem = services.CartService.GetCartItem(userEmail, productId);

                if (cartItem == null)
                {
                    // Create a new cart item if no cart item exists
                    cartItem = new CartItem
                    {
                        ProductID = productId,
                        Email = Session[Constants.EMAIL_SESSION_KEY].ToString(),
                        Quantity = int.Parse(ddlQuantity.Text),
                        DateCreated = DateTime.Now,
                    };

                    services.CartService.AddCartItem(cartItem);
                }
                else
                {
                    // If the item does exist in the cart, 
                    // then increase cart item quantity by selected quantity of drop down
                    cartItem.Quantity += int.Parse(ddlQuantity.SelectedValue);
                    services.CartService.UpdateCartItem(cartItem);
                }

                PageManager.RedirectToAddToCartPage(Request.QueryString["ProductID"]);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                Server.Transfer(Constants.PAGE_GENERIC_ERROR);
            }
        }
    }
}