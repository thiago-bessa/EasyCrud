﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.ViewData
{
    public class EntityViewData
    {
        public object Id { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}
