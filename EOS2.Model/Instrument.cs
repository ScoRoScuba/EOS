namespace EOS2.Model
{
    using System.Collections.Generic;

    public class Instrument : INamedEntity
    {
        public int Id { get; set; }

        public int PlantAreaId { get; set; }

        public virtual PlantArea PlantArea { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public string Notes { get; set; }

        public int TypeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "There is no real confusion between InstrumentType & object.GetType()")]
        public virtual InstrumentType Type { get; set; }

        public int CalibrationFrequencyId { get; set; }

        public virtual CalibrationFrequency CalibrationFrequency { get; set; }

        public bool IsSAT { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string SerialNumber { get; set; }

        // ReSharper disable once InconsistentNaming
        public bool IsAMS2750 { get; set; }

        public bool IsRemoved { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Required for UnitTesting")]
        public virtual ICollection<Channel> Channels { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Required for UnitTesting")]
        public virtual ICollection<CertificateHeader> CertificateHeaders { get; set; }
    }
}
