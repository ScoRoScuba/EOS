namespace EOS2.Web.Attributes
{
    using System;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Thinktecture.IdentityModel.Authorization;

    using AuthorizationContext = System.Web.Mvc.AuthorizationContext;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Justification = "Private ONLY")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class EOSClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private const string HttpAttributeKey = "EOS2.Web.Attributes.EOSClaimsAuthorizeAttribute";
        private readonly string action;
        private readonly string[] resources;

        public EOSClaimsAuthorizeAttribute()
        {
        }

        public EOSClaimsAuthorizeAttribute(string action, params string[] resources)
        {
            this.action = action;
            this.resources = resources;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");

            filterContext.HttpContext.Items[HttpAttributeKey] = filterContext;

            base.OnAuthorization(filterContext);

            // this means we probably didnt have authorization
            if (filterContext.Result != null)  
            {
                if (filterContext.Result.GetType() == typeof(HttpUnauthorizedResult))
                {
                    if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = string.Empty, controller = "Security", action = "AccessDenied", returnUrl = filterContext.HttpContext.Request.UrlReferrer }));
                    } 
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = string.Empty, controller = "Account", action = "SignIn", returnUrl = filterContext.HttpContext.Request.UrlReferrer }));
                    }
                }
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException("httpContext");

            if (!string.IsNullOrWhiteSpace(this.action))
                return ClaimsAuthorization.CheckAccess(this.action, this.resources);
        
            return CheckAccess(httpContext.Items[HttpAttributeKey] as AuthorizationContext);
        }

        private static bool CheckAccess(AuthorizationContext filterContext)
        {
          return ClaimsAuthorization.CheckAccess(filterContext.RouteData.Values["action"] as string, filterContext.RouteData.Values["controller"] as string, new Claim[0]);
        }
    }
}