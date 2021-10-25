namespace EOS2.Infrastructure.DependencyInjection.Lifetime
{
    using System;
    using System.Web;

    using Microsoft.Practices.Unity;

    public class UnityPerSessionLifetimeManager : LifetimeManager
    {
        private readonly string sessionKey = Guid.NewGuid().ToString();

        /// TODO: This needs its dependancy injected.  Best approach to this would be to pass in 
        /// an IHttpContextResolver of type HTTPContext Instance that can get at the HTTPContext.Current
        /// we do this to allow us to test

        public override object GetValue()
        {
            if (HttpContext.Current.Session == null) return null;

            return HttpContext.Current.Session[this.sessionKey];
        }

        public override void RemoveValue()
        {
            HttpContext.Current.Session.Remove(this.sessionKey);
        }

        public override void SetValue(object newValue)
        {
            if (HttpContext.Current.Session == null) return;

            HttpContext.Current.Session[this.sessionKey] = newValue;
        }
    }
}
