namespace KSTVideo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videoImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Video", "ImageID", c => c.Int(nullable: false));
            AddColumn("dbo.Video", "VideoImage_ID", c => c.Int());
            CreateIndex("dbo.Video", "VideoImage_ID");
            AddForeignKey("dbo.Video", "VideoImage_ID", "dbo.VideoImage", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Video", "VideoImage_ID", "dbo.VideoImage");
            DropIndex("dbo.Video", new[] { "VideoImage_ID" });
            DropColumn("dbo.Video", "VideoImage_ID");
            DropColumn("dbo.Video", "ImageID");
        }
    }
}
