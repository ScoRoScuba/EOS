namespace EOS2.Data.Mappings.Logging
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using global::EOS2.Model.Elmah;

    public class ELMAH_ErrorMappings : EntityTypeConfiguration<ELMAH_Error>
    {
        public ELMAH_ErrorMappings()
        {
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
