namespace KSTVideo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateImageName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Video", "ImageName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Video", "ImageName");
        }
    }
}
