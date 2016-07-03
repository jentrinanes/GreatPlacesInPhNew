namespace GreatPlacesInPh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlaceFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "Place_Id", "dbo.Places");
            DropIndex("dbo.Reviews", new[] { "Place_Id" });
            RenameColumn(table: "dbo.Reviews", name: "Place_Id", newName: "PlaceId");
            AlterColumn("dbo.Reviews", "PlaceId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Reviews", "PlaceId");
            AddForeignKey("dbo.Reviews", "PlaceId", "dbo.Places", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "PlaceId", "dbo.Places");
            DropIndex("dbo.Reviews", new[] { "PlaceId" });
            AlterColumn("dbo.Reviews", "PlaceId", c => c.Guid());
            RenameColumn(table: "dbo.Reviews", name: "PlaceId", newName: "Place_Id");
            CreateIndex("dbo.Reviews", "Place_Id");
            AddForeignKey("dbo.Reviews", "Place_Id", "dbo.Places", "Id");
        }
    }
}
