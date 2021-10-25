namespace EOS2.Web.Extensions
{    
    using System.Linq;
    using System.Text;
    using System.Web.Routing;

    public static class RouteDataExtension
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Extension Method")]
        public static string ToLoggingString(this RouteData routeData)
        {
            var dataString = new StringBuilder();

            var ignoreValues = new[]
                                   {
                                       "controller",
                                       "action"
                                   };

            foreach (var value in routeData.Values.Where(value => ignoreValues.All(ignore => ignore != value.Key)))
            {
                dataString.AppendFormat("{0}={1},", value.Key, value.Value);
            }

            return dataString.ToString();
        }
    }
}