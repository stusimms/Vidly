namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenreTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (GenreType) VALUES ('Action')");
            Sql("INSERT INTO Genres (GenreType) VALUES ('Comedy')");
            Sql("INSERT INTO Genres (GenreType) VALUES ('Thriller')");
            Sql("INSERT INTO Genres (GenreType) VALUES ('Crime')");
            Sql("INSERT INTO Genres (GenreType) VALUES ('Romance')");
            Sql("INSERT INTO Genres (GenreType) VALUES ('Sci-Fi')");
        }
        
        public override void Down()
        {
        }
    }
}
