namespace EOS2.Web.Areas.Organizations.Builders.EquipmentChannels
{
    using EOS2.Model;
    using EOS2.Web.Builders;

    public class ChannelsAllocatedToEquipmentCriteria : IBuilderCriteria
    {
        public ChannelsAllocatedToEquipmentCriteria(Equipment equipment, Instrument instrument)
        {
            this.Equipment = equipment;
            this.Instrument = instrument;
        }

        public Equipment Equipment { get; private set; }

        public Instrument Instrument { get; private set; }
    }
}