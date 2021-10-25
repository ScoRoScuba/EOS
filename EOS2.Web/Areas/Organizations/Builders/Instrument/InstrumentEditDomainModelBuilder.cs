namespace EOS2.Web.Areas.Organizations.Builders.Instrument
{
    using AutoMapper;

    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Instruments;
    using EOS2.Web.Builders;

    public class InstrumentEditDomainModelBuilder : IDomainModelBuilder<Instrument, InstrumentEditViewModel>
    {
        public Instrument Build(InstrumentEditViewModel viewModel)
        {
            return Mapper.Map<Instrument>(viewModel);
        }
    }
}