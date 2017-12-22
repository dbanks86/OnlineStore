namespace OnlineStore
{
    public class Constants
    {
        public const string USER_COOKIE_KEY = "UserCookie";
        public const string USER_EMAIL_COOKIE_KEY = "UserEmail";
        public const string USER_FIRST_NAME_COOKIE_KEY = "UserFirstName";

        public const int PASSWORD_HASH_LENGTH = 40;
        public const int PASSWORD_SALT_LENGTH = 20;
        public const int PASSWORD_INTERATIONS = 200000;

        public const string CSS_CLASS_STOCK_EQUALS_ZERO = "stockEquals0";
        public const string CSS_CLASS_STOCK_INCLUSIVELY_BETWEEN_ONE_AND_FIVE = "stockBetween1And5";
        public const string CSS_CLASS_STOCK_GREATER_THAN_FIVE = "stockGreaterThan5";

        public const string CONTROLLER_NAME_ERROR = "Error";
        public const string CONTROLLER_ACTION_ERROR = "Index";
    }
}