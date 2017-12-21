namespace OnlineStoreServices.Managers
{
    /// <summary>
    /// Utility class for products
    /// </summary>
    static class ProductManager
    {
        public static string GetProductStockMessage(int stockCount)
        {
            if (stockCount > 5)
            {
                return "In Stock";
            }
            else if (stockCount == 0)
            {
                return "Out of Stock";
            }
            else
            {
                return string.Format("Only {0} in stock", stockCount);
            }
        }
    }
}
