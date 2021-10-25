namespace EOS2.Web.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Site;

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class UniqueSiteAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var model = (CustomerSiteEditViewModel)value;

                var siteService = DependencyResolver.Current.GetService<ISiteService>();

                var siteFoundResult = model.Id > 0
                                           ? siteService.CustomerSiteExists(model.CustomerId, model.Name, model.Id)
                                           : siteService.CustomerSiteExists(model.CustomerId, model.Name);

                if (siteFoundResult)
                {
                    return new ValidationResult("[[[A site with this name already Exists.]]]", new[] { "Name" });
                }
            }

            return ValidationResult.Success;
        }
    }
}