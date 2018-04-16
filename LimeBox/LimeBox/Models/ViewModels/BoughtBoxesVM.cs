using LimeBox.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models.ViewModels
{
    public class BoughtBoxesVM
    {
        public Orders Orders { get; set; }
        public List<Boxes> Boxes { get; set; }
    }
}
