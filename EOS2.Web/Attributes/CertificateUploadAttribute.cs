namespace EOS2.Web.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using EOS2.Web.Areas.Organizations.ViewModels.Certificate;

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class CertificateUploadAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var model = (CertificateEditViewModel)value;

                var file = model.DetailViewModel;

                if (model.Id == 0 && file.File == null)
                {
                    return new ValidationResult(
                        "[[[A file must be supplied to be uploaded]]]",
                        new[] { "DetailViewModel" });
                }

                if (file.File != null)
                {
                    var allowedFileExtensions = new[] { ".pdf" };

                    if (
                        !allowedFileExtensions.Contains(
                            file.File.FileName.Substring(file.File.FileName.LastIndexOf('.')).ToLowerInvariant()))
                    {
                        return
                            new ValidationResult(
                                "[[[File must be of type ]]]" + string.Join(", ", allowedFileExtensions),
                                new[] { "DetailViewModel" });
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}