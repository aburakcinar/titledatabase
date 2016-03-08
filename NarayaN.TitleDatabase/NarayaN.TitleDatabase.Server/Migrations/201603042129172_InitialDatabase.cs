namespace NarayaN.TitleDatabase.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tmdb_movie",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ImdbId = c.String(unicode: false),
                        Title = c.String(unicode: false),
                        OriginalTitle = c.String(unicode: false),
                        OriginalLanguage = c.String(unicode: false),
                        Status = c.String(unicode: false),
                        Tagline = c.String(unicode: false),
                        Overview = c.String(unicode: false),
                        Homepage = c.String(unicode: false),
                        BackdropPath = c.String(unicode: false),
                        PosterPath = c.String(unicode: false),
                        Adult = c.Boolean(nullable: false),
                        Video = c.Boolean(nullable: false),
                        ReleaseDate = c.DateTime(precision: 0),
                        Revenue = c.Long(nullable: false),
                        Budget = c.Long(nullable: false),
                        Runtime = c.Int(),
                        Popularity = c.Double(nullable: false),
                        VoteAverage = c.Double(nullable: false),
                        VoteCount = c.Int(nullable: false),
                        IslemTarihSaat = c.DateTime(precision: 0),
                        IslemKullaniciId = c.String(maxLength: 11, storeType: "nvarchar"),
                        IslemSessionId = c.String(maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tmdb_movie");
        }
    }
}
