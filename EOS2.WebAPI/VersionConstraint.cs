namespace EOS2.WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http.Routing;

    /// <summary>
    /// A Constraint implementation that matches an HTTP header against an expected version value.
    /// </summary>
    internal class VersionConstraint : IHttpRouteConstraint
    {
        public const string VersionCustomHeaderName = "api-version";

        public const string VersionParameterHeaderName = "version";

        private const int DefaultVersion = 2;

        public VersionConstraint(int allowedVersion)
        {
            AllowedVersion = allowedVersion;
        }

        public int AllowedVersion
        {
            get;
            private set;
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                int version = GetVersionHeader(request) ?? DefaultVersion;

                return version == AllowedVersion;
            }

            return true;
        }

        private static int? GetVersionHeader(HttpRequestMessage request)
        {
            string versionAsString = null;
            IEnumerable<string> headerValues;
            if (request.Headers.TryGetValues(VersionCustomHeaderName, out headerValues) && headerValues.Count() == 1)
            {
                versionAsString = headerValues.First();
            }
            else
            {
                var accept = request.Headers.Accept.Where(a => a.Parameters.Count(p => p.Name == VersionParameterHeaderName) > 0);
                if (accept.Any())
                    versionAsString = accept.First().Parameters.Single(s => s.Name == VersionParameterHeaderName).Value;
            }

            int version;
            if (!string.IsNullOrEmpty(versionAsString) && int.TryParse(versionAsString, out version))
            {
                return version;
            }

            return null;
        }
    }
}