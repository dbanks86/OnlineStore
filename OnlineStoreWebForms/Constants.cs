namespace OnlineStore
{
    public static class Constants
    {
        public const string COOKIE_KEY_USER = "UserCookie";
        public const string COOKIE_KEY_USER_SUB_KEY_USER_EMAIL = "UserEmail";
        public const string COOKIE_KEY_USER_SUB_KEY_USER_FIRSTNAME = "UserFirstName";

        public const string REGEX_PATTERN_EMAIL = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}";

        public const string QUERY_STRING_RETURN_URL = "returnUrl";

        public const int PASSWORD_HASH_LENGTH = 40;
        public const int PASSWORD_SALT_LENGTH = 20;
        public const int PASSWORD_INTERATIONS = 200000;

        public const string CSS_CLASS_FIELD_ERROR = "field-error-textbox";
        public const string CSS_CLASS_STOCK_EQUALS_ZERO = "stockEquals0";
        public const string CSS_CLASS_STOCK_INCLUSIVELY_BETWEEN_ONE_AND_FIVE = "stockBetween1And5";
        public const string CSS_CLASS_STOCK_GREATER_THAN_FIVE = "stockGreaterThan5";

        public const int INITIAL_SELECTED_SHIPPING_OPTION_ID = 1;

        public const string DATABASE_TABLE_PRODUCTS = "Product";

        public const string PAGE_PRODUCT_BROWSE = "~/Product/Browse.aspx?searchString=";
        public const string PAGE_PRODUCT_DETAILS = "~/Product/Details.aspx?ProductID=";
        public const string PAGE_CART = "~/Cart/Cart.aspx";
        public const string PAGE_ADD_TO_CART = "~/Cart/AddToCart.aspx?ProductID=";
        public const string PAGE_GENERIC_ERROR = "~/GenericErrorPage.aspx";

        public const string ORDER_CONFIRMATION_DTO_SESSION_KEY = "OrderConfirmationDTO";
        public const string SELECTED_SHIPPING_OPTION = "SelectShippingOption";

        public const string SMARTY_STREETS_AUTHENICATION_ID = "00df82c8-b0dd-50f8-d9a5-664ec1e94f83";
        public const string SMARTY_STREETS_AUTHENICATION_TOKEN = "75TTfFlVY43aSKhsHrw4";

        public static string EMAIL_SESSION_KEY { get; internal set; }
    }
}
