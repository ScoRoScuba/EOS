namespace EOS2.Web.Areas.Organizations.Builders.Users
{
    using EOS2.Identity.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Users;
    using EOS2.Web.Builders;

    using Microsoft.AspNet.Identity;

    public class PasswordForUserViewModelBuilder : IEditViewModelBuilder<UserPasswordViewModel>
    {
        private readonly IdentityUserService userService;

        public PasswordForUserViewModelBuilder(IdentityUserService userService)
        {
            this.userService = userService;
        }

        public UserPasswordViewModel Build(int? id)
        {
            var viewModel = new UserPasswordViewModel();

            if (id.HasValue)
            {
                var user = userService.FindById(id.Value);
                viewModel = new UserPasswordViewModel
                       {
                            UserName = user.UserName, 
                            Name = user.FullName, 
                            Id = user.Id
                       };          
            }

            return viewModel;
        }
    }
}