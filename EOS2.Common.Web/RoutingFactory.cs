namespace EOS2.Common.Web
{
    using System.Web.Routing;

    public static class RoutingFactory
    {
        private static RouteCollection routes;

        public static RouteCollection Routes
        {
            get
            {
                if (routes == null)
                {
                    return RouteTable.Routes;
                }
                else
                {
                    return routes;
                }
            }
        }

        // ReSharper disable once ParameterHidesMember
        public static void SetRouteCollection(RouteCollection routes)
        {
            RoutingFactory.routes = routes;
        }
    }
}
