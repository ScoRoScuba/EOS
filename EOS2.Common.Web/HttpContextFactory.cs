namespace EOS2.Common.Web
{
    using System;
    using System.Web;

    public static class HttpContextFactory
    {
        private static HttpContextBase httpContext;

        public static HttpContextBase Current
        {
            get
            {
                if (httpContext != null) return httpContext;
                if (HttpContext.Current == null) throw new InvalidOperationException("HttpContext not Available");

                return new HttpContextWrapper(HttpContext.Current);
            }
        }

        // ReSharper disable once ParameterHidesMember
        public static void SetCurrentContext(HttpContextBase httpContext)
        {
            HttpContextFactory.httpContext = httpContext;
        }
    }
}
