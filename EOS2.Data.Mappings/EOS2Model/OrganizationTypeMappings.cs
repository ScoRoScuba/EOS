namespace EOS2.Data.Mappings.EOS2
{
    using System.Data.Entity.ModelConfiguration;
 
    using global::EOS2.Model;

    public class OrganizationTypeMappings : EntityTypeConfiguration<OrganizationType>
    {
        public OrganizationTypeMappings()
        {
            Property(x => x.Description).HasMaxLength(512);
        }
    }
}
