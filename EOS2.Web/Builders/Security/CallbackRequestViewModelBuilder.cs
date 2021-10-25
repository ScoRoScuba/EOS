namespace EOS2.Web.Builders.Security
{
    using System.Web;

    using EOS2.Web.ViewModels.Security;

    public class CallbackRequestViewModelBuilder : IRequestViewModelBuilder<CallbackReplyViewData>
    {
        public CallbackReplyViewData Build(HttpRequest request)
        {
            return new CallbackReplyViewData();
        }
    }
}