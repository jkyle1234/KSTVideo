namespace KSTVideo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeVideoImage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Video", "VideoImage_ID", "dbo.VideoImage");
            DropIndex("dbo.Video", new[] { "VideoImage_ID" });
            DropColumn("dbo.Video", "VideoImage_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Video", "VideoImage_ID", c => c.Int());
            CreateIndex("dbo.Video", "VideoImage_ID");
            AddForeignKey("dbo.Video", "VideoImage_ID", "dbo.VideoImage", "ID");
        }
    }
}
