namespace EOS2.Web.Areas.Organizations.Controllers
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;

    using EOS2.Common.Validation;
    using EOS2.Common.Web.Validation;
    using EOS2.Identity.Model;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Infrastructure.Interfaces.SessionManagement;
    using EOS2.Model.Enums;
    using EOS2.Web.Areas.Organizations.ViewModels.Users;
    using EOS2.Web.Builders;
    using EOS2.Web.Code;
    using EOS2.Web.Filters;

    public class UsersController : BaseController
    {
        private readonly IViewModelWithQueryBuilder<UsersIndexViewModel> usersForOrganizationAndTypeListViewModelBuilder;

        private readonly IDomainModelBuilder<User, UserEditViewModel> userEditDomainModelBuilder;

        private readonly IEditViewModelBuilder<UserEditViewModel> userViewModelBuilder;

        private readonly IUserAppSession userApplicationSession;

        private readonly IAuthenticationService authenticationService;

        private readonly IOrganizationsService organizationsService;

        private readonly IEditViewModelBuilder<UserPasswordViewModel> passwordForUserEditViewModelBuilder;

        public UsersController(
            IUserAppSession userApplicationSession,
            IAuthenticationService authenticationService,
            IOrganizationsService organizationsService,
            IViewModelWithQueryBuilder<UsersIndexViewModel> usersForOrganizationAndTypeListViewModelBuilder,
            IDomainModelBuilder<User, UserEditViewModel> userEditDomainModelBuilder,
            IEditViewModelBuilder<UserEditViewModel> userViewModelBuilder,
            IEditViewModelBuilder<UserPasswordViewModel> passwordForUserEditViewModelBuilder)
        {
            if (userApplicationSession == null) throw new ArgumentNullException("userApplicationSession");
            if (authenticationService == null) throw new ArgumentNullException("authenticationService");
            if (organizationsService == null) throw new ArgumentNullException("organizationsService");
            if (usersForOrganizationAndTypeListViewModelBuilder == null) throw new ArgumentNullException("usersForOrganizationAndTypeListViewModelBuilder");
            if (userEditDomainModelBuilder == null) throw new ArgumentNullException("userEditDomainModelBuilder");
            if (userViewModelBuilder == null) throw new ArgumentNullException("userViewModelBuilder");
            if (passwordForUserEditViewModelBuilder == null) throw new ArgumentNullException("passwordForUserEditViewModelBuilder");

            this.userApplicationSession = userApplicationSession;
            this.authenticationService = authenticationService;
            this.organizationsService = organizationsService;
            this.userEditDomainModelBuilder = userEditDomainModelBuilder;
            this.userViewModelBuilder = userViewModelBuilder;
            this.usersForOrganizationAndTypeListViewModelBuilder = usersForOrganizationAndTypeListViewModelBuilder;
            this.passwordForUserEditViewModelBuilder = passwordForUserEditViewModelBuilder;
        }

        // GET: Organizations/Users
        public ActionResult Index(OrganizationType? organizationType, int? organizationId)
        {
            if (organizationType.HasValue && organizationId.HasValue)
                return View(usersForOrganizationAndTypeListViewModelBuilder.Build(new UsersForOrganizationCriteria(organizationType.Value, organizationId.Value)));
            
            return View(usersForOrganizationAndTypeListViewModelBuilder.Build(new UsersForOrganizationCriteria(userApplicationSession.CurrentOrganizationType, userApplicationSession.CurrentOrganization.Id)));
        }

        public ActionResult Add(OrganizationType? organizationType, int? organizationId)
        {
            var viewModel = new UserEditViewModel
                                {
                                    OrganizationId = organizationId,
                                    OrganizationType = organizationType,
                                    OrganizationName = organizationId.HasValue ? organizationsService.GetOrganization(organizationId.Value).Name : userApplicationSession.CurrentOrganization.Name
                                };

            return View("Edit", viewModel);
        }

        public ActionResult Edit(OrganizationType? organizationType, int? organizationId, int? id)
        {
            if (!id.HasValue) throw new ArgumentNullException("id");

            var viewModel = userViewModelBuilder.Build(id);

            viewModel.OrganizationId = organizationId;
            viewModel.OrganizationType = organizationType;
            viewModel.OrganizationName = organizationId.HasValue
                                             ? organizationsService.GetOrganization(organizationId.Value).Name
                                             : userApplicationSession.CurrentOrganization.Name;

            return View("Edit", viewModel);
        }

        [HttpPost]
        public ActionResult Create(UserEditViewModel editViewModel)
        {
            if (editViewModel == null) throw new ArgumentNullException("editViewModel");

            if (ModelState.IsValid)
            {
                var user = userEditDomainModelBuilder.Build(editViewModel);

                ServiceResultDictionary resultDictionary = authenticationService.CreateUser(user);

                if (!resultDictionary.HasErrors)
                {
                    object routeValues = null;
                    if (editViewModel.OrganizationType.HasValue && editViewModel.OrganizationId.HasValue)
                    {
                        resultDictionary = organizationsService.AddUserToOrganization(editViewModel.OrganizationId.Value, editViewModel.OrganizationType.Value, user.Id);
                        routeValues =
                            new
                                {
                                    organizationType = editViewModel.OrganizationType.Value,
                                    organizationId = editViewModel.OrganizationId.Value,
                                    user.Id
                                };
                    }
                    else
                    {
                        resultDictionary = organizationsService.AddUserToOrganization(userApplicationSession.CurrentOrganization.Id, userApplicationSession.CurrentOrganizationType, user.Id);
                        routeValues = 
                            new
                                {
                                    organizationType = userApplicationSession.CurrentOrganizationType,
                                    organizationId = userApplicationSession.CurrentOrganization.Id,
                                    user.Id
                                };
                    }

                    if (!resultDictionary.HasErrors)
                    {
                        TempData["ControllerActionMessage"] = string.Format(CultureInfo.CurrentCulture, "[[[User saved successfully]]]");
                        return RedirectToAction("Edit", routeValues);
                    }
                }

                ModelState.Merge(resultDictionary);
            }

            editViewModel.OrganizationName = editViewModel.OrganizationId.HasValue
                                             ? organizationsService.GetOrganization(editViewModel.OrganizationId.Value).Name
                                             : userApplicationSession.CurrentOrganization.Name;

            return View("Edit", editViewModel);
        }

        [HttpPost]
        public ActionResult Update(UserEditViewModel editViewModel)
        {
            // CA1062 rule not consistant did not have to do this until making sure OrganizationName was set
            // as it should have failed because of the model builder
            if (editViewModel == null) throw new ArgumentNullException("editViewModel");

            if (ModelState.IsValid)
            {
                var user = userEditDomainModelBuilder.Build(editViewModel);

                var result = authenticationService.UpdateUser(user);
                if (!result.HasErrors)
                {
                    TempData["ControllerActionMessage"] = string.Format(CultureInfo.CurrentCulture, "[[[User Updated successfully]]]");                    
                }
            }

            editViewModel.OrganizationName = editViewModel.OrganizationId.HasValue
                                             ? organizationsService.GetOrganization(editViewModel.OrganizationId.Value).Name
                                             : userApplicationSession.CurrentOrganization.Name;

            return View("Edit", editViewModel);
        }

        public ActionResult Password(int? id)
        {
            if (!id.HasValue) throw new ArgumentNullException("id");

            var viewModel = passwordForUserEditViewModelBuilder.Build(id);

            return View("Password", viewModel);
        }

        [HttpPost]
        public ActionResult Password(UserPasswordViewModel passwordViewModel)
        {
            if (passwordViewModel == null) throw new ArgumentNullException("passwordViewModel");

            if (ModelState.IsValid)
            {
                var result = authenticationService.SetPassword(passwordViewModel.Id, passwordViewModel.Password);
                if (!result.HasErrors)
                {
                    TempData["ControllerActionMessage"] = string.Format(CultureInfo.CurrentCulture, "[[[Password Updated successfully]]]");
                    return RedirectToAction("Edit", new { id = passwordViewModel.Id });
                }
            }

            return View("Password", passwordViewModel);            
        }
    }
}