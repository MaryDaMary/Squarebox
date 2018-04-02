﻿using LimeBox.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models.ViewModels
{
    public class ManyBoxesVM
    {
     
        public string Description { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public ManyBoxesItemVM[] Items { get; set; }

    }
}
