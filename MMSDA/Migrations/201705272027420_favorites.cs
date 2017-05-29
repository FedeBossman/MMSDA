namespace MMSDA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class favorites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserSongs",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Song_SongID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Song_SongID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.Song_SongID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Song_SongID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserSongs", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.ApplicationUserSongs", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserSongs", new[] { "Song_SongID" });
            DropIndex("dbo.ApplicationUserSongs", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserSongs");
        }
    }
}
