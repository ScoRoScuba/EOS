namespace EOS2.Data.Mappings.Security
{
    using System.Data.Entity.ModelConfiguration;

    using global::EOS2.Identity.Model;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login", Justification = "Identity Framework naming issue, outside scope of these checks")]
    public class UserLoginMappings : EntityTypeConfiguration<UserLogin>
    {
    }
}
