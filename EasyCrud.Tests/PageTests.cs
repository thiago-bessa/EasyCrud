﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Attributes;
using EasyCrud.Model.Exceptions;
using EasyCrud.Workflow;
using FluentAssertions;
using NUnit.Framework;

namespace EasyCrud.Tests
{

    public class PageTests
    {
        //[Test]
        //public void PageWithMainComponent()
        //{
        //    var pageWorkflow = new PageWorkflow();
        //    var pageViewData = pageWorkflow.GetPageViewData("EasyCrud.Tests", "EasyCrud.Tests.MockContext", "NoChildren");

        //    pageViewData.MainComponent.Should().NotBeNull();
        //    pageViewData.Components.Count.Should().Be(0);
        //}

        //[Test]
        //public void PageWithMainComponentAndOneChild()
        //{
        //    var pageWorkflow = new PageWorkflow();
        //    var pageViewData = pageWorkflow.GetPageViewData("EasyCrud.Tests", "EasyCrud.Tests.MockContext", "SingleChild");

        //    pageViewData.MainComponent.Should().NotBeNull();
        //    pageViewData.Components.Count.Should().Be(1);
        //}

        //[Test]
        //public void PageWithMainComponentAndTwoChildren()
        //{
        //    var pageWorkflow = new PageWorkflow();
        //    var pageViewData = pageWorkflow.GetPageViewData("EasyCrud.Tests", "EasyCrud.Tests.MockContext", "DoubleChildren");

        //    pageViewData.MainComponent.Should().NotBeNull();
        //    pageViewData.Components.Count.Should().Be(2);
        //}

        //[Test]
        //public void ShouldThrowExceptionForWrongPropertyOnDbSet()
        //{
        //    var pageWorkflow = new PageWorkflow();
        //    Action getPageViewData = () => pageWorkflow.GetPageViewData("EasyCrud.Tests", "EasyCrud.Tests.MockContext", "WrongTypes");
        //    getPageViewData.Should().Throw<PropertyConfigurationException>("Only ICollection<> is allowed to have a ComponentAttribute");
        //}
        
        //[Test]
        //public void ShouldThrowExceptionForDbSetNotFound()
        //{
        //    var pageWorkflow = new PageWorkflow();
        //    Action getPageViewData = () => pageWorkflow.GetPageViewData("EasyCrud.Tests", "EasyCrud.Tests.MockContext", "UnknownType");
        //    getPageViewData.Should().Throw<RepositoryNotFoundException>();
        //}
    }

    #region Mocks

    public class MockContext
    {
        public DbSet<MockNoChildClass> NoChildren { get; set; }
        public DbSet<MockSingleChildClass> SingleChild { get; set; }
        public DbSet<MockDoubleChildClass> DoubleChildren { get; set; }
        public DbSet<MockChildWrongType> WrongTypes { get; set; }
    }

    public class MockNoChildClass
    {
        [Text(Label = "Name")]
        public string Name { get; set; }

        [Selection]
        public int Selection { get; set; }
    }

    public class MockSingleChildClass
    {
        [Text(Label = "Name")]
        public string Name { get; set; }

        [DateTime(Label = "BirthDate")]
        public DateTime BirthDate { get; set; }

        [ListComponent]
        public virtual ICollection<MockNoChildClass> Children { get; set; }
    }

    public class MockDoubleChildClass
    {
        [Text(Label = "Name")]
        public string Name { get; set; }

        [Boolean(Label = "Enabled")]
        public bool Enabled { get; set; }

        [Image(Label = "Image")]
        public string Image { get; set; }

        [ListComponent]
        public virtual ICollection<MockNoChildClass> ChildrenNoChild { get; set; }

        [ListComponent]
        public virtual ICollection<MockSingleChildClass> ChildrenSingleChild { get; set; }
    }

    public class MockChildWrongType
    {
        [Text(Label = "Name")]
        public string Name { get; set; }

        [ListComponent]
        public List<MockSingleChildClass> ChildrenSingleChild { get; set; }
    }

    #endregion
}
