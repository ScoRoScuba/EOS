namespace EOS2.Web.Areas.Organizations.Builders.InstrumentChannels
{
    using EOS2.Model;
    using EOS2.Web.Builders;

    public class ChannelsForInstrumentCriteria : IBuilderCriteria
    {
        public ChannelsForInstrumentCriteria(Instrument instrument)
        {
            Instrument = instrument;
        }

        public Instrument Instrument { get; private set; }
    }
}