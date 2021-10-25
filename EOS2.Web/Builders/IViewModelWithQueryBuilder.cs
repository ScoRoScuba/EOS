namespace EOS2.Web.Builders
{
    public interface IViewModelWithQueryBuilder<in TQueryObject, out TViewModel>
    {
        TViewModel Build(TQueryObject criteria);
    }

    public interface IViewModelWithQueryBuilder<out TViewModel>
    {
        TViewModel Build(IBuilderCriteria criteria);
    }
}
