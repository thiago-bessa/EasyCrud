using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Attributes;

namespace EasyCrud.Workflow.Model
{
    public class RepositoryInfo
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public int Order { get; set; }
        public Type Type { get; set; }
    }
}
