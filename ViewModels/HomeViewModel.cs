using AspDotnetMvcToyApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotnetMvcToyApp.Models
{
    public class HomeViewModel
    {
        public EmployeeFormViewModel EmployeeFormViewModel { get; set; }

        public EmployeeTableViewModel EmployeeTableViewModel { get; set; }
    }
}
