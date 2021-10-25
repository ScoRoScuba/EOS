namespace EOS2.Web.Builders
{
    public interface IDomainModelBuilder<out TDomainModel, in TViewModel>
    {
        TDomainModel Build(TViewModel viewModel);
    }
}
