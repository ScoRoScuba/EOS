namespace EOS2.Web.Builders 
{
    public interface IViewModelBuilder<out TViewModel>
    {
        TViewModel Build();
    }
}
