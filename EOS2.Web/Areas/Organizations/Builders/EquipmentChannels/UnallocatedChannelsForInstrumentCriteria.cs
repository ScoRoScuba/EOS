namespace EOS2.Web.Areas.Organizations.Builders.EquipmentChannels
{
    using EOS2.Web.Builders;

    public class UnallocatedChannelsForInstrumentCriteria : IBuilderCriteria
    {
        public UnallocatedChannelsForInstrumentCriteria(int instrumentId, int equipmentToSelectId)
        {
            InstrumentId = instrumentId;
            EquipmentToSelectId = equipmentToSelectId;
        }

        public int InstrumentId { get; private set; }

        public int EquipmentToSelectId { get; private set; }
    }
}