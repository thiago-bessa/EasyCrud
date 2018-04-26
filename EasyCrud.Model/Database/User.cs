using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Attributes;
using EasyCrud.Model.Enums;

namespace EasyCrud.Model.Database
{
    public class User
    {
        [Key]
        [Text(Label = "Id", Order = 1, Type = TextType.Default, ReadOnly = true)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(20), Index(IsUnique = true)]
        [Text(Label = "Username", Order = 2, Type = TextType.Default, ReadOnly = true)]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(20)]
        [Text(Label = "Password", Order = 2, Type = TextType.Password, ReadOnly = true)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(20)]
        public string Salt { get; set; }
    }
}
