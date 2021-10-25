namespace EOS2.Data.Mappings.Logging
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using global::EOS2.Model.Elmah;

    public class ElmahErrorMappings : EntityTypeConfiguration<ElmahError>
    {
        public ElmahErrorMappings()
        {
            ToTable("ELMAH_Error");

            HasKey(m => m.ErrorId);
            Property(m => m.ErrorId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(m => m.Application).HasMaxLength(60).IsRequired();
            Property(m => m.Host).HasMaxLength(50).IsRequired();
            Property(m => m.Type).HasMaxLength(100).IsRequired();
            Property(m => m.Source).HasMaxLength(60).IsRequired();
            Property(m => m.Message).HasMaxLength(500).IsRequired();
            Property(m => m.User).HasMaxLength(50).IsRequired();
            Property(m => m.AllXml).HasColumnType("ntext");
            Property(m => m.StatusCode).IsRequired();
            Property(m => m.TimeUtc).IsRequired();

            Property(m => m.Sequence).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
