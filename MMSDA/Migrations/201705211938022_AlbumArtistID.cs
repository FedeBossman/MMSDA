namespace MMSDA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlbumArtistID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "ArtistRef", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "ArtistRef");
        }
    }
}
