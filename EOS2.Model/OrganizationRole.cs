namespace EOS2.Model
{
    using System;
    using System.Collections.Generic;
    using EOS2.Model.Enums;

    public class OrganizationRole : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime FromDate { get; set; }
        
        public DateTime? ToDate { get; set; }

        public string Comment { get; set; }

        public int? OrganizationId { get; set; }
        
        public virtual Organization Organization { get; set; }

        public int? OrganizationTypeId { get; set; }
        
        public OrganizationType OrganizationType { get; set; }

        /// <summary>
        /// This is used to model the relationship between a Portal Agent and a Service Provider (ONLY)
        /// </summary>
        public virtual OrganizationRole ParentOrganization { get; set; }
        
        public int? ParentOrganizationId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Design Choice")]
        public virtual IList<OrganizationRoleUser> RoleUsers { get; set; }
    }
}
