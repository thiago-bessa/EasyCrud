using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using EasyCrud.Model.Attributes;
using EasyCrud.Model.Enums;

namespace EasyCrud.SampleWeb.Model
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(50)]
        [Text(Label = "Name", Order = 1)]
        public string Name { get; set; }

        [Boolean(Label = "Explicit Content", Order = 2)]
        public bool Explicit { get; set; }

        [Required]
        [Selection(Label = "Album", Order = 3)]
        public int AlbumId { get; set; }
        
        [ForeignKey("AlbumId")]
        public virtual Album Album { get; set; }
        

        public override string ToString()
        {
            return Name;
        }
    }
}