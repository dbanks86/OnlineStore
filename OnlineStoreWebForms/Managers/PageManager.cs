using System.Web;

namespace OnlineStore.Managers
{
    public static class PageManager
    {
        public static void RedirectToProductDetailsPage(string productId)
        {
            HttpContext.Current.Response.Redirect(Constants.PAGE_PRODUCT_DETAILS + productId);
        }

        public static void RedirectToAddToCartPage(string productId)
        {
            HttpContext.Current.Response.Redirect(Constants.PAGE_ADD_TO_CART + productId);
        }
    }
}