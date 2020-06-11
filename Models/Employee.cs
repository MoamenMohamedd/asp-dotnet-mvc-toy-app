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
        [StringLength(60, MinimumLength = 3)]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

    }
}
