using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Data
{
    public class Entity
    {
        public object Id { get; set; }
        public Dictionary<string, object> Data { get; set; }

        public Entity()
        {
            Data = new Dictionary<string, object>();
        }
    }
}
