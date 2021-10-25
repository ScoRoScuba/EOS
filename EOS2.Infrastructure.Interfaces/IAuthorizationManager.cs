namespace EOS2.Infrastructure.Security
{
    using System.Security.Claims;

    public interface IAuthorizationManager
    {
        bool CheckAccess(AuthorizationContext authorizationContext);
    }
}
