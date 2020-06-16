using AspDotnetMvcToyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotnetMvcToyApp.Repositories
{
    public interface IEmployeeRepository:IDisposable
    {
        // Returns list of employees without skills
        Task<List<Employee>> GetEmployees();

        // Returns an employee without skills
        Task<Employee> GetEmployee(int id);
        
        // Returns an employee with skills
        Task<Employee> GetEmployeeWithSkills(int id);

        // Creates an employee
        void CreateEmployee(Employee employee);

        // Deletes an employee
        void DeleteEmployee(Employee employee);

        // Updates an employee
        void UpdateEmployee(Employee employee);

        Task<int> SaveAsync();
    }
}
