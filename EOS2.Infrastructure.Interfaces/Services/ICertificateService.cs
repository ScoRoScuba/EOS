namespace EOS2.Infrastructure.Interfaces.Services
{
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Model;

    public interface ICertificateService
    {
        ServiceResultDictionary Save(CertificateHeader certificate);

        CertificateHeader GetCertificate(int id);

        bool CertificateExists(string certificateNumber, int instrumentId, int certificateId, int organizationId);
    }
}
