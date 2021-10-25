namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChannelsAndReferenceData : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EquipmentInstrumentLink", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.EquipmentInstrumentLink", "InstrumentId", "dbo.Instruments");
            DropIndex("dbo.EquipmentInstrumentLink", new[] { "EquipmentId" });
            DropIndex("dbo.EquipmentInstrumentLink", new[] { "InstrumentId" });
            CreateTable(
                "dbo.Channels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstrumentId = c.Int(nullable: false),
                        Name = c.String(),
                        Number = c.String(),
                        TypeId = c.Int(),
                        ConnectedToEquipmentId = c.Int(),
                        ScheduleFrequencyId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipments", t => t.ConnectedToEquipmentId)
                .ForeignKey("dbo.Instruments", t => t.InstrumentId, cascadeDelete: true)
                .ForeignKey("dbo.ScheduleFrequencies", t => t.ScheduleFrequencyId)
                .ForeignKey("dbo.ChannelTypes", t => t.TypeId)
                .Index(t => t.InstrumentId)
                .Index(t => t.TypeId)
                .Index(t => t.ConnectedToEquipmentId)
                .Index(t => t.ScheduleFrequencyId);
            
            CreateTable(
                "dbo.ChannelTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CalibrationFrequencies", "Code", c => c.String());
            AddColumn("dbo.ScheduleFrequencies", "Code", c => c.String());
            AddColumn("dbo.FurnaceClasses", "Code", c => c.String());
            AddColumn("dbo.ScheduleTypes", "Code", c => c.String());
            AddColumn("dbo.EquipmentTypes", "Code", c => c.String());
            AddColumn("dbo.InstrumentTypes", "Code", c => c.String());
            AlterColumn("dbo.CertificateDetails", "StartDate", c => c.DateTime(nullable: false, precision: 3, storeType: "datetime2"));
            AlterColumn("dbo.CertificateDetails", "EndDate", c => c.DateTime(nullable: false, precision: 3, storeType: "datetime2"));
            AlterColumn("dbo.OrganizationRoles", "FromDate", c => c.DateTime(nullable: false, precision: 3, storeType: "datetime2"));
            AlterColumn("dbo.OrganizationRoles", "ToDate", c => c.DateTime(precision: 3, storeType: "datetime2"));
            DropColumn("dbo.Instruments", "ChannelCount");
            DropTable("dbo.EquipmentInstrumentLink");
            DropStoredProcedure("dbo.EquipmentInstrument_Insert");
            DropStoredProcedure("dbo.EquipmentInstrument_Delete");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EquipmentInstrumentLink",
                c => new
                    {
                        EquipmentId = c.Int(nullable: false),
                        InstrumentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EquipmentId, t.InstrumentId });
            
            AddColumn("dbo.Instruments", "ChannelCount", c => c.Int(nullable: false));
            DropForeignKey("dbo.Channels", "TypeId", "dbo.ChannelTypes");
            DropForeignKey("dbo.Channels", "ScheduleFrequencyId", "dbo.ScheduleFrequencies");
            DropForeignKey("dbo.Channels", "InstrumentId", "dbo.Instruments");
            DropForeignKey("dbo.Channels", "ConnectedToEquipmentId", "dbo.Equipments");
            DropIndex("dbo.Channels", new[] { "ScheduleFrequencyId" });
            DropIndex("dbo.Channels", new[] { "ConnectedToEquipmentId" });
            DropIndex("dbo.Channels", new[] { "TypeId" });
            DropIndex("dbo.Channels", new[] { "InstrumentId" });
            AlterColumn("dbo.OrganizationRoles", "ToDate", c => c.DateTime());
            AlterColumn("dbo.OrganizationRoles", "FromDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CertificateDetails", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CertificateDetails", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.InstrumentTypes", "Code");
            DropColumn("dbo.EquipmentTypes", "Code");
            DropColumn("dbo.ScheduleTypes", "Code");
            DropColumn("dbo.FurnaceClasses", "Code");
            DropColumn("dbo.ScheduleFrequencies", "Code");
            DropColumn("dbo.CalibrationFrequencies", "Code");
            DropTable("dbo.ChannelTypes");
            DropTable("dbo.Channels");
            CreateIndex("dbo.EquipmentInstrumentLink", "InstrumentId");
            CreateIndex("dbo.EquipmentInstrumentLink", "EquipmentId");
            AddForeignKey("dbo.EquipmentInstrumentLink", "InstrumentId", "dbo.Instruments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EquipmentInstrumentLink", "EquipmentId", "dbo.Equipments", "Id", cascadeDelete: true);
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
