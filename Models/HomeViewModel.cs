using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotnetMvcToyApp.Models
{
    public class HomeViewModel
    {
        public List<Employee> Employees { get; set; }

        public Employee FocusedEmployee { get; set; }
    }
}
