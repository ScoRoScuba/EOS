namespace EOS2.Common.Web.Extensions
{
    using System.Web.Routing;

    public static class RouteDataExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "This is an Extension method type, this will always exist")] 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int", Justification = "represents actual type to be read from routeData")]
        public static int GetIntRouteDataValue(this RouteData routeData, string key)
        {
            object output;
            if (!routeData.Values.TryGetValue(key, out output)) return 0;

            int intValue = 0;
            if (!int.TryParse(output.ToString(), out intValue)) return 0;

            return intValue;
        }
    }
}
