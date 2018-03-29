using System;
using System.Collections.Generic;

namespace LimeBox.Models.Entities
{
    public partial class Users
    {
        public Users()
        {

        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? PostalCode { get; set; }
        public string City { get; set; }
        public string AspNetId { get; set; }
    }
}
