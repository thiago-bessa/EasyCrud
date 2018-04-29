using EasyCrud.SampleWeb.Model;

namespace EasyCrud.SampleWeb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EasyCrud.SampleWeb.Model.SampleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EasyCrud.SampleWeb.Model.SampleContext context)
        {
            var genres = new[]
            {
                new Genre { Name = "Rock" },
                new Genre { Name = "Pop" }
            };

            context.Genres.AddOrUpdate(x => x.Name, genres);
            context.SaveChanges();

            var artists = new[]
            {
                new Artist { Name = "Some Band", About = "This is a rock band"},
                new Artist { Name = "A singer", About = "This is a pop artist"}
            };

            context.Artists.AddOrUpdate(x => x.Name, artists);
            context.SaveChanges();

            var albums = new[]
            {
                new Album { Name = "Rock Album", ArtistId = artists[0].Id},
                new Album { Name = "Pop Album", ArtistId = artists[1].Id},
            };

            context.Albums.AddOrUpdate(x => x.Name, albums);
            context.SaveChanges();

            var songs = new[]
            {
                new Song { Name = "Rock Song 1", AlbumId = albums[0].Id},
                new Song { Name = "Rock Song 2", AlbumId = albums[0].Id},
                new Song { Name = "Rock Song 3", AlbumId = albums[0].Id},
                new Song { Name = "Pop Song 1", AlbumId = albums[1].Id},
                new Song { Name = "Pop Song 1", AlbumId = albums[1].Id},
            };

            context.Songs.AddOrUpdate(x => x.Name, songs);
            context.SaveChanges();
        }
    }
}
