using AspDotnetMvcToyApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotnetMvcToyApp.Models
{
    public class DbSeeder
    {
        public static void Initialize(ToyAppContext context)
        {
            if (!context.Employees.Any())
            {
                var emp1 = new Employee
                {
                    FullName = "Moamen Mohamed",
                    Email = "moamenmohamedalii@gmail.com",
                    EmployeeSkills = new List<EmployeeSkill> {
                        new EmployeeSkill { SkillId = 1 },
                        new EmployeeSkill { SkillId = 2 },
                        new EmployeeSkill { SkillId = 4 }
                    }
                };

                var emp2 = new Employee
                {
                    FullName = "Moamed Essam",
                };

                context.Add(emp1);
                context.Add(emp2);

                context.SaveChanges();
            }
        }
    }
}
