namespace EOS2.Web.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class IgnoreSessionExpiredAttribute : Attribute
    {
    }
}