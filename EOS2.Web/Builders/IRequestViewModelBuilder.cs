namespace EOS2.Web.Builders
{
    using System.Web;

    public interface IRequestViewModelBuilder<out TViewModel>
    {
        TViewModel Build(HttpRequest request);
    }
}
