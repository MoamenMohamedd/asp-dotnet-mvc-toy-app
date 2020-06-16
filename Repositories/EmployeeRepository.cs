using AspDotnetMvcToyApp.Data;
using AspDotnetMvcToyApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotnetMvcToyApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private ToyAppContext _context;
        private bool _disposed = false;



        public EmployeeRepository(ToyAppContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> GetEmployeeWithSkills(int id)
        {
            return await _context.Employees.Include(e => e.EmployeeSkills).SingleAsync(e => e.Id == id);
        }

        public void CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Remove(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Update(employee);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
