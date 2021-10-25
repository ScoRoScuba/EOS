namespace TestingHelpers
{
    using System.Web.Mvc;

    public static class ModelStateTester
    {
        public static ModelStateDictionary TryUpdateModel<TModel>(TModel model, IValueProvider valueProvider) where TModel : class
        {
            var modelState = new ModelStateDictionary();
            var controllerContext = new ControllerContext();

            var binder = ModelBinders.Binders.GetBinder(typeof(TModel));
            var bindingContext = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(
                    () => model, typeof(TModel)),
                ModelState = modelState,
                ValueProvider = valueProvider
            };

            binder.BindModel(controllerContext, bindingContext);

            return modelState;
        }
    }
}
