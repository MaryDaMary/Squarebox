using System;
using System.Collections.Generic;

namespace LimeBox.Models.Entities
{
    public partial class BoxType
    {
        public int Id { get; set; }
        public int BoxId { get; set; }
        public int BoxValue { get; set; }
        public bool Bought { get; set; }
        public decimal BoxPrice { get; set; }
        public int BoxTypeId { get; set; }

        public BoxTypes BoxTypes { get; set; }
    }
}
