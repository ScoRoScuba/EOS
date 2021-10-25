namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganisationUserRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrganizationRoleUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganizationRoles", t => t.RoleId)
                .Index(t => t.RoleId, name: "IX_Role_Id");            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrganizationRoleUsers", "RoleId", "dbo.OrganizationRoles");
            DropIndex("dbo.OrganizationRoleUsers", "IX_Role_Id");
            DropTable("dbo.OrganizationRoleUsers");
        }
    }
}
