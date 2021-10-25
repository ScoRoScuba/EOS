namespace EOS2.Infrastructure.Security.OAuth2
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using System.Web;

    using EOS2.Common;
    using EOS2.Infrastructure.Interfaces.Security;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Infrastructure.Security.Configuration;

    using Thinktecture.IdentityModel.Client;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "EOSO", Justification = "Name is correct")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Auth", Justification = "Name is as expected for type")]
    // ReSharper disable once InconsistentNaming
    public class EOSOAuth2Client : IOAuth2Client
    {
        private const string DefaultAuthorizeEndPoint = "/connect/authorize";

        private const string DefaultEndSessionEndPoint = "/connect/endsession";

        private const string DefaultTokenEndPoint = "/connect/token";

        private const string DefaultUserInfoEndPoint = "/connect/userinfo";

        private readonly ILoggerService loggerService;

        private readonly IdentityConfigurationSection identityConfiguration;

        public EOSOAuth2Client(ILoggerService loggerService)
        {
            if (loggerService == null) throw new ArgumentNullException("loggerService"); 
            this.loggerService = loggerService;

            identityConfiguration = IdentityServerConfigurationManager.Configuration;
        }

        private string AuthorizeEndpoint
        {
            get
            {
                return string.IsNullOrWhiteSpace(identityConfiguration.Endpoints.AuthorizeEndpoint.Value)
                            ? identityConfiguration.IdentityServerUri.Value + DefaultAuthorizeEndPoint
                            : identityConfiguration.IdentityServerUri.Value + identityConfiguration.Endpoints.AuthorizeEndpoint.Value;
            }
        }

        private string TokenEndpoint
        {
            get
            {
                return string.IsNullOrWhiteSpace(identityConfiguration.Endpoints.TokenEndpoint.Value)
                            ? identityConfiguration.IdentityServerUri.Value + DefaultTokenEndPoint
                            : identityConfiguration.IdentityServerUri.Value + identityConfiguration.Endpoints.TokenEndpoint.Value;
            }
        }

        private string UserInfoEndpoint
        {
            get
            {
                return string.IsNullOrWhiteSpace(identityConfiguration.Endpoints.UserInfoEndpoint.Value)
                            ? identityConfiguration.IdentityServerUri.Value + DefaultUserInfoEndPoint
                            : identityConfiguration.IdentityServerUri.Value + identityConfiguration.Endpoints.UserInfoEndpoint.Value;
            }
        }

        private string EndSessionEndpoint
        {
            get
            {
                return string.IsNullOrWhiteSpace(identityConfiguration.Endpoints.EndSessionEndpoint.Value)
                            ? identityConfiguration.IdentityServerUri.Value + DefaultEndSessionEndPoint
                            : identityConfiguration.IdentityServerUri.Value + identityConfiguration.Endpoints.EndSessionEndpoint.Value;
            }
        }
          
        public Uri GetAuthenticationUri(string state, string nonce, Dictionary<string, string> additionalValues)
        {
            var client = new OAuth2Client(new Uri(this.AuthorizeEndpoint));

            var url = client.CreateCodeFlowUrl(
                clientId: identityConfiguration.ClientId.Value,
                scope: identityConfiguration.Scopes.Value,
                redirectUri: identityConfiguration.RedirectUri.Value,
                state: state,
                nonce: nonce,
                additionalValues: additionalValues);

            return new Uri(url);
        }

        public Uri GetSignOutUri()
        {       
            return new Uri(this.EndSessionEndpoint);        
        }

        public Uri GetSignOutUri(string identityToken, string redirectUrl)
        {
            var uriBuilder = new UriBuilder(GetSignOutUri());

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["id_token_hint"] = identityToken;
            query["post_logout_redirect_uri"] = redirectUrl;

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        public async Task<IAuthorizationToken> GetAuthorizationTokenAsync(string accessCode)
        {
            var client = new OAuth2Client(new Uri(this.TokenEndpoint), identityConfiguration.ClientId.Value, identityConfiguration.Secret.Value);

            var response = await client.RequestAuthorizationCodeAsync(accessCode, identityConfiguration.RedirectUri.Value);

            return new AuthorizationToken()
                       {
                           IsHttpError = response.IsHttpError,
                           HttpErrorStatusCode = response.HttpErrorStatusCode,
                           HttpErrorReason = response.HttpErrorReason,
                           AccessToken = response.AccessToken,
                           IdentityToken = response.IdentityToken,
                           Error = response.Error,
                           IsError = response.IsError,
                           ExpiresIn = response.ExpiresIn,
                           TokenType = response.TokenType,
                           RefreshToken = response.RefreshToken
                       };
        }

        public async Task<IEnumerable<Claim>> GetUsersClaimsAsync(string accessToken)
        {
            var userInfoClient = new UserInfoClient(new Uri(this.UserInfoEndpoint), accessToken);

            var userInfo = await userInfoClient.GetAsync();

            var claims = new List<Claim>();
            userInfo.Claims.ToList().ForEach(ui => claims.Add(new Claim(ui.Item1, ui.Item2)));

            return claims;        
        }

        public ClaimsPrincipal GetClaimsPrincipal(string accessToken)
        {
            loggerService.Log("oAuth2Client - GetClaimsPrincipal - Start");

            var signingCertificate =
                CertificatesManager.LoadCertificateByThumbprint(identityConfiguration.CertThumbprint.Value);
            
            var parameters = new TokenValidationParameters
            {
                ValidAudience = identityConfiguration.Audience.Value, // Who is interested in this data
                ValidIssuer = identityConfiguration.ValidIssuer.Value, // who issued certificate 
                IssuerSigningToken = new X509SecurityToken(signingCertificate)
            };

            SecurityToken jwt;
            try
            {            
                var principal = new JwtSecurityTokenHandler().ValidateToken(accessToken, parameters, out jwt);

                loggerService.Log("oAuth2Client - GetClaimsPrincipal - Finished");
                return principal;
            }
            catch (ArgumentException aex)
            {
                loggerService.LogFatal("Cert Error", aex);
                return null;
            }
        }
    }
}
