namespace EOS2.Model
{
    public class Schedule : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int FurnaceClassId { get; set; } 

        public virtual FurnaceClass FurnaceClass { get; set; }
        
        public int TypeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "There is no real confusion between ScheduleType & object.GetType()")]
        public virtual ScheduleType Type { get; set; }
      
        public int EquipmentId { get; set; }

        public string Description { get; set; }

        public int FrequencyId { get; set; }

        public virtual ScheduleFrequency Frequency { get; set; }

        public string SpecialConditions { get; set; }
    }
}
