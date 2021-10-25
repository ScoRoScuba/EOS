namespace EOS2.Model.Enums
{
    using System.ComponentModel;

    public enum OrganizationType
    {
        Unknown,        // This is needed within security when checking claims
        [Description("EOS Owner")]
        EOSOwner,
        [Description("Portal Agent")]
        PortalAgent,
        [Description("Service Provider")]
        ServiceProvider,
        [Description("Customer")]
        Customer
    }
}
