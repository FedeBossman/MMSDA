namespace MMSDA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class songalbumrelease : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "AlbumRef", c => c.Int(nullable: false));
            AddColumn("dbo.Songs", "Album_AlbumID", c => c.Int());
            CreateIndex("dbo.Songs", "Album_AlbumID");
            AddForeignKey("dbo.Songs", "Album_AlbumID", "dbo.Albums", "AlbumID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "Album_AlbumID", "dbo.Albums");
            DropIndex("dbo.Songs", new[] { "Album_AlbumID" });
            DropColumn("dbo.Songs", "Album_AlbumID");
            DropColumn("dbo.Songs", "AlbumRef");
        }
    }
}
