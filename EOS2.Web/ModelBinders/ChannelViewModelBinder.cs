namespace EOS2.Web.ModelBinders
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;

    using EOS2.Common.Web.Extensions;
    using EOS2.Web.Areas.Organizations.ViewModels.Common;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ChannelView", Justification = "The suggested change to ChannelviewModel... would obscure the real purpose of this class.")]
    public class ChannelViewModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null) throw new ArgumentNullException("controllerContext");
            if (bindingContext == null) throw new ArgumentNullException("bindingContext");

            var prefixValue = bindingContext.ValueProvider.GetValue("__prefix");

            if (prefixValue != null)
            {
                var prefix = (string)prefixValue.ConvertTo(typeof(string), CultureInfo.InvariantCulture);

                if (!string.IsNullOrEmpty(prefix))
                {
                    var localViewData = GetModelFromBindingContextUsingPrefix(typeof(ChannelViewModel), prefix, bindingContext);

                    return localViewData;
                }
            } 

            return base.BindModel(controllerContext, bindingContext);
        }

        private static object GetModelFromBindingContextUsingPrefix(Type bindingModel, string prefix, ModelBindingContext context)
        {
            var properties = bindingModel.GetProperties();

            var modelInstance = Activator.CreateInstance(bindingModel);

            foreach (var property in properties)
            {
                var propertyName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", prefix, property.Name);

                var value = context.ValueProvider.GetValue(propertyName);

                if (value != null)
                {
                    if (!string.IsNullOrWhiteSpace(value.AttemptedValue))
                    {
                        var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                        property.SetValue(modelInstance, Convert.ChangeType(value.AttemptedValue, propertyType, CultureInfo.InvariantCulture), null);
                    }
                }            
            }

            context.ValidateModel(modelInstance);

            return modelInstance;
        }
    }
}