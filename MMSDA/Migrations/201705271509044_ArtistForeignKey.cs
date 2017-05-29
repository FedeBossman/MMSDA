namespace MMSDA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArtistForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "Artist_ArtistID", "dbo.Artists");
            DropIndex("dbo.Albums", new[] { "Artist_ArtistID" });
            DropColumn("dbo.Albums", "ArtistRef");
            RenameColumn(table: "dbo.Albums", name: "Artist_ArtistID", newName: "ArtistRef");
            AlterColumn("dbo.Albums", "ArtistRef", c => c.Int(nullable: false));
            CreateIndex("dbo.Albums", "ArtistRef");
            AddForeignKey("dbo.Albums", "ArtistRef", "dbo.Artists", "ArtistID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "ArtistRef", "dbo.Artists");
            DropIndex("dbo.Albums", new[] { "ArtistRef" });
            AlterColumn("dbo.Albums", "ArtistRef", c => c.Int());
            RenameColumn(table: "dbo.Albums", name: "ArtistRef", newName: "Artist_ArtistID");
            AddColumn("dbo.Albums", "ArtistRef", c => c.Int(nullable: false));
            CreateIndex("dbo.Albums", "Artist_ArtistID");
            AddForeignKey("dbo.Albums", "Artist_ArtistID", "dbo.Artists", "ArtistID");
        }
    }
}
