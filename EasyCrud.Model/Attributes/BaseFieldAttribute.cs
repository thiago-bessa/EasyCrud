﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class BaseFieldAttribute : BaseAttribute
    {
        public abstract string ViewFile { get; }

        public bool ReadOnly { get; set; }
    }
}
