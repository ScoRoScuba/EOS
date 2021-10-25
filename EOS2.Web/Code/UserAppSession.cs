namespace EOS2.Web.Code
{
    using EOS2.Infrastructure.Interfaces.SessionManagement;
    using EOS2.Model;
    using EOS2.Model.Enums;

    public class UserAppSession : IUserAppSession
    {
        public Identity.Model.User CurrentUser { get; set; }

        public OrganizationType CurrentOrganizationType { get; set; }

        public string UsersCulture { get; set; }

        public Organization CurrentOrganization { get; set; }
    }
}