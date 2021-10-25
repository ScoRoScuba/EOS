namespace EOS2.Model
{
    using System.Collections.Generic;

    public class Site : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public virtual int OrganizationId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Design Choice")]
        public virtual IList<PlantArea> PlantAreas { get; set; }

        // Navigation Properties
        public virtual Organization Customer { get; set; }
    }
}
