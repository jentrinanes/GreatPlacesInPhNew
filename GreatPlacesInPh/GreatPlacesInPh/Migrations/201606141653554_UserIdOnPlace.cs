namespace GreatPlacesInPh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdOnPlace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Places", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Places", "UserId");
        }
    }
}
