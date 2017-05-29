namespace MMSDA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlbumForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Songs", "Album_AlbumID", "dbo.Albums");
            DropIndex("dbo.Songs", new[] { "Album_AlbumID" });
            DropColumn("dbo.Songs", "AlbumRef");
            RenameColumn(table: "dbo.Songs", name: "Album_AlbumID", newName: "AlbumRef");
            AlterColumn("dbo.Songs", "AlbumRef", c => c.Int(nullable: false));
            CreateIndex("dbo.Songs", "AlbumRef");
            AddForeignKey("dbo.Songs", "AlbumRef", "dbo.Albums", "AlbumID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "AlbumRef", "dbo.Albums");
            DropIndex("dbo.Songs", new[] { "AlbumRef" });
            AlterColumn("dbo.Songs", "AlbumRef", c => c.Int());
            RenameColumn(table: "dbo.Songs", name: "AlbumRef", newName: "Album_AlbumID");
            AddColumn("dbo.Songs", "AlbumRef", c => c.Int(nullable: false));
            CreateIndex("dbo.Songs", "Album_AlbumID");
            AddForeignKey("dbo.Songs", "Album_AlbumID", "dbo.Albums", "AlbumID");
        }
    }
}
