namespace EOS2.Web.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using AutoMapper;

    using EOS2.Common.Exceptions;
    using EOS2.Common.Web.Extensions;
    using EOS2.Web.Code;
    using EOS2.Web.ViewModels;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ViewModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");

            var baseViewModel = InitialiseLocalBaseViewModel(filterContext);

            AddAvailableLanguagesToBaseViewModel(baseViewModel);

            AddDefaultRouteDataToBaseViewModel(filterContext.RouteData, baseViewModel);

            filterContext.Controller.ViewData.Model = baseViewModel;

            base.OnActionExecuted(filterContext);
        }

        internal static BaseViewModel InitialiseLocalBaseViewModel(ActionExecutedContext filterContext)
        {
            if (filterContext.Controller.ViewData.Model == null)
            {
                return new BaseViewModel();
            }

            var viewModel = filterContext.Controller.ViewData.Model as BaseViewModel;
            if (viewModel == null) throw new BaseViewModelException("BaseViewModel Is Required");

            return viewModel;
        }

        internal static void AddAvailableLanguagesToBaseViewModel(BaseViewModel baseViewModel)
        {           
            baseViewModel.AvailableLanguages = LanguageHelper.GetAvailableLanguages();

            var httpCookie = HttpContext.Current.Request.Cookies["i18n.langtag"];
            if (httpCookie != null)
            {
                baseViewModel.SelectedLanguageId = httpCookie.Value;
            }
        }

        internal static void AddDefaultRouteDataToBaseViewModel(RouteData routeData, BaseViewModel baseViewModel)
        {
            baseViewModel.CustomerId = routeData.GetIntRouteDataValue("CustomerId");
            baseViewModel.SiteId = routeData.GetIntRouteDataValue("SiteId");
            baseViewModel.PlantAreaId = routeData.GetIntRouteDataValue("PlantAreaId");
            baseViewModel.InstrumentId = routeData.GetIntRouteDataValue("InstrumentId");
            baseViewModel.EquipmentId = routeData.GetIntRouteDataValue("EquipmentId");
        }
    }
}