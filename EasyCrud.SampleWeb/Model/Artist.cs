using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EasyCrud.Model.Attributes;
using EasyCrud.Model.Enums;

namespace EasyCrud.SampleWeb.Model
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(50)]
        [Text(Label = "Name", Order = 1)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Text(Label = "About", Order = 2, Type = TextType.Html)]
        public string About { get; set; }

        [ListComponent]
        public virtual ICollection<Album> Albums { get; set; }

        [ListComponent]
        public virtual ICollection<Song> Songs { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}