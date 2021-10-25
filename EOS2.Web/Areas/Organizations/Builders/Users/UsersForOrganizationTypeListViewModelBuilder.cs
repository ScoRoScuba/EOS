namespace EOS2.Web.Areas.Organizations.Builders.Users
{
    using System;
    using System.Linq;

    using EOS2.Identity.Services;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Infrastructure.Interfaces.SessionManagement;
    using EOS2.Web.Areas.Organizations.ViewModels.Users;

    using EOS2.Web.Builders;

    using Microsoft.AspNet.Identity;

    public class UsersForOrganizationTypeListViewModelBuilder : IViewModelBuilder<UsersIndexViewModel>, IViewModelWithQueryBuilder<UsersIndexViewModel>
    {
        private readonly IUserAppSession userApplicationSession;

        private readonly IOrganizationsService organizationsService;

        private readonly IdentityUserService userService;

        public UsersForOrganizationTypeListViewModelBuilder(IUserAppSession userApplicationSession, IOrganizationsService organizationsService, IdentityUserService userService)
        {
            if (userApplicationSession == null) throw new ArgumentNullException("userApplicationSession");
            if (organizationsService == null) throw new ArgumentNullException("organizationsService");
            if (userService == null) throw new ArgumentNullException("userService");

            this.userApplicationSession = userApplicationSession;
            this.organizationsService = organizationsService;
            this.userService = userService;
        }

        public UsersIndexViewModel Build()
        {
            var usersInRole = organizationsService.GetUsersForOrganizationType(userApplicationSession.CurrentOrganizationType);

            return new UsersIndexViewModel
                                {
                                    Users = usersInRole.Select(userInRole => this.userService.FindById(userInRole.UserId)).ToList()
                                };
        }

        public UsersIndexViewModel Build(IBuilderCriteria criteria)
        {
            if (criteria == null) throw new ArgumentNullException("criteria");

            var userCriteria = criteria as UsersForOrganizationCriteria;
            if (userCriteria == null) throw new InvalidCastException("[[[wrong criteria type]]]");

            var organization = organizationsService.GetOrganization(userCriteria.OrganizationId);
            var usersInOrganization = organizationsService.GetUsers(userCriteria.OrganizationId, userCriteria.OrganizationType);

            return new UsersIndexViewModel()
                                {
                                    OrganizationName = organization.Name,
                                    OrganizationType = userCriteria.OrganizationType,
                                    OrganizationId = userCriteria.OrganizationId,
                                    Users = usersInOrganization.Select(userInRole => this.userService.FindById(userInRole.UserId)).ToList()
                                };
        }
    }
}