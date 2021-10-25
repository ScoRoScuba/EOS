namespace EOS2.Model
{
    public class Channel : INamedEntity
    {
        public int Id { get; set; }

        public int InstrumentId { get; set; }

        public virtual Instrument Instrument { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public int? TypeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Ubiqutous language")]
        public virtual ChannelType Type { get; set; }

        public int? ConnectedToEquipmentId { get; set; }

        public virtual Equipment ConnectedToEquipment { get; set; }

        public int? ScheduleFrequencyId { get; set; }

        public virtual ScheduleFrequency Schedule { get; set; }
    }
}
