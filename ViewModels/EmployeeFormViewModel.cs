using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotnetMvcToyApp.ViewModels
{
    public class EmployeeFormViewModel
    {
        //[HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is Required")]
        [Display(Name = "Full Name")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Name length must be between {2} and {1} characters")]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Skills")]
        public string SkillsString { get; set; }

        public List<int> getSkillsList() {
            if (String.IsNullOrEmpty(SkillsString))
                return new List<int> ();
            return SkillsString.Split(",").Select(s => Int32.Parse(s)).ToList();
        }   
    }
}
