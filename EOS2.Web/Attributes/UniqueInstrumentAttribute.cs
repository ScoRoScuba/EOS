namespace EOS2.Web.Attributes
{
    using System;

    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Instruments;

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class UniqueInstrumentAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var model = (InstrumentEditViewModel)value;

                var organizationsService = DependencyResolver.Current.GetService<IInstrumentService>();

                // TODO: Below commented out until we know what fields are mandatory (would only make sense if at least serial number is mand)
                ////var instrumentFoundResult = organizationsService.InstrumentExists(model.Make, model.SerialNumber, model.Id);
                ////if (instrumentFoundResult)
                ////{
                ////    return new ValidationResult("[[[An Instrument with that Serial Number already exists.]]]", new[] { "SerialNumber" });
                ////}

                var instrumentFoundResult = organizationsService.InstrumentExists(model.Name, model.PlantAreaId, model.Id);
                if (instrumentFoundResult)
                {
                    return new ValidationResult("[[[An instrument with that Name already exists for this Plant Area.]]]", new[] { "Name" });
                }
            }

            return ValidationResult.Success;
        }
    }
}