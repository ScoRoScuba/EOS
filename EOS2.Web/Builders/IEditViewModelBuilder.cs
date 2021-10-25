namespace EOS2.Web.Builders
{
    public interface IEditViewModelBuilder<out TViewModel>
    {
        TViewModel Build(int? id);
    }
}
