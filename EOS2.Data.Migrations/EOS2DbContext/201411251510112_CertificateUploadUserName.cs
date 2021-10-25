namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System.Data.Entity.Migrations;
    
    public partial class CertificateUploadUserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CertificateDetails", "FileUploadUserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CertificateDetails", "FileUploadUserName");
        }
    }
}
