namespace EOS2.Common.Web.Validation
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    
    using EOS2.Common.Validation;

    public static class ModelStateDictionaryExtensions
    {
        public static void Merge(this ModelStateDictionary modelState, ServiceResultDictionary dictionary)
        {
            if (modelState == null) throw new ArgumentNullException("modelState");
            if (dictionary == null) throw new ArgumentNullException("dictionary");

            foreach (var item in dictionary)
            {
                foreach (var subitem in item.Value.Errors)
                {
                    if (subitem.Exception == null)
                    {
                        modelState.AddModelError(item.Key, subitem.ErrorMessage);
                    }
                    else
                    {
                        modelState.AddModelError(item.Key, subitem.Exception);
                    }
                }
            }
        }
    }
}
