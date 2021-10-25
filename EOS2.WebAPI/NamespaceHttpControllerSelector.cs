﻿namespace EOS2.WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;
    using System.Web.Http.Routing;

    public class NamespaceHttpControllerSelector : IHttpControllerSelector
    {
        private const string NamespaceKey = "namespace";
        private const string ControllerKey = "controller";

        private readonly HttpConfiguration configuration;
        private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> controllers;
        private readonly HashSet<string> duplicates;

        public NamespaceHttpControllerSelector(HttpConfiguration config)
        {
            configuration = config;
            duplicates = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            controllers = new Lazy<Dictionary<string, HttpControllerDescriptor>>(InitializeControllerDictionary);
        }

        // Get a value from the route data, if present.
        [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1204:StaticElementsMustAppearBeforeInstanceElements", Justification = "A stupid rule that makes it less readable")]
        private static T GetRouteVariable<T>(IHttpRouteData routeData, string name)
        {
            object result = null;
            if (routeData.Values.TryGetValue(name, out result))
            {
                return (T)result;
            }

            return default(T);
        }

        private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary()
        {
            var dictionary = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);

            // Create a lookup table where key is "namespace.controller". The value of "namespace" is the last
            // segment of the full namespace. For example:
            // MyApplication.Controllers.V1.ProductsController => "V1.Products"
            IAssembliesResolver assembliesResolver = configuration.Services.GetAssembliesResolver();
            IHttpControllerTypeResolver controllersResolver = configuration.Services.GetHttpControllerTypeResolver();

            ICollection<Type> controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);

            foreach (Type t in controllerTypes)
            {
                var segments = t.Namespace.Split(Type.Delimiter);

                // For the dictionary key, strip "Controller" from the end of the type name.
                // This matches the behavior of DefaultHttpControllerSelector.
                var controllerName = t.Name.Remove(t.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length);

                var key = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", segments[segments.Length - 1], controllerName);

                // Check for duplicate keys.
                if (dictionary.Keys.Contains(key))
                {
                    duplicates.Add(key);
                }
                else if (t.BaseType == typeof(ApiController))
                {
                    dictionary[key] = new HttpControllerDescriptor(configuration, t.Name, t);
                }
            }

            // Remove any duplicates from the dictionary, because these create ambiguous matches. 
            // For example, "Foo.V1.ProductsController" and "Bar.V1.ProductsController" both map to "v1.products".
            foreach (string s in duplicates)
            {
                dictionary.Remove(s);
            }

            return dictionary;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Not currently using this class, so will look into this issue if/when we use this")]
        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            IHttpRouteData routeData = request.GetRouteData();
            if (routeData == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // Get the namespace and controller variables from the route data.
            string namespaceName = GetRouteVariable<string>(routeData, NamespaceKey);
            if (namespaceName == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            string controllerName = GetRouteVariable<string>(routeData, ControllerKey);
            if (controllerName == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // Find a matching controller.
            string key = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, controllerName);

            HttpControllerDescriptor controllerDescriptor;
            if (controllers.Value.TryGetValue(key, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            else if (duplicates.Contains(key))
            {
                throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Multiple controllers were found that match this request."));
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return controllers.Value;
        }
    }
}