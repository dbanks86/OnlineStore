namespace OnlineStore
{
    /// <summary>
    /// Store commonly strings commonly used throughout application
    /// Whenever a change is needed, all that is needed to be done is change value of necessary constant variable
    /// </summary>
    public static class Constants
    {
        public const string COOKIE_KEY_USER = "UserCookie";
        public const string COOKIE_KEY_USER_SUB_KEY_USER_EMAIL = "UserEmail";
        public const string COOKIE_KEY_USER_SUB_KEY_USER_FIRSTNAME = "UserFirstName";

        public const string REGEX_PATTERN_EMAIL = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}";

        public const string DATABASE_TABLE_PRODUCTS = "Product";

        public const int PASSWORD_HASH_LENGTH = 40;
        public const int PASSWORD_SALT_LENGTH = 20;
        public const int PASSWORD_INTERATIONS = 200000;

        public const int INITIAL_SELECTED_SHIPPING_OPTION_ID = 4;

        public const string CSS_CLASS_STOCK_EQUALS_ZERO = "stockEquals0";
        public const string CSS_CLASS_STOCK_INCLUSIVELY_BETWEEN_ONE_AND_FIVE = "stockBetween1And5";
        public const string CSS_CLASS_STOCK_GREATER_THAN_FIVE = "stockGreaterThan5";

        public const string CONTROLLER_HOME = "Home";

        public const string CONTROLLER_PRODUCT = "Product";
        public const string CONTROLLER_PRODUCT_ACTION_DETAILS = "Details";

        public const string CONTROLLER_CART = "Cart";
        public const string CONTROLLER_CART_ACTION_CART_SUMMARY = "CartSummary";

        public const string CONTROLLER_ERROR = "Error";
        
        public const string CONTROLLER_ACTION_INDEX = "Index";

        public const string SMARTY_STREETS_AUTHENICATION_ID = "00df82c8-b0dd-50f8-d9a5-664ec1e94f83";
        public const string SMARTY_STREETS_AUTHENICATION_TOKEN = "75TTfFlVY43aSKhsHrw4";
    }
}