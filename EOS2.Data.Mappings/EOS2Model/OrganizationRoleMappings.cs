namespace EOS2.Data.Mappings.EOS2Model
{    
    using System.Data.Entity.ModelConfiguration;

    using EOS2.Model;

    public class OrganizationRoleMappings : EntityTypeConfiguration<OrganizationRole>
    {
        public OrganizationRoleMappings()
        {
            Property(x => x.Comment).HasMaxLength(512);
            Property(x => x.FromDate).IsRequired();
        }
    }
}
