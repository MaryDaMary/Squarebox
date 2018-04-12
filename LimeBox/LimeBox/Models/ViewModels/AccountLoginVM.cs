using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models.ViewModels
{
    public class AccountLoginVM
    {
        public bool IsLoggedIn { get; set; }

        
        [Required(ErrorMessage = "Fyll i användarnamn")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Fyll i lösenord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }


    }
}
