namespace EOS2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Mvc;

    using EOS2.Common.Exceptions;
    using EOS2.Common.Extensions;
    using EOS2.Infrastructure.Interfaces.Security;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Infrastructure.Interfaces.SessionManagement;
    using EOS2.Web.Attributes;
    using EOS2.Web.Code;
    using EOS2.Web.ViewModels;

    [IgnoreSessionExpired]
    public class AccountController : LoggingBaseController
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUserAppSession userSession;

        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        private readonly IOAuth2Client oAuth2Client;

        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Auth", Justification = "name indicates purpose")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o", Justification = "name indicates purpose")]
        public AccountController(
            IUserAppSession userSession,
            IAuthenticationService authenticationService,
            ILoggerService loggerService,
            IOAuth2Client oAuth2Client) : base(loggerService)
        {
            if (userSession == null) throw new ArgumentNullException("userSession");
            if (authenticationService == null) throw new ArgumentNullException("loggerService");
            if (oAuth2Client == null) throw new ArgumentNullException("oAuth2Client");

            this.userSession = userSession;
            this.authenticationService = authenticationService;
            this.oAuth2Client = oAuth2Client;
        }

        // GET: Account
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = "This value is passed on query string from external source and is only used within controller")]
        [AllowAnonymous]
        [NoActivityLogging]
        [IgnoreSessionExpired]
        public ActionResult SignIn(string returnUrl)
        {
            var state = Guid.NewGuid().ToString("N");
            var nonce = Guid.NewGuid().ToString("N");

            // set up the state for ther callback so we can prove we made it
            SetTempState(state, nonce);

            var url = oAuth2Client.GetAuthenticationUri(
                state,
                nonce,
                new Dictionary<string, string> { { "returnUrl", returnUrl } });

            return new RedirectResult(url.ToString());
        }

        private void SetTempState(string state, string nonce)
        {
            var tempId = new ClaimsIdentity("TempState");

            tempId.AddClaim(new Claim("state", state));
            tempId.AddClaim(new Claim("nonce", nonce));

            Request.GetOwinContext().Authentication.SignIn(tempId);
        }

        public ActionResult SignOut()
        {
            var identityToken = string.Empty;
 
            if (ClaimsPrincipal.Current.Claims.HasClaim("identity_token"))
            {
                identityToken = ClaimsPrincipal.Current.Claims.GetClaim("identity_token").Value;
            }

            Request.GetOwinContext().Authentication.SignOut("Cookies");
            userSession.CurrentUser = null;

            return new RedirectResult(oAuth2Client.GetSignOutUri(identityToken, "https://localhost:44301/").ToString());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SetLanguage(BaseViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (viewModel.LanguageReturnUrl == null) throw new UserLanguageException("LanguageReturnUrl");
            if (!viewModel.LanguageReturnUrl.IsWellFormedOriginalString()) throw new UserLanguageException(string.Format(CultureInfo.CurrentUICulture, "[[[Invalid URI : %0|||{0}|||]]]", viewModel.LanguageReturnUrl));

            // If valid 'languageTag' passed.
            if (LanguageHelper.IsLanguageValid(viewModel.SelectedLanguageId))
            {
                // Set persistent cookie in the client to remember the language choice.
                CookieHelper.SetHttpOnlyCookie("i18n.langtag", viewModel.SelectedLanguageId, DateTime.UtcNow.AddYears(1));
            }
            else
            {
                // Otherwise...delete any 'language' cookie in the client.
                CookieHelper.ClearCookie("i18n.langtag");
            }

            // Redirect user agent as approp.fs
            return this.Redirect(viewModel.LanguageReturnUrl.ToString());
        }

        [AllowAnonymous]
        public ActionResult SessionExpired()
        {
            authenticationService.SignOut();

            userSession.CurrentUser = null;

            return this.View();
        }
    }
}