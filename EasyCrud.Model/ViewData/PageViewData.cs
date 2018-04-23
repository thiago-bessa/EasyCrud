﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.ViewData
{
    public class PageViewData
    {
        public ComponentViewData MainComponent { get; set; }
        public List<ComponentViewData> Components { get; set; }
    }
}
