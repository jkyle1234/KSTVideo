namespace KSTVideo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingShoppingCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VideoID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Video", t => t.VideoID, cascadeDelete: true)
                .Index(t => t.VideoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketLine", "VideoID", "dbo.Video");
            DropIndex("dbo.BasketLine", new[] { "VideoID" });
            DropTable("dbo.BasketLine");
        }
    }
}
