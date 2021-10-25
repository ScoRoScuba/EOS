namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CertificateUpload : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CertificateDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        File = c.Binary(),
                        Filename = c.String(),
                        CertificateNumber = c.String(),
                        InstrumentId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instruments", t => t.InstrumentId, cascadeDelete: false)
                .ForeignKey("dbo.CertificateTypes", t => t.TypeId, cascadeDelete: false)
                .Index(t => t.InstrumentId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.CertificateTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsInstrumentApplicable = c.Boolean(nullable: false),
                        IsEquipmentApplicable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);          
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CertificateDetails", "TypeId", "dbo.CertificateTypes");
            DropForeignKey("dbo.CertificateDetails", "InstrumentId", "dbo.Instruments");
            DropIndex("dbo.CertificateDetails", new[] { "TypeId" });
            DropIndex("dbo.CertificateDetails", new[] { "InstrumentId" });
            DropTable("dbo.CertificateTypes");
            DropTable("dbo.CertificateDetails");
        }
    }
}
