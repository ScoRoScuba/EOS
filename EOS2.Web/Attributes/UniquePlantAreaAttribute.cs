namespace EOS2.Web.Attributes
{
    using System;

    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.PlantAreas;

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class UniquePlantAreaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var model = (PlantAreaEditViewModel)value;

                var organizationsService = DependencyResolver.Current.GetService<IPlantAreaService>();

                var plantareaFoundResult = organizationsService.PlantAreaExists(model.Name, model.Id, model.SiteId);

                if (plantareaFoundResult)
                {
                    return new ValidationResult("[[[A Plant Area with that name already exists.]]]", new[] { "Name" });
                }
            }

            return ValidationResult.Success;
        }
    }
}