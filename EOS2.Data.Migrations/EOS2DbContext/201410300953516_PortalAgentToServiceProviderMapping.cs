namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PortalAgentToServiceProviderMapping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrganizationRoles", "ParentOrganizationId", c => c.Int());
            CreateIndex("dbo.OrganizationRoles", "ParentOrganizationId");
            AddForeignKey("dbo.OrganizationRoles", "ParentOrganizationId", "dbo.OrganizationRoles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrganizationRoles", "ParentOrganizationId", "dbo.OrganizationRoles");
            DropIndex("dbo.OrganizationRoles", new[] { "ParentOrganizationId" });
            DropColumn("dbo.OrganizationRoles", "ParentOrganizationId");
        }
    }
}
