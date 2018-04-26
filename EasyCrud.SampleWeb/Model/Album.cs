using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using EasyCrud.Model.Attributes;
using EasyCrud.Model.Enums;

namespace EasyCrud.SampleWeb.Model
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(50)]
        [Text(Label = "Name", Order = 1)]
        public string Name { get; set; }

        [Required]
        [Selection(Label = "Artist", Order = 2, Type = SelectionType.Modal)]
        public int ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public virtual Artist Artist { get; set; }

        [ListComponent]
        public virtual ICollection<Song> Songs { get; set; }

        [ListComponent]
        public virtual ICollection<Genre> Genres { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}