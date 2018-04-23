using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Database
{
    public class EasyCrudContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
