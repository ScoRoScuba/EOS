namespace EOS2.Data.Migrations.ELMAHLogging
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ErrorTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ELMAH_Error", newName: "ElmahErrors");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ElmahErrors", newName: "ELMAH_Error");
        }
    }
}
