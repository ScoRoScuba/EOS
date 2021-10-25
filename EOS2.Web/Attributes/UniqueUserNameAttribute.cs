namespace EOS2.Web.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using EOS2.Identity.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Users;

    using Microsoft.AspNet.Identity;

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class UniqueUserNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var model = (UserEditViewModel)value;

                var identityService = DependencyResolver.Current.GetService<IdentityUserService>();

                if (model.Id > 0)
                {
                    var userFound = identityService.FindByName(model.UserName);
                    if (userFound != null)
                    {
                        return new ValidationResult("[[[A user with this name already Exists.]]]", new[] { "UserName" });
                    }
                }
            }

            return ValidationResult.Success;            
        }
    }
}