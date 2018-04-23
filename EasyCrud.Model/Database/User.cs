using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Database
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(20), Index(IsUnique = true)]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(20)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(20)]
        public string Salt { get; set; }
    }
}
