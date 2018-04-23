using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Attributes;
using EasyCrud.Workflow;
using NUnit.Framework;

namespace EasyCrud.Tests
{
    public class AttributesTests
    {
        [Test]
        public void TemporaryTests()
        {
            var pageWorkflow = new PageWorkflow();
            var pageViewData = pageWorkflow.GetPageViewData(typeof(TestClass));
            var page2 = pageWorkflow.GetPageViewData(typeof(SubClass));
        }
    }

    public class TestClass
    {
        [Text(Name = "Name")]
        public string Name { get; set; }

        [DateTime(Name = "BirthDate")]
        public DateTime BirthDate { get; set; }

        [ListComponent]
        public virtual ICollection<SubClass> SubClasses { get; set; }
    }

    public class SubClass
    {
        [Text(Name = "Name")]
        public string Name { get; set; }
    }
}
