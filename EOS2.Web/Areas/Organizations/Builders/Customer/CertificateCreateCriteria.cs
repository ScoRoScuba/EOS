namespace EOS2.Web.Areas.Organizations.Builders.Customer
{
    using EOS2.Model.Enums;
    using EOS2.Web.Builders;

    public class CertificateCreateCriteria : IBuilderCriteria
    {
        public CertificateCreateCriteria(CertificateType certificateType, int instrumentId)
        {
            CertificateType = certificateType;
            InstrumentId = instrumentId;
        }

        public int InstrumentId { get; private set; }

        public CertificateType CertificateType { get; private set; }
    }
}