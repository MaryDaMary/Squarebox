using LimeBox.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models.ViewModels
{
    public class ManyBoxesVM
    {
     
        public string BoxDescription { get; set; }
        public string Boxtype { get; set; }
        public string BoxImage { get; set; }
        public string BoxImageHeader { get; set; }

        public ManyBoxesItemVM[] Items { get; set; }

    }
}
