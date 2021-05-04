namespace KSTVideo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videoquantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Video", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Video", "Quantity");
        }
    }
}
