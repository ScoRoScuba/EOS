namespace EOS2.Infrastructure.Interfaces.SessionManagement
{
    using EOS2.Identity.Model;
    using EOS2.Model;
    using EOS2.Model.Enums;

    public interface IUserAppSession
    {
        User CurrentUser { get; set; }

        Organization CurrentOrganization { get; set; }

        OrganizationType CurrentOrganizationType { get; set; }
    }
}
