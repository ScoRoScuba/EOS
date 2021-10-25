namespace EOS2.Model
{
    public abstract class ReferenceDataType : IReferenceDataEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }
    }
}
