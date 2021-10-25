namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System.Data.Entity.Migrations;
    
    public partial class Instruments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalibrationFrequencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DurationPosition = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InstrumentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EquipmentInstrumentLink",
                c => new
                    {
                        EquipmentId = c.Int(nullable: false),
                        InstrumentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EquipmentId, t.InstrumentId })
                .ForeignKey("dbo.Equipments", t => t.EquipmentId, cascadeDelete: true)
                .ForeignKey("dbo.Instruments", t => t.InstrumentId, cascadeDelete: true)
                .Index(t => t.EquipmentId)
                .Index(t => t.InstrumentId);
            
            AddColumn("dbo.Instruments", "PlantAreaId", c => c.Int(nullable: false));
            AddColumn("dbo.Instruments", "Description", c => c.String());
            AddColumn("dbo.Instruments", "Notes", c => c.String());
            AddColumn("dbo.Instruments", "TypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Instruments", "CalibrationFrequencyId", c => c.Int(nullable: false));
            AddColumn("dbo.Instruments", "IsSAT", c => c.Boolean(nullable: false));
            AddColumn("dbo.Instruments", "ChannelCount", c => c.Int(nullable: false));
            AddColumn("dbo.Instruments", "Make", c => c.String());
            AddColumn("dbo.Instruments", "Model", c => c.String());
            AddColumn("dbo.Instruments", "SerialNumber", c => c.String());
            AddColumn("dbo.Instruments", "IsAMS2750", c => c.Boolean(nullable: false));
            AddColumn("dbo.Instruments", "IsRemoved", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Instruments", "PlantAreaId");
            CreateIndex("dbo.Instruments", "TypeId");
            CreateIndex("dbo.Instruments", "CalibrationFrequencyId");
            AddForeignKey("dbo.Instruments", "CalibrationFrequencyId", "dbo.CalibrationFrequencies", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Instruments", "TypeId", "dbo.InstrumentTypes", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Instruments", "PlantAreaId", "dbo.PlantAreas", "Id", cascadeDelete: false);
            DropColumn("dbo.Instruments", "PostalCode");
            CreateStoredProcedure(
                "dbo.EquipmentInstrument_Insert",
                p => new
                    {
                        EquipmentId = p.Int(),
                        InstrumentId = p.Int(),
                    },
                body: @"INSERT [dbo].[EquipmentInstrumentLink]([EquipmentId], [InstrumentId]) VALUES (@EquipmentId, @InstrumentId)");

            CreateStoredProcedure(
                "dbo.EquipmentInstrument_Delete",
                p => new
                    {
                        EquipmentId = p.Int(),
                        InstrumentId = p.Int(),
                    },
                body: @"DELETE [dbo].[EquipmentInstrumentLink] WHERE (([EquipmentId] = @EquipmentId) AND ([InstrumentId] = @InstrumentId))"); 

            // Reference Data Migration
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Analyser') INSERT INTO InstrumentTypes(Name) VALUES ('Analyser')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Bench Multimeter') INSERT INTO InstrumentTypes(Name) VALUES ('Bench Multimeter')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Caliper') INSERT INTO InstrumentTypes(Name) VALUES ('Caliper')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Centre Gauge') INSERT INTO InstrumentTypes(Name) VALUES ('Centre Gauge')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Circular Recorder') INSERT INTO InstrumentTypes(Name) VALUES ('Circular Recorder')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Clampmeter') INSERT INTO InstrumentTypes(Name) VALUES ('Clampmeter')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Compound Thickness Gauge') INSERT INTO InstrumentTypes(Name) VALUES ('Compound Thickness Gauge')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Conductivity Meter') INSERT INTO InstrumentTypes(Name) VALUES ('Conductivity Meter')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Controller') INSERT INTO InstrumentTypes(Name) VALUES ('Controller')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Controller/Recorder') INSERT INTO InstrumentTypes(Name) VALUES ('Controller/Recorder')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Cord Thickness Gauge') INSERT INTO InstrumentTypes(Name) VALUES ('Cord Thickness Gauge')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Cord Twist') INSERT INTO InstrumentTypes(Name) VALUES ('Cord Twist')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Decade Box') INSERT INTO InstrumentTypes(Name) VALUES ('Decade Box')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Depth Gauge') INSERT INTO InstrumentTypes(Name) VALUES ('Depth Gauge')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Digital Micrometer') INSERT INTO InstrumentTypes(Name) VALUES ('Digital Micrometer')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Digital Thermometer') INSERT INTO InstrumentTypes(Name) VALUES ('Digital Thermometer')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Feeler Gauge') INSERT INTO InstrumentTypes(Name) VALUES ('Feeler Gauge')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Graphic Recorder') INSERT INTO InstrumentTypes(Name) VALUES ('Graphic Recorder')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Handheld Multimeter') INSERT INTO InstrumentTypes(Name) VALUES ('Handheld Multimeter')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Handheld Thermometer') INSERT INTO InstrumentTypes(Name) VALUES ('Handheld Thermometer')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Hardness') INSERT INTO InstrumentTypes(Name) VALUES ('Hardness')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Height Gauge') INSERT INTO InstrumentTypes(Name) VALUES ('Height Gauge')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Humidity Sensor') INSERT INTO InstrumentTypes(Name) VALUES ('Humidity Sensor')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Humidity Transmitter') INSERT INTO InstrumentTypes(Name) VALUES ('Humidity Transmitter')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'I/P Converter') INSERT INTO InstrumentTypes(Name) VALUES ('I/P Converter')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Indicator') INSERT INTO InstrumentTypes(Name) VALUES ('Indicator')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Infra-red Thermometer') INSERT INTO InstrumentTypes(Name) VALUES ('Infra-red Thermometer')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Insulation Tester') INSERT INTO InstrumentTypes(Name) VALUES ('Insulation Tester')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Lateral Run Out') INSERT INTO InstrumentTypes(Name) VALUES ('Lateral Run Out')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Load Cell') INSERT INTO InstrumentTypes(Name) VALUES ('Load Cell')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Micrometer') INSERT INTO InstrumentTypes(Name) VALUES ('Micrometer')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Millivolt Source') INSERT INTO InstrumentTypes(Name) VALUES ('Millivolt Source')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Oven') INSERT INTO InstrumentTypes(Name) VALUES ('Oven')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Override') INSERT INTO InstrumentTypes(Name) VALUES ('Override')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Overtemp') INSERT INTO InstrumentTypes(Name) VALUES ('Overtemp')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Paper Chart Recorder') INSERT INTO InstrumentTypes(Name) VALUES ('Paper Chart Recorder')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'PI Tape') INSERT INTO InstrumentTypes(Name) VALUES ('PI Tape')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'PLC') INSERT INTO InstrumentTypes(Name) VALUES ('PLC')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Portable Field Test Calibrator') INSERT INTO InstrumentTypes(Name) VALUES ('Portable Field Test Calibrator')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Power Meter') INSERT INTO InstrumentTypes(Name) VALUES ('Power Meter')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Pressure Gauge') INSERT INTO InstrumentTypes(Name) VALUES ('Pressure Gauge')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Pressure Switch') INSERT INTO InstrumentTypes(Name) VALUES ('Pressure Switch')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Pressure Transducer') INSERT INTO InstrumentTypes(Name) VALUES ('Pressure Transducer')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Programmer/Controller') INSERT INTO InstrumentTypes(Name) VALUES ('Programmer/Controller')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Recorder') INSERT INTO InstrumentTypes(Name) VALUES ('Recorder')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'SAT Instrument') INSERT INTO InstrumentTypes(Name) VALUES ('SAT Instrument')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Scales') INSERT INTO InstrumentTypes(Name) VALUES ('Scales')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Survey Recorder') INSERT INTO InstrumentTypes(Name) VALUES ('Survey Recorder')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Temperature Logger') INSERT INTO InstrumentTypes(Name) VALUES ('Temperature Logger')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Temperature Transmitter') INSERT INTO InstrumentTypes(Name) VALUES ('Temperature Transmitter')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Thickness Gauge') INSERT INTO InstrumentTypes(Name) VALUES ('Thickness Gauge')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Timer') INSERT INTO InstrumentTypes(Name) VALUES ('Timer')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Tooth Depth Gauge') INSERT INTO InstrumentTypes(Name) VALUES ('Tooth Depth Gauge')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Torque') INSERT INTO InstrumentTypes(Name) VALUES ('Torque')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Vacuum Gauge') INSERT INTO InstrumentTypes(Name) VALUES ('Vacuum Gauge')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Vacuum Transmitter') INSERT INTO InstrumentTypes(Name) VALUES ('Vacuum Transmitter')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Vernier Caliper') INSERT INTO InstrumentTypes(Name) VALUES ('Vernier Caliper')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Vernier Height') INSERT INTO InstrumentTypes(Name) VALUES ('Vernier Height')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM InstrumentTypes WHERE Name = 'Weights') INSERT INTO InstrumentTypes(Name) VALUES ('Weights')");

            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM CalibrationFrequencies WHERE Name = 'None') INSERT INTO CalibrationFrequencies(Name, DurationPosition) VALUES ('None', 0)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM CalibrationFrequencies WHERE Name = '4-Weekly') INSERT INTO CalibrationFrequencies(Name, DurationPosition) VALUES ('4-Weekly', 1)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM CalibrationFrequencies WHERE Name = 'Monthly') INSERT INTO CalibrationFrequencies(Name, DurationPosition) VALUES ('Monthly', 2)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM CalibrationFrequencies WHERE Name = 'Bi-Monthly') INSERT INTO CalibrationFrequencies(Name, DurationPosition) VALUES ('Bi-Monthly', 3)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM CalibrationFrequencies WHERE Name = 'Quarterly') INSERT INTO CalibrationFrequencies(Name, DurationPosition) VALUES ('Quarterly', 4)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM CalibrationFrequencies WHERE Name = '4-Monthly') INSERT INTO CalibrationFrequencies(Name, DurationPosition) VALUES ('4-Monthly', 5)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM CalibrationFrequencies WHERE Name = 'Half-Yearly') INSERT INTO CalibrationFrequencies(Name, DurationPosition) VALUES ('Half-Yearly', 6)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM CalibrationFrequencies WHERE Name = 'Yearly') INSERT INTO CalibrationFrequencies(Name, DurationPosition) VALUES ('Yearly', 7)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM CalibrationFrequencies WHERE Name = 'Bi-Annual') INSERT INTO CalibrationFrequencies(Name, DurationPosition) VALUES ('Bi-Annual', 8)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM CalibrationFrequencies WHERE Name = '3-Yearly') INSERT INTO CalibrationFrequencies(Name, DurationPosition) VALUES ('3-Yearly', 9)");
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.EquipmentInstrument_Delete");
            DropStoredProcedure("dbo.EquipmentInstrument_Insert");
            AddColumn("dbo.Instruments", "PostalCode", c => c.String());
            DropForeignKey("dbo.Instruments", "PlantAreaId", "dbo.PlantAreas");
            DropForeignKey("dbo.EquipmentInstrumentLink", "InstrumentId", "dbo.Instruments");
            DropForeignKey("dbo.EquipmentInstrumentLink", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.Instruments", "TypeId", "dbo.InstrumentTypes");
            DropForeignKey("dbo.Instruments", "CalibrationFrequencyId", "dbo.CalibrationFrequencies");
            DropIndex("dbo.EquipmentInstrumentLink", new[] { "InstrumentId" });
            DropIndex("dbo.EquipmentInstrumentLink", new[] { "EquipmentId" });
            DropIndex("dbo.Instruments", new[] { "CalibrationFrequencyId" });
            DropIndex("dbo.Instruments", new[] { "TypeId" });
            DropIndex("dbo.Instruments", new[] { "PlantAreaId" });
            DropColumn("dbo.Instruments", "IsRemoved");
            DropColumn("dbo.Instruments", "IsAMS2750");
            DropColumn("dbo.Instruments", "SerialNumber");
            DropColumn("dbo.Instruments", "Model");
            DropColumn("dbo.Instruments", "Make");
            DropColumn("dbo.Instruments", "ChannelCount");
            DropColumn("dbo.Instruments", "IsSAT");
            DropColumn("dbo.Instruments", "CalibrationFrequencyId");
            DropColumn("dbo.Instruments", "TypeId");
            DropColumn("dbo.Instruments", "Notes");
            DropColumn("dbo.Instruments", "Description");
            DropColumn("dbo.Instruments", "PlantAreaId");
            DropTable("dbo.EquipmentInstrumentLink");
            DropTable("dbo.InstrumentTypes");
            DropTable("dbo.CalibrationFrequencies");
        }
    }
}
