namespace EOS2.Model
{
    using System.Collections.Generic;

    public class Organization : INamedEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "In place to ensure valid list object is always created")]
        public Organization()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            OrganizationRole = new List<OrganizationRole>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Requirment for Entity Framework")]
        public virtual IList<OrganizationRole> OrganizationRole { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Requirment for Entity Framework")]
        public virtual IList<Site> Sites { get; set; }
    }
}
