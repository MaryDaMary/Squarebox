using LimeBox.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models.ViewModels
{
    public class AdminAddBoxesVM
    {
        [Required(ErrorMessage = " <span style=\"color:red\">*</span>Namn saknas!")]
        [Display(Name = "Namn på box")]
        public string BoxType { get; set; }

        [Required(ErrorMessage = " <span style=\"color:red\">*</span>Pris saknas")]
        [Display(Name = "Pris")]
        [DataType(DataType.Currency, ErrorMessage = " <span style=\"color:red\">*</span>Måste vara en siffra")]
        public decimal? BoxPrice { get; set; }

        [Required(ErrorMessage = " <span style=\"color:red\">*</span>Bild saknas")]
        [Display(Name = "Bild Url")]
        public string BoxImage { get; set; }

        [Required(ErrorMessage = " <span style=\"color:red\">*</span>Bild saknas")]
        [Display(Name = "Header Bild Url")]
        public string BoxImageHeader { get; set; }

        [Required(ErrorMessage = " <span style=\"color:red\">*</span>Beskrivning saknas")]
        [Display(Name = "Beskrivning")]
        public string BoxDescription { get; set; }
    }
}
