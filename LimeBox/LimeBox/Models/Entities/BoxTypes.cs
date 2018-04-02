using System;
using System.Collections.Generic;

namespace LimeBox.Models.Entities
{
    public partial class BoxTypes
    {
        public BoxTypes()
        {
            Boxes = new HashSet<Boxes>();
        }

        public int Id { get; set; }
        public string BoxType { get; set; }
        public string BoxImage { get; set; }
        public string BoxDescription { get; set; }

        public ICollection<Boxes> Boxes { get; set; }
    }
}
