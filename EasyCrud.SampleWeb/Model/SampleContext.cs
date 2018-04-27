using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EasyCrud.Model.Attributes;

namespace EasyCrud.SampleWeb.Model
{
    public class SampleContext : DbContext
    {
        [Repository(Label = "Artistas", Order = 1)]
        public DbSet<Artist> Artists { get; set; }

        [Repository(Label = "Álbums", Order = 2)]
        public DbSet<Album> Albums { get; set; }

        [Repository(Label = "Gêneros", Order = 4)]
        public DbSet<Genre> Genres { get; set; }

        [Repository(Label = "Músicas", Order = 3)]
        public DbSet<Song> Songs { get; set; }
        
    }
}