using LimeBox.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models.ViewModels
{
    public class AdminAllOrdersVM
    {
        public Orders Order { get; set; }
        public string StatusName { get; set; }
    }
}
