namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System.Data.Entity.Migrations;
    
    public partial class Equipments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipmentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Equipments", "PlantAreaId", c => c.Int(nullable: false));
            AddColumn("dbo.Equipments", "Make", c => c.String());
            AddColumn("dbo.Equipments", "Model", c => c.String());
            AddColumn("dbo.Equipments", "SerialNumber", c => c.String());
            AddColumn("dbo.Equipments", "Notes", c => c.String());
            AddColumn("dbo.Equipments", "Description", c => c.String());
            AddColumn("dbo.Equipments", "TypeId", c => c.Int());
            CreateIndex("dbo.Equipments", "TypeId", name: "IX_Type_Id");
            AddForeignKey("dbo.Equipments", "TypeId", "dbo.EquipmentTypes", "Id");

            // Migrate Reference Data
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Autoclave') INSERT INTO EquipmentTypes (Name) VALUES ('Autoclave')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Conveyor Oven') INSERT INTO EquipmentTypes (Name) VALUES ('Conveyor Oven')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Drying Oven') INSERT INTO EquipmentTypes (Name) VALUES ('Drying Oven')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Fluidised Bed') INSERT INTO EquipmentTypes (Name) VALUES ('Fluidised Bed')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Freezer') INSERT INTO EquipmentTypes (Name) VALUES ('Freezer')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Furnace') INSERT INTO EquipmentTypes (Name) VALUES ('Furnace')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Gas Generator') INSERT INTO EquipmentTypes (Name) VALUES ('Gas Generator')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'HIP Furnace') INSERT INTO EquipmentTypes (Name) VALUES ('HIP Furnace')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'NDT Oven') INSERT INTO EquipmentTypes (Name) VALUES ('NDT Oven')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Other') INSERT INTO EquipmentTypes (Name) VALUES ('Other')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Oven') INSERT INTO EquipmentTypes (Name) VALUES ('Oven')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Paint Oven') INSERT INTO EquipmentTypes (Name) VALUES ('Paint Oven')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Pit Furnace') INSERT INTO EquipmentTypes (Name) VALUES ('Pit Furnace')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Portable') INSERT INTO EquipmentTypes (Name) VALUES ('Portable')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Press') INSERT INTO EquipmentTypes (Name) VALUES ('Press')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Quench Bath') INSERT INTO EquipmentTypes (Name) VALUES ('Quench Bath')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Rotary Hearth') INSERT INTO EquipmentTypes (Name) VALUES ('Rotary Hearth')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Salt Bath') INSERT INTO EquipmentTypes (Name) VALUES ('Salt Bath')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Test Equipment') INSERT INTO EquipmentTypes (Name) VALUES ('Test Equipment')");
            this.Sql("IF NOT EXISTS (SELECT TOP 1 1 FROM EquipmentTypes WHERE Name = 'Vacuum Furnace') INSERT INTO EquipmentTypes (Name) VALUES ('Vacuum Furnace')"); 
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipments", "TypeId", "dbo.EquipmentTypes");
            DropIndex("dbo.Equipments", "IX_Type_Id");
            DropColumn("dbo.Equipments", "TypeId");
            DropColumn("dbo.Equipments", "Description");
            DropColumn("dbo.Equipments", "Notes");
            DropColumn("dbo.Equipments", "SerialNumber");
            DropColumn("dbo.Equipments", "Model");
            DropColumn("dbo.Equipments", "Make");
            DropColumn("dbo.Equipments", "PlantAreaId");
            DropTable("dbo.EquipmentTypes");
        }
    }
}
