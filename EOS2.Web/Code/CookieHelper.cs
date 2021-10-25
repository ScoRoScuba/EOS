namespace EOS2.Web.Code
{
    using System;
    using System.Web;

    public static class CookieHelper
    {
        public static void SetHttpOnlyCookie(string name, string value)
        {
            var context = HttpContext.Current;

            context.Response.Cookies.Add(
                new HttpCookie(name)
                    {
                        Value = value,
                        HttpOnly = true
                    });
        }

        public static void SetHttpOnlyCookie(string name, string value, DateTime expires)
        {
            HttpContext.Current.Response.Cookies.Add(
                new HttpCookie(name)
                    {
                        Value = value,
                        HttpOnly = true,
                        Expires = expires
                    });
        }

        public static void ClearCookie(string name)
        {
            var cookie = HttpContext.Current.Response.Cookies[name];
            if (cookie != null)
            {
                cookie.Value = null;
                cookie.Expires = DateTime.UtcNow.AddMonths(-1);
            }            
        }

        public static HttpCookie GetCookie(string name)
        {            
            return HttpContext.Current.Response.Cookies[name];
        }

        public static string GetCookieValue(string name)
        {
            var cookie = GetCookie(name);
            if (cookie != null)
            {
                return cookie.Value;
            }

            return null;
        }
    }
}