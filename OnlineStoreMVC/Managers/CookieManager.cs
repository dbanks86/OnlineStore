using System;
using System.Web;

namespace OnlineStore.Managers
{
    public static class CookieManager
    {
        public static string GetEmailFromUserCookie()
        {
            //check if a user is logged in
            HttpCookie userInfo = GetUserCookie();
            if (userInfo == null)
            {
                // Generate a new random GUID using System.Guid class
                Guid tempCartId = Guid.NewGuid();

                // Send tempCartId (for anonymous user) back to client as a cookie
                userInfo = CreateUserCookie(tempCartId.ToString());
            }
            return userInfo[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL].ToString();
        }

        public static HttpCookie GetUserCookie()
        {
            //check if a user is logged in
            return HttpContext.Current.Request.Cookies[Constants.COOKIE_KEY_USER];
        }

        public static HttpCookie CreateUserCookie(string email, string firstname = "")
        {
            HttpCookie userInfo = new HttpCookie(Constants.COOKIE_KEY_USER);
            userInfo[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL] = email;

            if (!string.IsNullOrWhiteSpace(firstname))
            {
                userInfo[Constants.COOKIE_KEY_USER_SUB_KEY_USER_FIRSTNAME] = firstname;
            }

            userInfo.Expires = DateTime.Now.AddHours(6);
            HttpContext.Current.Response.Cookies.Add(userInfo);

            return userInfo;
        }

        public static void SetUserCookie(string email, string firstname = "")
        {
            HttpCookie userCookie = HttpContext.Current.Request.Cookies[Constants.COOKIE_KEY_USER];

            if (userCookie == null)
            {
                //create new cookie if no cookie exists
                userCookie = new HttpCookie(Constants.COOKIE_KEY_USER);
            }

            //set cookie values
            userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL] = email;

            if (!string.IsNullOrEmpty(firstname))
            {
                userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_FIRSTNAME] = firstname;
            }

            //update the expiration timestamp
            userCookie.Expires = DateTime.Now.AddHours(6);

            //add cookie if it did not exist or overwrite cookie that already existed
            HttpContext.Current.Response.Cookies.Add(userCookie);
        }

        public static HttpCookie UpdateUserCookie(string email, string firstname = "")
        {
            HttpCookie userInfo = GetUserCookie();
            userInfo[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL] = email;

            if (!string.IsNullOrWhiteSpace(firstname))
            {
                userInfo[Constants.COOKIE_KEY_USER_SUB_KEY_USER_FIRSTNAME] = firstname;
            }
            HttpContext.Current.Response.SetCookie(userInfo);
            return userInfo;
        }

        public static void DeleteUserCookie()
        {
            HttpCookie userCookie = HttpContext.Current.Response.Cookies[Constants.COOKIE_KEY_USER];

            if (userCookie != null)
            {
                userCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(userCookie);
            }
        }
    }
}