namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Schedules : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FurnaceClassClasses", newName: "ScheduleTypes");
            RenameTable(name: "dbo.SATFrequencies", newName: "ScheduleFrequencies");
            DropForeignKey("dbo.FurnaceClasses", "FurnaceClassClassId", "dbo.FurnaceClassClasses");
            DropForeignKey("dbo.FurnaceClasses", "SATFrequencyId", "dbo.SATFrequencies");
            DropForeignKey("dbo.FurnaceClasses", "TUSFrequencyId", "dbo.TUSFrequencies");
            DropForeignKey("dbo.FurnaceClasses", "EquipmentId", "dbo.Equipments");
            DropIndex("dbo.FurnaceClasses", new[] { "FurnaceClassClassId" });
            DropIndex("dbo.FurnaceClasses", new[] { "EquipmentId" });
            DropIndex("dbo.FurnaceClasses", new[] { "TUSFrequencyId" });
            DropIndex("dbo.FurnaceClasses", new[] { "SATFrequencyId" });
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FurnaceClassId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        EquipmentId = c.Int(nullable: false),
                        Description = c.String(),
                        FrequencyId = c.Int(nullable: false),
                        SpecialConditions = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScheduleFrequencies", t => t.FrequencyId, cascadeDelete: false)
                .ForeignKey("dbo.FurnaceClasses", t => t.FurnaceClassId, cascadeDelete: false)
                .ForeignKey("dbo.ScheduleTypes", t => t.TypeId, cascadeDelete: false)
                .ForeignKey("dbo.Equipments", t => t.EquipmentId, cascadeDelete: false)
                .Index(t => t.FurnaceClassId)
                .Index(t => t.TypeId)
                .Index(t => t.EquipmentId)
                .Index(t => t.FrequencyId);
            
            DropColumn("dbo.FurnaceClasses", "FurnaceClassClassId");
            DropColumn("dbo.FurnaceClasses", "EquipmentId");
            DropColumn("dbo.FurnaceClasses", "Description");
            DropColumn("dbo.FurnaceClasses", "TUSFrequencyId");
            DropColumn("dbo.FurnaceClasses", "TUSSyncDate");
            DropColumn("dbo.FurnaceClasses", "TUSManDays");
            DropColumn("dbo.FurnaceClasses", "TUSSpecialConditions");
            DropColumn("dbo.FurnaceClasses", "SATFrequencyId");
            DropColumn("dbo.FurnaceClasses", "SATSpecialConditions");
            DropTable("dbo.TUSFrequencies");

            // Manual Stuff
            this.Sql("TRUNCATE TABLE [FurnaceClasses]");
            this.Sql("TRUNCATE TABLE [ScheduleTypes]");
            this.Sql("TRUNCATE TABLE [ScheduleFrequencies]");

            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM FurnaceClasses WHERE Name = '-') INSERT INTO FurnaceClasses (Name) VALUES ('-')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM FurnaceClasses WHERE Name = '1') INSERT INTO FurnaceClasses (Name) VALUES ('1')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM FurnaceClasses WHERE Name = '2') INSERT INTO FurnaceClasses (Name) VALUES ('2')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM FurnaceClasses WHERE Name = '3') INSERT INTO FurnaceClasses (Name) VALUES ('3')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM FurnaceClasses WHERE Name = '4') INSERT INTO FurnaceClasses (Name) VALUES ('4')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM FurnaceClasses WHERE Name = '5') INSERT INTO FurnaceClasses (Name) VALUES ('5')");

            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM ScheduleTypes WHERE Name = 'SAT') INSERT INTO ScheduleTypes (Name) VALUES ('SAT')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM ScheduleTypes WHERE Name = 'TUS') INSERT INTO ScheduleTypes (Name) VALUES ('TUS')");

            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM ScheduleFrequencies WHERE Name = 'None') INSERT INTO ScheduleFrequencies (Name, DurationPosition) VALUES ('None', 0)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM ScheduleFrequencies WHERE Name = 'Weekly') INSERT INTO ScheduleFrequencies (Name, DurationPosition) VALUES ('Weekly', 1)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM ScheduleFrequencies WHERE Name = 'Bi-Weekly') INSERT INTO ScheduleFrequencies (Name, DurationPosition) VALUES ('Bi-Weekly', 2)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM ScheduleFrequencies WHERE Name = '4-Weekly') INSERT INTO ScheduleFrequencies (Name, DurationPosition) VALUES ('4-Weekly', 3)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM ScheduleFrequencies WHERE Name = 'Monthly') INSERT INTO ScheduleFrequencies (Name, DurationPosition) VALUES ('Monthly', 4)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM ScheduleFrequencies WHERE Name = 'Bi-Monthly') INSERT INTO ScheduleFrequencies (Name, DurationPosition) VALUES ('Bi-Monthly', 5)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM ScheduleFrequencies WHERE Name = 'Quarterly') INSERT INTO ScheduleFrequencies (Name, DurationPosition) VALUES ('Quarterly', 6)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM ScheduleFrequencies WHERE Name = 'Half-Yearly') INSERT INTO ScheduleFrequencies (Name, DurationPosition) VALUES ('Half-Yearly', 7)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM ScheduleFrequencies WHERE Name = 'Yearly') INSERT INTO ScheduleFrequencies (Name, DurationPosition) VALUES ('Yearly', 8)");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TUSFrequencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DurationPosition = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.FurnaceClasses", "SATSpecialConditions", c => c.String());
            AddColumn("dbo.FurnaceClasses", "SATFrequencyId", c => c.Int(nullable: false));
            AddColumn("dbo.FurnaceClasses", "TUSSpecialConditions", c => c.String());
            AddColumn("dbo.FurnaceClasses", "TUSManDays", c => c.Single(nullable: false));
            AddColumn("dbo.FurnaceClasses", "TUSSyncDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.FurnaceClasses", "TUSFrequencyId", c => c.Int(nullable: false));
            AddColumn("dbo.FurnaceClasses", "Description", c => c.String());
            AddColumn("dbo.FurnaceClasses", "EquipmentId", c => c.Int(nullable: false));
            AddColumn("dbo.FurnaceClasses", "FurnaceClassClassId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Schedules", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.Schedules", "TypeId", "dbo.ScheduleTypes");
            DropForeignKey("dbo.Schedules", "FurnaceClassId", "dbo.FurnaceClasses");
            DropForeignKey("dbo.Schedules", "FrequencyId", "dbo.ScheduleFrequencies");
            DropIndex("dbo.Schedules", new[] { "FrequencyId" });
            DropIndex("dbo.Schedules", new[] { "EquipmentId" });
            DropIndex("dbo.Schedules", new[] { "TypeId" });
            DropIndex("dbo.Schedules", new[] { "FurnaceClassId" });
            DropTable("dbo.Schedules");
            CreateIndex("dbo.FurnaceClasses", "SATFrequencyId");
            CreateIndex("dbo.FurnaceClasses", "TUSFrequencyId");
            CreateIndex("dbo.FurnaceClasses", "EquipmentId");
            CreateIndex("dbo.FurnaceClasses", "FurnaceClassClassId");
            AddForeignKey("dbo.FurnaceClasses", "EquipmentId", "dbo.Equipments", "Id", cascadeDelete: false);
            AddForeignKey("dbo.FurnaceClasses", "TUSFrequencyId", "dbo.TUSFrequencies", "Id", cascadeDelete: false);
            RenameTable(name: "dbo.ScheduleFrequencies", newName: "SATFrequencies");
            RenameTable(name: "dbo.ScheduleTypes", newName: "FurnaceClassClasses");
            AddForeignKey("dbo.FurnaceClasses", "SATFrequencyId", "dbo.SATFrequencies", "Id", cascadeDelete: false);
            AddForeignKey("dbo.FurnaceClasses", "FurnaceClassClassId", "dbo.FurnaceClassClasses", "Id", cascadeDelete: false);
        }
    }
}
