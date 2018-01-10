using System.Web;

namespace OnlineStore.Managers
{
    public static class PageManager
    {
        public static void RedirectToProductBrowsePage(string searchString)
        {
            HttpContext.Current.Response.Redirect(Constants.PAGE_PRODUCT_BROWSE + searchString);
        }

        public static void RedirectToProductDetailsPage(string productId)
        {
            HttpContext.Current.Response.Redirect(Constants.PAGE_PRODUCT_DETAILS + productId);
        }

        public static void RedirectToCartPage()
        {
            HttpContext.Current.Response.Redirect(Constants.PAGE_CART);
        }

        public static void RedirectToAddToCartPage(string productId)
        {
            HttpContext.Current.Response.Redirect(Constants.PAGE_ADD_TO_CART + productId);
        }
    }
}