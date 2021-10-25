namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FurnaceClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Instruments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PostalCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrganizationRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(),
                        Comment = c.String(),
                        OrganizationId = c.Int(),
                        OrganizationTypeId = c.Int(),
                        OrganizationType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        PostalCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.PlantAreas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sites", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.OrganizationRoles", "OrganizationId", "dbo.Organizations");
            DropIndex("dbo.Sites", new[] { "OrganizationId" });
            DropIndex("dbo.OrganizationRoles", new[] { "OrganizationId" });
            DropTable("dbo.PlantAreas");
            DropTable("dbo.Sites");
            DropTable("dbo.Organizations");
            DropTable("dbo.OrganizationRoles");
            DropTable("dbo.Instruments");
            DropTable("dbo.FurnaceClasses");
            DropTable("dbo.Equipments");
        }
    }
}
