namespace MMSDA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelsArtistAlbum : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Genre = c.String(),
                        Year = c.String(),
                        Artist_ArtistID = c.Int(),
                    })
                .PrimaryKey(t => t.AlbumID)
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistID)
                .Index(t => t.Artist_ArtistID);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ArtistID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "Artist_ArtistID", "dbo.Artists");
            DropIndex("dbo.Albums", new[] { "Artist_ArtistID" });
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
        }
    }
}
