using System;
using System.Web;

namespace OnlineStore.Managers
{
    public static class CookieManager
    {
        public static string GetEmailFromUserCookie()
        {
            //check if a user is logged in
            HttpCookie userCookie = GetUserCookie();

            if (userCookie == null)
            {
                // Generate a new random GUID using System.Guid class
                Guid tempCartId = Guid.NewGuid();

                // Send tempCartId (for anonymous user) back to client as a cookie
                userCookie = CreateUserCookie(tempCartId.ToString());
            }
            else
            {
                UpdateUserCookieExpiration();
            }

            return userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL].ToString();
        }

        public static HttpCookie GetUserCookie()
        {
            //check if a user is logged in
            return HttpContext.Current.Request.Cookies[Constants.COOKIE_KEY_USER];
        }

        /// <summary>
        /// Create new cookie
        /// </summary>
        /// <param name="email">email, or anonymous guid, of user</param>
        /// <param name="firstname">first name of user</param>
        /// <returns></returns>
        public static HttpCookie CreateUserCookie(string email, string firstname = "")
        {
            HttpCookie userCookie = new HttpCookie(Constants.COOKIE_KEY_USER);

            userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL] = email;

            if (!string.IsNullOrWhiteSpace(firstname))
            {
                userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_FIRSTNAME] = firstname;
            }

            //update the expiration timestamp
            userCookie.Expires = DateTime.Now.AddHours(6);

            //add cookie if it did not exist or overwrite cookie that already existed
            HttpContext.Current.Response.Cookies.Add(userCookie);

            return userCookie;
        }

        /// <summary>
        /// Updates cookie that store user infomation
        /// Updates expirations date of cookie as well
        /// </summary>
        /// <param name="email"></param>
        /// <param name="firstname"></param>
        public static void SetUserCookie(string email = null, string firstname = null)
        {
            HttpCookie userCookie = HttpContext.Current.Request.Cookies[Constants.COOKIE_KEY_USER];

            if (userCookie == null)
            {
                //create new cookie if no cookie exists
                userCookie = new HttpCookie(Constants.COOKIE_KEY_USER);
                email = Guid.NewGuid().ToString();
            }

            //set cookie values
            userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL] = email;

            if (!string.IsNullOrWhiteSpace(firstname))
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
            HttpCookie userCookie = GetUserCookie();
            userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_EMAIL] = email;

            if (!string.IsNullOrWhiteSpace(firstname))
            {
                userCookie[Constants.COOKIE_KEY_USER_SUB_KEY_USER_FIRSTNAME] = firstname;
            }

            //update the expiration timestamp
            userCookie.Expires = DateTime.Now.AddHours(6);

            //add cookie if it did not exist or overwrite cookie that already existed
            HttpContext.Current.Response.Cookies.Add(userCookie);

            return userCookie;
        }

        public static HttpCookie UpdateUserCookieExpiration()
        {
            HttpCookie userCookie = GetUserCookie();

            //update the expiration timestamp
            userCookie.Expires = DateTime.Now.AddHours(6);

            //add cookie if it did not exist or overwrite cookie that already existed
            HttpContext.Current.Response.Cookies.Add(userCookie);

            return userCookie;
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