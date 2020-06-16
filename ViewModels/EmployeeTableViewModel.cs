using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotnetMvcToyApp.ViewModels
{
    public class EmployeeTableViewModel
    {
        public List<EmployeeTableRowViewModel> Employees { get; set; }
    }

    public class EmployeeTableRowViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
    }
}
