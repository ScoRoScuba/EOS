namespace EOS2.Infrastructure.Security
{
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    using EOS2.Common;

    public static class CertificatesManager
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "thumbPrint", Justification = "Name is Purpose")]
        public static X509Certificate2 LoadCertificateByThumbprint(string thumbPrint)
        {
            var certificateFinder = new X509CertificatesFinder(
                StoreLocation.LocalMachine,
                StoreName.My,
                X509FindType.FindByThumbprint);

            var certificate = certificateFinder.Find(thumbPrint);

            return certificate.FirstOrDefault();
        }
    }
}
