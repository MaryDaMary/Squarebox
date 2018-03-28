using System;
using System.Collections.Generic;

namespace LimeBox.Models.Entities
{
    public partial class Boxes
    {
        public int Id { get; set; }
        public int BoxId { get; set; }
        public string BoxType { get; set; }
        public int BoxValue { get; set; }
        public bool Bought { get; set; }
        public decimal BoxPrice { get; set; }
    }
}
