﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotnetMvcToyApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        
        public string Email { get; set; }

        public List<EmployeeSkill> EmployeeSkills { get; set; }

    }
}
