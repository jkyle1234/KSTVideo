namespace KSTVideo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailclient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailClient",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SMTPAddress = c.String(),
                        FromAddress = c.String(),
                        Password = c.String(),
                        Port = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmailClient");
        }
    }
}
