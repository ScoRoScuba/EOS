namespace EOS2.Model
{
    using System.Collections.Generic;

    public class Equipment : INamedEntity
    {
        public int Id { get; set; }

        public int PlantAreaId { get; set; }
        
        public virtual PlantArea PlantArea { get; set; }   

        public string Name { get; set; }

        public string Make { get; set; }
        
        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public int TypeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "There is no real confusion between EquipmentType & object.GetType()")]
        public virtual EquipmentType Type { get; set; }

        public string Notes { get; set; }

        public string Description { get; set; }

        // TODO: Remove this once this line it used.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Temporary suppression.")]
        public virtual ICollection<Schedule> Schedules { get; private set; }

        // TODO: Remove this when Schedules is used.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "This will be used later in the project.")]
        public virtual ICollection<Channel> RecordByChannels { get; private set; }
    }
}
