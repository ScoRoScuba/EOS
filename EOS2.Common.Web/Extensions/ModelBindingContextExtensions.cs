namespace EOS2.Common.Web.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public static class ModelBindingContextExtensions
    {
        public static void ValidateModel(this ModelBindingContext bindingContext, object model)
        {
            if (bindingContext == null) throw new ArgumentNullException("bindingContext");
            if (model == null) throw new ArgumentNullException("model");

            var validationResults = new HashSet<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, new ValidationContext(model, null, null), validationResults, true);

            if (isValid) return;

            var resultsGroupedByMembers = validationResults
                .SelectMany(_ => _.MemberNames
                                     .Select(
                                         x => new                                
                                                  {
                                                      MemberName = x ?? string.Empty, 
                                                      Error = _.ErrorMessage
                                                  }))
                .GroupBy(_ => _.MemberName);

            foreach (var member in resultsGroupedByMembers)
            {
                bindingContext.ModelState.AddModelError(
                    member.Key,
                    string.Join(". ", member.Select(_ => _.Error)));
            }
        }
    }
}
