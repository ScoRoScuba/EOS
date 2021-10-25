namespace EOS2.Model
{
    using System;

    public class CertificateHeader : IEntity
    {
        public int Id { get; set; }

        public string CertificateNumber { get; set; }

        public virtual Instrument Instrument { get; set; }

        public int InstrumentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TypeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "There is no real confusion between Certificate Type & object.GetType()")]
        public virtual CertificateType Type { get; set; }

        public virtual CertificateBody CertificateBody { get; set; }
    }
}
