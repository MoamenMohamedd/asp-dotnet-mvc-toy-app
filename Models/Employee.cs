using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotnetMvcToyApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Full Name")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Name length must be between {2} and {1} characters")]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

    }
}
