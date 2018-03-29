using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models.ViewModels
{
    [Bind(Prefix = nameof(AccountCreateVM.CreateForm))]
    public class AccountCreateVM
    {
        public string ReturnUrl { get; set; }
        public CreateFormVM CreateForm { get; set; }

        [Bind(Prefix = nameof(AccountCreateVM.CreateForm))]
        public class CreateFormVM
        {
            [Display(Name = "Användarnamn")]
            [Required(ErrorMessage = "Fyll i användarnamn")]
            public string UserName { get; set; }

            [Display(Name = "Lösenord")]
            [Required(ErrorMessage = "Fyll i lösenord")]
            public string PassWord { get; set; }

            [Display(Name = "Förnamn")]
            [Required(ErrorMessage = "Fyll i förnamn")]
            public string FirstName { get; set; }

            [Display(Name = "Efternamn")]
            [Required(ErrorMessage = "Fyll i efternamn")]
            public string LastName { get; set; }

            [Display(Name = "Adress")]
            [Required(ErrorMessage = "Fyll i adress")]
            public string Address { get; set; }

            [Display(Name = "Postnummer")]
            [Required(ErrorMessage = "Fyll i postnummer")]
            public int PostalCode { get; set; }

            [Display(Name = "Ort")]
            [Required(ErrorMessage = "Fyll i ort")]
            public string City { get; set; }

            [Display(Name = "E-post")]
            [EmailAddress]
            [Required(ErrorMessage = "Fyll i mejladressen")]
            public string Email { get; set; }

            [Display(Name = "Telefonnummer")]
            [Phone]
            [Required(ErrorMessage = "Fyll i telefonnummer")]
            public string PhoneNumber { get; set; }

        }
    }
}
