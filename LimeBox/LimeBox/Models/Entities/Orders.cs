using System;
using System.Collections.Generic;

namespace LimeBox.Models.Entities
{
    public partial class Orders
    {
        public Orders()
        {
            OrderRows = new HashSet<OrderRows>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public int PhoneNumber { get; set; }

        public ICollection<OrderRows> OrderRows { get; set; }
    }
}
