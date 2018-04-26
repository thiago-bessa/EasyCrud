using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EasyCrud.Model.Attributes;

namespace EasyCrud.SampleWeb.Model
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(50)]
        [Text(Label = "Name", Order = 1)]
        public string Name { get; set; }

        [ListComponent]
        public virtual ICollection<Album> Albums { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}