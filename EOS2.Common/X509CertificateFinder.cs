namespace EOS2.Common
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class X509CertificatesFinder
    {
        private readonly StoreLocation location;

        private readonly StoreName name;

        private readonly X509FindType findType;

        public X509CertificatesFinder(
            StoreLocation location,
            StoreName name,
            X509FindType findType)
        {
            this.location = location;
            this.name = name;
            this.findType = findType;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Will only be called by C#")]
        public IEnumerable<X509Certificate2> Find(
            object findValue,
            bool validOnly = true)
        {
            var store = new X509Store(
                name,
                location);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                var certColl = store.Certificates.Find(
                    findType,
                    findValue,
                    validOnly);
                return certColl.Cast<X509Certificate2>();
            }
            finally
            {
                store.Close();
            }
        }
    }
}
