using AspDotnetMvcToyApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotnetMvcToyApp.Data
{
    public class ToyAppContext:DbContext
    {
        public ToyAppContext(DbContextOptions<ToyAppContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
