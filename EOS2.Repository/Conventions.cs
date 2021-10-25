namespace EOS2.Repository
{
    using System;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DateTime2Convention : Convention
    {
        public DateTime2Convention()
        {
            this.Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2").HasPrecision(3));
        }
    }
 }
