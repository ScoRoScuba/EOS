namespace EOS2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using EOS2.Common.Extensions;
    using EOS2.Identity.Services;
    using EOS2.Infrastructure.Interfaces.Security;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Infrastructure.Interfaces.SessionManagement;
    using EOS2.Infrastructure.Security.OAuth2;
    using EOS2.Web.Attributes;
    using EOS2.Web.ViewModels.Security;

    using Microsoft.AspNet.Identity;

    using Thinktecture.IdentityModel.Client;

    [IgnoreSessionExpired]
    [AllowAnonymous]
    public class CallbackController : Controller
    {
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        private readonly IOAuth2Client oAuth2Client;
        private readonly IClaimsBuilderService applicationClaimsBuilderService;            
        private readonly ILoggerService loggerService;

        private readonly IUserAppSession userAppSession;
        private readonly IdentityUserService identityUserService;

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Auth", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        public CallbackController(
                            IClaimsBuilderService applicationClaimsBuilderService,
                            IOAuth2Client oAuth2Client,
                            ILoggerService loggerService,
                            IUserAppSession userAppSession,
                            IdentityUserService identityUserService)
        {
            if (applicationClaimsBuilderService == null) throw new ArgumentNullException("applicationClaimsBuilderService");
            if (oAuth2Client == null) throw new ArgumentNullException("oAuth2Client");
            if (loggerService == null) throw new ArgumentNullException("loggerService");
            if (userAppSession == null) throw new ArgumentNullException("userAppSession");
            if (identityUserService == null) throw new ArgumentNullException("identityUserService");

            this.applicationClaimsBuilderService = applicationClaimsBuilderService;
            this.oAuth2Client = oAuth2Client;
            this.loggerService = loggerService;
            this.userAppSession = userAppSession;
            this.identityUserService = identityUserService;
        }

        public async Task<ActionResult> Index(CallbackReplyViewData viewData)
        {
            loggerService.Log("Authentication Callback Process - Starting");

            var requestState = await GetTempStateAsync();

            if (!ValidCallback(requestState.Item1, viewData.state))
            {
                return new RedirectResult("~/Error/BadRequest");
            }
                 
            var response = await oAuth2Client.GetAuthorizationTokenAsync(viewData.code);

            if (response.IsError || response.IsHttpError)
            {
                return new RedirectResult("~/Error/BadRequest");                
            }

            var authorizationReponse = response as AuthorizationToken;

            var principal = oAuth2Client.GetClaimsPrincipal(authorizationReponse.IdentityToken);
            if (principal == null)
            {
                return new RedirectResult("~/Error/BadRequest");                                                
            }

            if (!ValidateClaimPrincipal(principal, requestState.Item2))
            {
                return new RedirectResult("~/Error/BadRequest");                                
            }

            var claims = new List<Claim>
                             {
                                 new Claim("identity_token", authorizationReponse.IdentityToken)
                             };

            if (!string.IsNullOrWhiteSpace(authorizationReponse.RefreshToken))
            {
                claims.Add(new Claim("refresh_token", authorizationReponse.RefreshToken));
            }

            if (!string.IsNullOrWhiteSpace(authorizationReponse.AccessToken))
            {
                var usersClaims = await oAuth2Client.GetUsersClaimsAsync(authorizationReponse.AccessToken);

                claims.AddRange(usersClaims);

                claims.Add(new Claim("access_token", authorizationReponse.AccessToken));
                claims.Add(new Claim("expires_at", (DateTime.UtcNow.ToEpochTime() + authorizationReponse.ExpiresIn).ToDateTimeFromEpoch().ToString()));

                if (usersClaims.HasClaim("sub"))
                {
                    var claimUserId = usersClaims.GetClaim("sub");
                    int userId = int.Parse(claimUserId.Value);

                    claims.AddRange(applicationClaimsBuilderService.GetUsersApplicationClaims(userId));
                    claims.AddRange(applicationClaimsBuilderService.GetUsersClaims(userId));

                    userAppSession.CurrentUser = identityUserService.FindById(userId);
                }
            }

            var identity = new ClaimsIdentity(claims, "Cookies");

            Request.GetOwinContext().Authentication.SignIn(identity);

            loggerService.Log("Authentication Callback Process - Ending - Signed In");

            return new RedirectResult(string.IsNullOrWhiteSpace(viewData.returnUrl) ? "~/" : viewData.returnUrl);
        }

        private static bool ValidCallback(string savedState, string callbackState)
        {
            return callbackState.Equals(savedState, StringComparison.Ordinal);
        }

        private bool ValidateClaimPrincipal(ClaimsPrincipal principal, string nonce)
        {
            // validate nonce
            var nonceClaim = principal.FindFirst("nonce");

            if (!string.Equals(nonceClaim.Value, nonce, StringComparison.Ordinal))
            {
                loggerService.LogFatal("invalid nonce", new ArgumentException("invalid nonce"));
                return false;
            }

            return true;
        }

        private async Task<Tuple<string, string>> GetTempStateAsync()
        {
            var data = await Request.GetOwinContext().Authentication.AuthenticateAsync("TempState");

            var state = data.Identity.FindFirst("state").Value;
            var nonce = data.Identity.FindFirst("nonce").Value;

            return Tuple.Create(state, nonce);
        }
    }
}