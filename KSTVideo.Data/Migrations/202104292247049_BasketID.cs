namespace KSTVideo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasketID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasketLine", "BasketID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BasketLine", "BasketID");
        }
    }
}
