namespace Eurotherm.Security.Tests
{
    using System.Collections.Generic;
    using System.IdentityModel.Metadata;
    using System.Security.Claims;
    using System.Web.Mvc;
    using System.Web.Routing;

    using EOS2.Common.Web;
    using EOS2.Identity.Model;
    using EOS2.Infrastructure.Security;
    using EOS2.Model.Enums;

    using Eurotherm.Security.Tests.AreaRegistrations;
    using NUnit.Framework;
    using TestingHelpers;

    // we have to do this bit as System.Web.MVC also has an AuthorizationContext which is not the same
    // but we need some of MVC for the MVC elements used by the tests
    using AuthorizationContext = System.Security.Claims.AuthorizationContext;

    /// <summary>
    /// AuthorizationManager_CheckAccess_Tests
    /// Contains tests related to the AuthorizationManager
    /// </summary>    
    public class AuthorizationManagerTests
    {
        /// <summary>
        /// Contained TextFixture to hold the 
        /// </summary>
        [TestFixture]
        public class TheCheckAccessMethod
        {
            ////[Test]
            ////public void ReturnnsTrueIfUserClaimsAllowAccessToResourceForEdit()
            ////{
            ////    var routes = new RouteCollection();
            ////    routes.Clear();

            ////    var areaRegistration = new DemoAreaRegistration();
            ////    var areaRegistrationContext = new AreaRegistrationContext(areaRegistration.AreaName, routes);
            ////    areaRegistration.RegisterArea(areaRegistrationContext);

            ////    HttpContextFactory.SetCurrentContext(MvcMockHelpers.MockHttpContext("~/Customers/Home/index"));
            ////    RoutingFactory.SetRouteCollection(routes);

            ////    var claimIdentity =
            ////        new ClaimsIdentity(
            ////            new List<Claim>
            ////                {
            ////                    new Claim(ClaimTypes.Role, "Administrator"),
            ////                    new Claim("OrganizationType", "EOSOwner"),
            ////                });

            ////    var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
            ////    var authContext = new AuthorizationContext(claimsPrincipal, "User", "Insert");

            ////    var authorizationManager = new AuthorizationManager();

            ////    var result = authorizationManager.CheckAccess(authContext);

            ////    Assert.IsTrue(result);
            ////}
            
            ////[Test]
            ////public void ReturnnsTrueIfUserClaimsAllowAccessToResourceForView()
            ////{
            ////    var claimIdentity =
            ////        new ClaimsIdentity(
            ////            new List<Claim>
            ////                {
            ////                    new Claim(ClaimTypes.Role, "Administrator"),
            ////                    new Claim(ClaimTypes.Role, "User"),
            ////                    new Claim("OrganizationType", "EOSOwner"),
            ////                });

            ////    var routes = new RouteCollection();
            ////    routes.Clear();

            ////    var areaRegistration = new DemoAreaRegistration();
            ////    var areaRegistrationContext = new AreaRegistrationContext(areaRegistration.AreaName, routes);
            ////    areaRegistration.RegisterArea(areaRegistrationContext);

            ////    HttpContextFactory.SetCurrentContext(MvcMockHelpers.MockHttpContext("~/Customers/Home/index"));
            ////    RoutingFactory.SetRouteCollection(routes);

            ////    var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
            ////    var authContext = new AuthorizationContext(claimsPrincipal, "User", "View");

            ////    var authorizationManager = new AuthorizationManager();

            ////    var result = authorizationManager.CheckAccess(authContext);

            ////    Assert.IsTrue(result);
            ////}
            
            [Test]
            public static void CheckResourceManagerIsAllowedToInsertUserReturnsFalse()
            {
                var claimIdentity =
                    new ClaimsIdentity(
                        new List<Claim>
                            {
                                new Claim(ClaimTypes.Role, "ResourceManager"),
                                new Claim(ClaimTypes.Role, "User"),
                                new Claim("Site", string.Empty),
                                new Claim("Site", "2"),
                                new Claim(EOS2ClaimTypes.OrganizationType, OrganizationType.EOSOwner.ToString()),
                            });

                var routes = new RouteCollection();
                routes.Clear();

                var areaRegistration = new DemoAreaRegistration();
                var areaRegistrationContext = new AreaRegistrationContext(areaRegistration.AreaName, routes);

                // build up the route collection
                areaRegistration.RegisterArea(areaRegistrationContext);

                HttpContextFactory.SetCurrentContext(MvcMockHelpers.MockHttpContext("~/Customers/Home/index"));
                RoutingFactory.SetRouteCollection(routes);

                var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
                var authContext = new AuthorizationContext(claimsPrincipal, "User", "Insert");

                var authorizationManager = new AuthorizationManager();

                var result = authorizationManager.CheckAccess(authContext);

                Assert.IsFalse(result);
            }
            
            [Test]
            public static void CheckAnonymousUserIsAllowedToViewUserReturnsFalse()
            {
                var claimIdentity =
                    new ClaimsIdentity(
                        new List<Claim>
                            {
                                new Claim(ClaimTypes.Role, "ResourceManager"),
                                new Claim(ClaimTypes.Role, "Anonymous"),
                                new Claim(EOS2ClaimTypes.OrganizationType, OrganizationType.EOSOwner.ToString()),
                            });

                var routes = new RouteCollection();
                routes.Clear();

                var areaRegistration = new DemoAreaRegistration();
                var areaRegistrationContext = new AreaRegistrationContext(areaRegistration.AreaName, routes);
                areaRegistration.RegisterArea(areaRegistrationContext);

                HttpContextFactory.SetCurrentContext(MvcMockHelpers.MockHttpContext("~/Customers/Home/index"));
                RoutingFactory.SetRouteCollection(routes);

                var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
                var authContext = new AuthorizationContext(claimsPrincipal, "User", "Index");

                var authorizationManager = new AuthorizationManager();

                var result = authorizationManager.CheckAccess(authContext);

                Assert.IsFalse(result);
            }
            
            ////[Test]
            ////public void CheckAccessEngineerWantsToCreateEquipmentAtCustomerSiteAndIsAllowedReturnsTrue()
            ////{
            ////    var routes = new RouteCollection();
            ////    routes.Clear();

            ////    var areaRegistration = new ServiceProviderAreaRegistration();
            ////    var areaRegistrationContext = new AreaRegistrationContext(areaRegistration.AreaName, routes);
            ////    areaRegistration.RegisterArea(areaRegistrationContext);

            ////    HttpContextFactory.SetCurrentContext(MvcMockHelpers.MockHttpContext("~/ServiceProvider/Customers/1/Site/2/Equipment/Create"));
            ////    RoutingFactory.SetRouteCollection(routes);

            ////    var claimIdentity =
            ////        new ClaimsIdentity(
            ////            new List<Claim>
            ////                {
            ////                    new Claim(ClaimTypes.Role, "Engineer"),
            ////                    new Claim("OrganizationType", "ServiceProvider"),
            ////                    new Claim("OrganizationId", "1"),
            ////                    new Claim("Customer", "1"),
            ////                    new Claim("Site", "1:1"),
            ////                    new Claim("Site", "1:2"),
            ////                    new Claim("Site", "1:3"),
            ////                    new Claim("Customer", "2"),
            ////                    new Claim("Site", "2:1"),
            ////                    new Claim("Site", "2:2")
            ////                });

            ////    var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
            ////    var authContext = new AuthorizationContext(claimsPrincipal, "Equipment", "Create");

            ////    var authorizationManager = new AuthorizationManager();

            ////    var result = authorizationManager.CheckAccess(authContext);

            ////    Assert.IsTrue(result);
            ////}
            
            [Test]
            public static void CheckAccessEngineerWantsToCreateEquipmentAtCustomerSiteAndIsNotAllowedReturnsFalse()
            {
                var routes = new RouteCollection();
                routes.Clear();

                var areaRegistration = new ServiceProviderAreaRegistration();
                var areaRegistrationContext = new AreaRegistrationContext(areaRegistration.AreaName, routes);
                areaRegistration.RegisterArea(areaRegistrationContext);

                HttpContextFactory.SetCurrentContext(
                    MvcMockHelpers.MockHttpContext("~/ServiceProvider/Customers/1/Site/3/Equipment/Create"));
                RoutingFactory.SetRouteCollection(routes);

                var claimIdentity =
                    new ClaimsIdentity(
                        new List<Claim>
                            {
                                new Claim(ClaimTypes.Role, "Engineer"),
                                new Claim("OrganizationType", "ServiceProvider"),
                                new Claim("OrganizationId", "1"),
                                new Claim("Customer", "1"),
                                new Claim("Site", "1:1"),
                                new Claim("Site", "1:2"),
                                new Claim("Customer", "2"),
                                new Claim("Site", "2:1"),
                                new Claim("Site", "2:2")
                            });

                var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
                var authContext = new AuthorizationContext(claimsPrincipal, "Equipment", "Create");

                var authorizationManager = new AuthorizationManager();

                var result = authorizationManager.CheckAccess(authContext);

                Assert.IsFalse(result);
            }
        }
    }
}
