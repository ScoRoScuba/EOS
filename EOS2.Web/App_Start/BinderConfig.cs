namespace EOS2.Web
{
    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.ModelBinders;

    public static class BinderConfig
    {
        public static void RegisterBinders()
        {
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(ChannelViewModel), new ChannelViewModelBinder());
        }
    }
}