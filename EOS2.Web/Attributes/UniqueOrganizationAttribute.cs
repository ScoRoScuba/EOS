namespace EOS2.Web.Attributes
{
    using System;

    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class UniqueOrganizationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext != null)
            {
                var name = validationContext.ObjectInstance.GetType().GetProperty("Name")
                    .GetValue(validationContext.ObjectInstance).ToString();

                var id = (int)validationContext.ObjectInstance.GetType().GetProperty("Id")
                    .GetValue(validationContext.ObjectInstance);

                var organizationsService = DependencyResolver.Current.GetService<IOrganizationsService>();

                var foundResult = id > 0
                                           ? organizationsService.OrganizationExists(name, id)
                                           : organizationsService.OrganizationExists(name);

                if (foundResult)
                {
                    return new ValidationResult("[[[An organization with that name already Exists.]]]", new[] { "Name" });
                }
            }

            return ValidationResult.Success;
        }
    }
}