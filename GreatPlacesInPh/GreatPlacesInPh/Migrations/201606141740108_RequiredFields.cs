namespace GreatPlacesInPh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Message", c => c.String(nullable: false));
            AlterColumn("dbo.Places", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Places", "ImageUrl", c => c.String(nullable: false));
            AlterColumn("dbo.Places", "Review", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Places", "Review", c => c.String());
            AlterColumn("dbo.Places", "ImageUrl", c => c.String());
            AlterColumn("dbo.Places", "Name", c => c.String());
            AlterColumn("dbo.Comments", "Message", c => c.String());
        }
    }
}
