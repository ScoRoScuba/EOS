namespace EOS2.Model
{
    public class CertificateBody : IEntity
    {
        public int Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "To be investigated")]
        public byte[] File { get; set; }

        public string FileName { get; set; }

        public string FileUploadUserName { get; set; }

        public CertificateHeader CertificateHeader { get; set; }
    }
}
