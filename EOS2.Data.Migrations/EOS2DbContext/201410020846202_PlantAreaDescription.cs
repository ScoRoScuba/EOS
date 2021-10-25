namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlantAreaDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlantAreas", "Description", c => c.String());
            CreateIndex("dbo.PlantAreas", "SiteId");
            AddForeignKey("dbo.PlantAreas", "SiteId", "dbo.Sites", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlantAreas", "SiteId", "dbo.Sites");
            DropIndex("dbo.PlantAreas", new[] { "SiteId" });
            DropColumn("dbo.PlantAreas", "Description");
        }
    }
}
