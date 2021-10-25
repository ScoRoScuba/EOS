namespace EOS2.Data.Migrations.ELMAHLogging
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ELMAHErrorTableNameChange : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ElmahErrors", newName: "ELMAH_Error");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ELMAH_Error", newName: "ElmahErrors");
        }
    }
}
