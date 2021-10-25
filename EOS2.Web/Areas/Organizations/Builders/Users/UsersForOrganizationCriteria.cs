namespace EOS2.Web.Builders
{
    using EOS2.Model.Enums;

    public class UsersForOrganizationCriteria : IBuilderCriteria
    {
        public UsersForOrganizationCriteria(OrganizationType organizationType, int organizationId)
        {
            OrganizationType = organizationType;
            OrganizationId = organizationId;
        }

        public OrganizationType OrganizationType { get; private set; }

        public int OrganizationId { get; private set; }
    }
}