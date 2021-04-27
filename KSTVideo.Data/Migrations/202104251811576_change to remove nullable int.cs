namespace KSTVideo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetoremovenullableint : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Video", "GenreID", "dbo.Genre");
            DropIndex("dbo.Video", new[] { "GenreID" });
            AlterColumn("dbo.Video", "GenreID", c => c.Int(nullable: false));
            CreateIndex("dbo.Video", "GenreID");
            AddForeignKey("dbo.Video", "GenreID", "dbo.Genre", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Video", "GenreID", "dbo.Genre");
            DropIndex("dbo.Video", new[] { "GenreID" });
            AlterColumn("dbo.Video", "GenreID", c => c.Int());
            CreateIndex("dbo.Video", "GenreID");
            AddForeignKey("dbo.Video", "GenreID", "dbo.Genre", "ID");
        }
    }
}
