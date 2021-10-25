namespace EOS2.Data.Migrations.SecurityContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewUserFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "FamilyName", c => c.String());
            AddColumn("dbo.AspNetUsers", "MiddleName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "MiddleName");
            DropColumn("dbo.AspNetUsers", "FamilyName");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
