namespace EOS2.Web.Builders
{
    public interface IEditViewPartialModelBuilder<TViewModel> : IEditViewModelBuilder<TViewModel>
    {
        TViewModel Rebuild(TViewModel viewModel);
    }
}
