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
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ToyAppContext(
                serviceProvider.GetRequiredService<DbContextOptions<ToyAppContext>>()))
            {

                if (context.Employees.Any())
                {
                    return;
                }

                context.AddRange(
                    new Employee
                    {
                        FullName = "Moamen Mohamed",
                        Email = "moamenmohamedalii@gmail.com"
                    },

                    new Employee
                    {
                        FullName = "Ali Nour Eldin",
                        Email = "ali@gmail.com",
                    },

                    new Employee
                    {
                        FullName = "Ammar Yasser Ismail",
                    },

                    new Employee
                    {
                        FullName = "Amr Saleh",
                        Email = "amr@gmail.com",
                    },

                    new Employee
                    {
                        FullName = "Moamed Essam"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
