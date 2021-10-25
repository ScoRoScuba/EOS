namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Equipments1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipments", "TypeId", "dbo.EquipmentTypes");
            DropIndex("dbo.Equipments", "IX_Type_Id");
            AlterColumn("dbo.Equipments", "TypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Equipments", "PlantAreaId");
            CreateIndex("dbo.Equipments", "TypeId");
            AddForeignKey("dbo.Equipments", "PlantAreaId", "dbo.PlantAreas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Equipments", "TypeId", "dbo.EquipmentTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipments", "TypeId", "dbo.EquipmentTypes");
            DropForeignKey("dbo.Equipments", "PlantAreaId", "dbo.PlantAreas");
            DropIndex("dbo.Equipments", new[] { "TypeId" });
            DropIndex("dbo.Equipments", new[] { "PlantAreaId" });
            AlterColumn("dbo.Equipments", "TypeId", c => c.Int());
            CreateIndex("dbo.Equipments", "TypeId", name: "IX_Type_Id");
            AddForeignKey("dbo.Equipments", "TypeId", "dbo.EquipmentTypes", "Id");
        }
    }
}
