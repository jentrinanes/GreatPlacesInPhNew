namespace GreatPlacesInPh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameReviewTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "PlaceId", "dbo.Places");
            DropIndex("dbo.Reviews", new[] { "PlaceId" });
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Message = c.String(),
                        UserId = c.String(),
                        PlaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId);
            
            AddColumn("dbo.Places", "Review", c => c.String());
            DropTable("dbo.Reviews");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Comment = c.String(),
                        UserId = c.String(),
                        PlaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Comments", "PlaceId", "dbo.Places");
            DropIndex("dbo.Comments", new[] { "PlaceId" });
            DropColumn("dbo.Places", "Review");
            DropTable("dbo.Comments");
            CreateIndex("dbo.Reviews", "PlaceId");
            AddForeignKey("dbo.Reviews", "PlaceId", "dbo.Places", "Id", cascadeDelete: true);
        }
    }
}
