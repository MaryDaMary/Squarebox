using System;
using System.Collections.Generic;

namespace LimeBox.Models.Entities
{
    public partial class OrderRows
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BoxId { get; set; }

        public Orders Order { get; set; }
    }
}
