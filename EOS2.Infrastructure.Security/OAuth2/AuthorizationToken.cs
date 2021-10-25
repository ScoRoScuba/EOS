namespace EOS2.Infrastructure.Security.OAuth2
{
    using System.Net;

    using EOS2.Infrastructure.Interfaces.Security;

    public class AuthorizationToken : IAuthorizationToken 
    {
        public bool IsHttpError { get; set; }

        public HttpStatusCode HttpErrorStatusCode { get; set; }
    
        public string HttpErrorReason { get; set; }

        public string AccessToken { get; set; }

        public string IdentityToken { get; set; } 
  
        public string Error { get; set; }

        public bool IsError { get; set; }

        public long ExpiresIn { get; set; }

        public string TokenType { get; set; }

        public string RefreshToken { get; set; }
    }
}
