namespace EOS2.Model
{
    using System.Collections.Generic;

    public class PlantArea : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SiteId { get; set; }

        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "EntityFramework")]
        public virtual ICollection<Equipment> Equipments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "EntityFramework")]
        public virtual ICollection<Instrument> Instruments { get; set; }

        // Navigation Properties
        public virtual Site Site { get; set; }
    }
}
