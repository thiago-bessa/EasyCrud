﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace EasyCrud.Model.Info
{
    public class RepositoryInfo
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public int Order { get; set; }
        public Type Type { get; set; }
        public PropertyInfo IdColumn { get; set; }
        public List<ColumnInfo> Columns { get; set; }
    }
}
