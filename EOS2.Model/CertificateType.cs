namespace EOS2.Model
{
    public class CertificateType : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsInstrumentApplicable { get; set; }

        public bool IsEquipmentApplicable { get; set; }
    }
}
