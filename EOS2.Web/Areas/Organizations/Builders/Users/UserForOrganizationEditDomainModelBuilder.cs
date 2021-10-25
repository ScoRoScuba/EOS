namespace EOS2.Web.Areas.Organizations.Builders.Users
{
    using System;

    using EOS2.Identity.Model;
    using EOS2.Identity.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Users;
    using EOS2.Web.Builders;

    using Microsoft.AspNet.Identity;

    public class UserForOrganizationEditDomainModelBuilder : IDomainModelBuilder<User, UserEditViewModel>
    {
        private readonly IdentityUserService userService;

        public UserForOrganizationEditDomainModelBuilder(IdentityUserService userService)
        {
            if (userService == null) throw new ArgumentNullException("userService");

            this.userService = userService;
        }

        public User Build(UserEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            User user = null;
            if (viewModel.Id > 0)
            {
                user = userService.FindById(viewModel.Id);
                if (user == null) throw new ArgumentException("[[[Invalid User Id]]]");

                user.Name = viewModel.Name;
                user.FamilyName = viewModel.FamilyName;
                user.Email = viewModel.Email;
                user.MiddleName = viewModel.MiddleName;
                user.PhoneNumber = viewModel.PhoneNumber;
            }
            else
            {
                user = new User
                           {
                               UserName = viewModel.UserName,
                               Name = viewModel.Name,
                               FamilyName = viewModel.FamilyName,
                               Email = viewModel.Email,
                               MiddleName = viewModel.MiddleName,
                               PhoneNumber = viewModel.PhoneNumber
                           };
            }
            
            return user;
        }
    }
}