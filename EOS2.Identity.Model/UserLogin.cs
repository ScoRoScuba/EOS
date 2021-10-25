namespace EOS2.Identity.Model
{
    using Microsoft.AspNet.Identity.EntityFramework;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login", Justification = "Identity Framework Naming Style")]
    public class UserLogin : IdentityUserLogin<int>
    {
    }
}
