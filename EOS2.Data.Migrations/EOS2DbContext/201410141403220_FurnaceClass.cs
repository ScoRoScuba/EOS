namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FurnaceClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FurnaceClassClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SATFrequencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DurationPosition = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TUSFrequencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DurationPosition = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.FurnaceClasses", "FurnaceClassClassId", c => c.Int(nullable: false));
            AddColumn("dbo.FurnaceClasses", "EquipmentId", c => c.Int(nullable: false));
            AddColumn("dbo.FurnaceClasses", "Description", c => c.String());
            AddColumn("dbo.FurnaceClasses", "TUSFrequencyId", c => c.Int(nullable: false));
            AddColumn("dbo.FurnaceClasses", "TUSSyncDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.FurnaceClasses", "TUSManDays", c => c.Single(nullable: false));
            AddColumn("dbo.FurnaceClasses", "TUSSpecialConditions", c => c.String());
            AddColumn("dbo.FurnaceClasses", "SATFrequencyId", c => c.Int(nullable: false));
            AddColumn("dbo.FurnaceClasses", "SATSpecialConditions", c => c.String());
            CreateIndex("dbo.FurnaceClasses", "FurnaceClassClassId");
            CreateIndex("dbo.FurnaceClasses", "EquipmentId");
            CreateIndex("dbo.FurnaceClasses", "TUSFrequencyId");
            CreateIndex("dbo.FurnaceClasses", "SATFrequencyId");
            AddForeignKey("dbo.FurnaceClasses", "FurnaceClassClassId", "dbo.FurnaceClassClasses", "Id", cascadeDelete: false);
            AddForeignKey("dbo.FurnaceClasses", "SATFrequencyId", "dbo.SATFrequencies", "Id", cascadeDelete: false);
            AddForeignKey("dbo.FurnaceClasses", "TUSFrequencyId", "dbo.TUSFrequencies", "Id", cascadeDelete: false);
            AddForeignKey("dbo.FurnaceClasses", "EquipmentId", "dbo.Equipments", "Id", cascadeDelete: false);

            // Populate the Reference Data
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM FurnaceClassClasses WHERE Name = '-') INSERT INTO FurnaceClassClasses (Name) VALUES ('-')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM FurnaceClassClasses WHERE Name = '1') INSERT INTO FurnaceClassClasses (Name) VALUES ('1')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM FurnaceClassClasses WHERE Name = '2') INSERT INTO FurnaceClassClasses (Name) VALUES ('2')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM FurnaceClassClasses WHERE Name = '3') INSERT INTO FurnaceClassClasses (Name) VALUES ('3')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM FurnaceClassClasses WHERE Name = '4') INSERT INTO FurnaceClassClasses (Name) VALUES ('4')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM FurnaceClassClasses WHERE Name = '5') INSERT INTO FurnaceClassClasses (Name) VALUES ('5')");

            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM SATFrequencies WHERE Name = '5') INSERT INTO SATFrequencies (Name, DurationPosition) VALUES ('None', 0)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM SATFrequencies WHERE Name = '5') INSERT INTO SATFrequencies (Name, DurationPosition) VALUES ('Weekly', 1)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM SATFrequencies WHERE Name = '5') INSERT INTO SATFrequencies (Name, DurationPosition) VALUES ('Bi-Weekly', 2)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM SATFrequencies WHERE Name = '5') INSERT INTO SATFrequencies (Name, DurationPosition) VALUES ('4-Weekly', 3)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM SATFrequencies WHERE Name = '5') INSERT INTO SATFrequencies (Name, DurationPosition) VALUES ('Monthly', 4)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM SATFrequencies WHERE Name = '5') INSERT INTO SATFrequencies (Name, DurationPosition) VALUES ('Quarterly', 5)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM SATFrequencies WHERE Name = '5') INSERT INTO SATFrequencies (Name, DurationPosition) VALUES ('Half-Yearly', 6)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM SATFrequencies WHERE Name = '5') INSERT INTO SATFrequencies (Name, DurationPosition) VALUES ('Yearly', 7)");

            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM TUSFrequencies WHERE Name = '5') INSERT INTO TUSFrequencies (Name, DurationPosition) VALUES ('None', 0)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM TUSFrequencies WHERE Name = '5') INSERT INTO TUSFrequencies (Name, DurationPosition) VALUES ('4-Weekly', 1)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM TUSFrequencies WHERE Name = '5') INSERT INTO TUSFrequencies (Name, DurationPosition) VALUES ('Monthly', 2)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM TUSFrequencies WHERE Name = '5') INSERT INTO TUSFrequencies (Name, DurationPosition) VALUES ('Bi-Monthly', 3)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM TUSFrequencies WHERE Name = '5') INSERT INTO TUSFrequencies (Name, DurationPosition) VALUES ('Quarterly', 4)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM TUSFrequencies WHERE Name = '5') INSERT INTO TUSFrequencies (Name, DurationPosition) VALUES ('Half-Yearly', 5)");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM TUSFrequencies WHERE Name = '5') INSERT INTO TUSFrequencies (Name, DurationPosition) VALUES ('Yearly', 6)");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FurnaceClasses", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.FurnaceClasses", "TUSFrequencyId", "dbo.TUSFrequencies");
            DropForeignKey("dbo.FurnaceClasses", "SATFrequencyId", "dbo.SATFrequencies");
            DropForeignKey("dbo.FurnaceClasses", "FurnaceClassClassId", "dbo.FurnaceClassClasses");
            DropIndex("dbo.FurnaceClasses", new[] { "SATFrequencyId" });
            DropIndex("dbo.FurnaceClasses", new[] { "TUSFrequencyId" });
            DropIndex("dbo.FurnaceClasses", new[] { "EquipmentId" });
            DropIndex("dbo.FurnaceClasses", new[] { "FurnaceClassClassId" });
            DropColumn("dbo.FurnaceClasses", "SATSpecialConditions");
            DropColumn("dbo.FurnaceClasses", "SATFrequencyId");
            DropColumn("dbo.FurnaceClasses", "TUSSpecialConditions");
            DropColumn("dbo.FurnaceClasses", "TUSManDays");
            DropColumn("dbo.FurnaceClasses", "TUSSyncDate");
            DropColumn("dbo.FurnaceClasses", "TUSFrequencyId");
            DropColumn("dbo.FurnaceClasses", "Description");
            DropColumn("dbo.FurnaceClasses", "EquipmentId");
            DropColumn("dbo.FurnaceClasses", "FurnaceClassClassId");
            DropTable("dbo.TUSFrequencies");
            DropTable("dbo.SATFrequencies");
            DropTable("dbo.FurnaceClassClasses");
        }
    }
}
