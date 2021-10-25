namespace EOS2.Services.Tests
{
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Services.BusinessDomain;

    using Moq;

    using NUnit.Framework;

    public abstract class CertificateServiceTestsBase
    {
        protected Mock<IUnitOfWork> MockUnitOfWork { get; set; }

        protected Mock<IRepository<CertificateHeader>> MockCertificateHeaderRepository { get; set; }

        protected Mock<IRepository<CertificateBody>> MockCertificateBodyRepository { get; set; }

        protected Mock<IRepository<CertificateType>> MockCertificateTypeRepository { get; set; }

        [SetUp]
        public void FixtureSetup()
        {
            MockCertificateHeaderRepository = new Mock<IRepository<CertificateHeader>>();
            MockCertificateHeaderRepository = new Mock<IRepository<CertificateHeader>>();
            MockCertificateBodyRepository = new Mock<IRepository<CertificateBody>>();
            MockCertificateTypeRepository = new Mock<IRepository<CertificateType>>();

            MockUnitOfWork = new Mock<IUnitOfWork>();
        }

        public ICertificateService ServiceUnderTest()
        {
            return new CertificateService(MockUnitOfWork.Object, MockCertificateHeaderRepository.Object);
        }
    }
}
