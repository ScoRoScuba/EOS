namespace EOS2.Web.Attributes
{
    using System;

    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Equipments;

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class UniqueEquipmentAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var model = (EquipmentEditViewModel)value;

                var organizationsService = DependencyResolver.Current.GetService<IEquipmentService>();

                // TODO: Below commented out until we know what fields are mandatory (would only make sense if at least serial number is mand)
                ////var equipmentFoundResult = organizationsService.EquipmentExists(model.Make, model.SerialNumber, model.Id);
                ////if (equipmentFoundResult)
                ////{
                ////   return new ValidationResult("[[[A piece of Equipment with that Serial Number already exists.]]]", new[] { "SerialNumber" });
                ////}

                var equipmentFoundResult = organizationsService.EquipmentExists(model.Name, model.PlantAreaId, model.Id);
                if (equipmentFoundResult)
                {
                    return new ValidationResult("[[[A piece of Equipment with that Name already exists for this Plant Area.]]]", new[] { "Name" });
                }
            }

            return ValidationResult.Success;
        }
    }
}