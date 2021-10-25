namespace EOS2.Services.BusinessDomain
{
    using System;

    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;

    public class CertificateService : ICertificateService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IRepository<CertificateHeader> repository;

        public CertificateService(IUnitOfWork unitOfWork, IRepository<CertificateHeader> repository)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            if (repository == null) throw new ArgumentNullException("repository");

            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        public CertificateHeader GetCertificate(int id)
        {
            var certificate = repository.Find(i => i.Id == id);

            return certificate;
        }

        public ServiceResultDictionary Save(CertificateHeader certificate)
        {
            if (certificate == null) throw new ArgumentNullException("certificate");

            var serviceResult = new ServiceResultDictionary();

            if (certificate.Id > 0)
            {
                if (certificate.CertificateBody == null)
                {
                    // Header update only
                    repository.Update(certificate);
                }
                else
                {
                    unitOfWork.GetRepository<CertificateHeader>().Update(certificate);
                    unitOfWork.GetRepository<CertificateBody>().Update(certificate.CertificateBody);

                    unitOfWork.SaveChanges();
                }
            }
            else
            {
                repository.Add(certificate);
            }

            return serviceResult;
        }

        public bool CertificateExists(string certificateNumber, int instrumentId, int certificateId, int organizationId)
        {           
            return repository.Find(
                c =>
                c.Instrument.PlantArea.Site.OrganizationId == organizationId
                && c.CertificateNumber.ToLower().Trim() == certificateNumber.ToLower().Trim() && c.Id != certificateId) != null;
        }
    }
}
