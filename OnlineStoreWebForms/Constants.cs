namespace OnlineStore
{
    public static class Constants
    {
        public const string EMAIL_SESSION_KEY = "UserEmail";
        public const string USER_FIRST_NAME_SESSION_KEY = "UserFirstName";
        public const string QUERY_STRING_RETURN_URL = "returnUrl";

        public const int PASSWORD_HASH_LENGTH = 40;
        public const int PASSWORD_SALT_LENGTH = 20;
        public const int PASSWORD_INTERATIONS = 200000;

        public const string CSS_CLASS_FIELD_ERROR = "field-error-textbox";
        public const string CSS_CLASS_STOCK_EQUALS_ZERO = "stockEquals0";
        public const string CSS_CLASS_STOCK_INCLUSIVELY_BETWEEN_ONE_AND_FIVE = "stockBetween1And5";
        public const string CSS_CLASS_STOCK_GREATER_THAN_FIVE = "stockGreaterThan5";


        public const int INITIAL_SELECTED_SHIPPING_OPTION_ID = 1;

        public const string DATABASE_TABLE_PRODUCT = "Product";

        public const string PAGE_PRODUCT_DETAILS = "~/Product/Details.aspx?productId=";
        public const string PAGE_ADD_TO_CART = "~/Cart/AddToCart.aspx?ProductID=";
        public const string PAGE_GENERIC_ERROR = "~/GenericErrorPage.aspx";

        public const string ORDER_CONFIRMATION_DTO_SESSION_KEY = "OrderConfirmationDTO";
        public const string SELECTED_SHIPPING_OPTION = "SelectShippingOption";
    }
}