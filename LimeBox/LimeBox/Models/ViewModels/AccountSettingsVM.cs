using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models.ViewModels
{
    public class AccountSettingsVM
    {
        public string ReturnUrl { get; set; }
        public CreateFormVM CreateForm { get; set; }

        [Bind(Prefix = nameof(AccountSettingsVM.CreateForm))]
        public class CreateFormVM
        {
            [Display(Name = "Användarnamn")]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i användarnamn")]
            public string Username { get; set; }

            [Display(Name = "Gammalt Lösenord")]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,255}$", ErrorMessage = "<span style=\"color:red\">*</span>Otillåtet lösenord")]
            public string OldPassword { get; set; }

            [Display(Name = "Nytt Lösenord")]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,255}$", ErrorMessage = "<span style=\"color:red\">*</span>Otillåtet lösenord")]
            public string NewPassword { get; set; }

            [Display(Name = "Förnamn")]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i förnamn")]
            public string FirstName { get; set; }

            [Display(Name = "Efternamn")]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i efternamn")]
            public string LastName { get; set; }

            [Display(Name = "Adress")]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i adress")]
            public string Address { get; set; }

            [Display(Name = "Postnummer")]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i postnummer")]
            public int PostalCode { get; set; }

            [Display(Name = "Ort")]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i ort")]
            public string City { get; set; }

            [Display(Name = "E-post")]
            [EmailAddress]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i mejladressen")]
            public string Email { get; set; }

            [Display(Name = "Telefonnummer")]
            [Phone]
            [Required(ErrorMessage = " <span style=\"color:red\">*</span>Fyll i telefonnummer")]
            public string PhoneNumber { get; set; }

        }
    }
}
