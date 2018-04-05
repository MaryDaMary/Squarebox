using LimeBox.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models.ViewModels
{
    public class ManyBoxesItemVM
    {
        public string BoxTypeName { get; set; }
        public int BoxId { get; set; }
        public string BoxImg { get; set; }
        public int Id { get; set; }
        public decimal Price { get; set; }
    }
}
