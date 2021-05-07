namespace KSTVideo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeQuantity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BasketLine", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BasketLine", "Quantity", c => c.Int(nullable: false));
        }
    }
}
