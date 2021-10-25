namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System.Data.Entity.Migrations;
    
    public partial class CertificateTypes : DbMigration
    {
        public override void Up()
        {
            // Reference Data Migration
            this.Sql("SET IDENTITY_INSERT dbo.CertificateTypes ON");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM CertificateTypes WHERE Id = 1) INSERT INTO CertificateTypes(Id, Name, IsInstrumentApplicable, IsEquipmentApplicable) VALUES (1,'Calibration', 1,0)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM CertificateTypes WHERE Id = 2) INSERT INTO CertificateTypes(Id, Name, IsInstrumentApplicable, IsEquipmentApplicable) VALUES (2,'SAT', 0,1)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM CertificateTypes WHERE Id = 3) INSERT INTO CertificateTypes(Id, Name, IsInstrumentApplicable, IsEquipmentApplicable) VALUES (3,'TUS', 0,1)");
            this.Sql("SET IDENTITY_INSERT dbo.CertificateTypes OFF");
        }
        
        public override void Down()
        {
        }
    }
}
