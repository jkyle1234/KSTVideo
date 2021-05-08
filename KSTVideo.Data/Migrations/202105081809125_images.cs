namespace KSTVideo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class images : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideoImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VideoImage");
        }
    }
}
