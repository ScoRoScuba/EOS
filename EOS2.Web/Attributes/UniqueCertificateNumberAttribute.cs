namespace EOS2.Web.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Certificate;

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class UniqueCertificateNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var model = (CertificateEditViewModel)value;

                var certificateService = DependencyResolver.Current.GetService<ICertificateService>();

                if (certificateService.CertificateExists(model.CertificateNumber, model.InstrumentId, model.Id, model.CustomerId))
                {
                    return new ValidationResult("[[[A certificate with that Certificate Number already exists for this customer.]]]", new[] { "CertificateNumber" });
                }
            }

            return ValidationResult.Success;
        }
    }
}