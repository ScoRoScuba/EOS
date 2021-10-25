namespace EOS2.Web.Tests.TestStubs
{
    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;

    public class StubCertificateService : ICertificateService
    {
        private readonly bool certificateNumberExistsReturnValue;

        public StubCertificateService(bool certificateNumberExistsReturnValue)
        {
            this.certificateNumberExistsReturnValue = certificateNumberExistsReturnValue;
        }

        public ServiceResultDictionary Save(CertificateHeader certificate)
        {
            throw new System.NotImplementedException();
        }

        public CertificateHeader GetCertificate(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool CertificateExists(string certificateNumber, int instrumentId, int certificateId, int organizationId)
        {
            return certificateNumberExistsReturnValue;
        }
    }
}
