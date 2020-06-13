using AspDotnetMvcToyApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotnetMvcToyApp.Data
{
    public class ToyAppContext : DbContext
    {
        public ToyAppContext(DbContextOptions<ToyAppContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "PHP" },
                new Skill { Id = 2, Name = "ASP.NET" },
                new Skill { Id = 3, Name = "iOS" },
                new Skill { Id = 4, Name = "Android" });
        }
    }
}
