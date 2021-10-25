namespace EOS2.Web.Areas.Organizations.Builders.Users
{
    using System.Linq;

    using AutoMapper;

    using EOS2.Identity.Services;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Users;
    using EOS2.Web.Builders;

    using Microsoft.AspNet.Identity;

    public class UserForOrganizationViewModelBuilder : IEditViewModelBuilder<UserEditViewModel>
    {
        private readonly IdentityUserService userService;

        private readonly IOrganizationsService organizationsService;

        public UserForOrganizationViewModelBuilder(IdentityUserService userService, IOrganizationsService organizationsService)
        {
            this.userService = userService;
            this.organizationsService = organizationsService;
        }

        public UserEditViewModel Build(int? id)
        {
            var viewModel = new UserEditViewModel();

            if (id.HasValue)
            {
                var user = userService.FindById(id.Value);

                var usersOrganization = organizationsService.GetUsersOrganizationalRoles(id.Value).First().Organization;

                viewModel = Mapper.Map<UserEditViewModel>(user);

                viewModel.OrganizationName = usersOrganization.Name;
            }

            return viewModel;
        }
    }
}