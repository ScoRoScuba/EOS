namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SitePostCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "PostalCode", c => c.String());
            AlterColumn("dbo.OrganizationRoles", "Comment", c => c.String(maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrganizationRoles", "Comment", c => c.String());
            DropColumn("dbo.Sites", "PostalCode");
        }
    }
}
