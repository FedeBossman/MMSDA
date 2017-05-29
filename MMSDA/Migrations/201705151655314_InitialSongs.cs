namespace MMSDA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSongs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        SongID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Lyrics = c.String(),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SongID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Songs");
        }
    }
}
