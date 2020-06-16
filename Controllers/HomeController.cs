using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspDotnetMvcToyApp.Models;
using AspDotnetMvcToyApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AspDotnetMvcToyApp.Repositories;
using AspDotnetMvcToyApp.ViewModels;

namespace AspDotnetMvcToyApp.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger;
        private IEmployeeRepository _employeeRepository;
        private ISkillRepository _skillRepository;

        public HomeController(IEmployeeRepository employeeRepository, ISkillRepository skillRepository, ILogger<HomeController> logger)
        {
            _employeeRepository = employeeRepository;
            _skillRepository = skillRepository;
            _logger = logger;
        }

        // Get /home
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepository.GetEmployees();
            var homeViewModel = MapToHomeViewModel(employees);

            ViewData["FormAction"] = "Register";

            return View(homeViewModel);
        }

        // Get /home/skills
        public async Task<List<Skill>> Skills()
        {
            return await _skillRepository.GetSkills();
        }

        // Get /home/edit/id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetEmployeeWithSkills((int)id);
            if (employee == null)
            {
                return NotFound();
            }

            var employees = await _employeeRepository.GetEmployees();

            var homeViewModel = MapToHomeViewModel(employees, employee);

            return View("Index", homeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,FullName,Email,SkillsString")] EmployeeFormViewModel employeeFormViewModel)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = new Employee { FullName = employeeFormViewModel.FullName, Email = employeeFormViewModel.Email };

                List<int> skillsArray = employeeFormViewModel.getSkillsList();
                if (skillsArray.Count > 0)
                {
                    newEmployee.EmployeeSkills = new List<EmployeeSkill>();

                    foreach (int skillId in skillsArray)
                    {
                        newEmployee.EmployeeSkills.Add(new EmployeeSkill { SkillId = skillId, EmployeeId = newEmployee.Id });
                    }
                }

                _employeeRepository.CreateEmployee(newEmployee);
                await _employeeRepository.SaveAsync();

                return RedirectToAction(nameof(Index));
            }

            var employees = await _employeeRepository.GetEmployees();
            var homeViewModel = MapToHomeViewModel(employees);
            homeViewModel.EmployeeFormViewModel = employeeFormViewModel;

            return View("Index", homeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,SkillsString")] EmployeeFormViewModel employeeFormViewModel)
        {
            if (id != employeeFormViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Employee employee = null;
                try
                {
                    employee = await _employeeRepository.GetEmployeeWithSkills(id);
                    // Update Email and FullName
                    employee.FullName = employeeFormViewModel.FullName;
                    employee.Email = employeeFormViewModel.Email;

                    // Find out changes in skills
                    List<int> currentSkills = employee.EmployeeSkills.Select(v => v.SkillId).ToList();
                    List<int> incomingSkills = employeeFormViewModel.getSkillsList();

                    List<int> toAdd = incomingSkills.Except(currentSkills).ToList();
                    List<int> toRemove = currentSkills.Except(incomingSkills).ToList();

                    employee.EmployeeSkills.RemoveAll(es => toRemove.Contains(es.SkillId));

                    foreach (int skillId in toAdd)
                        employee.EmployeeSkills.Add(new EmployeeSkill { EmployeeId = id, SkillId = skillId });

                    _employeeRepository.UpdateEmployee(employee);
                    await _employeeRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (employee == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var employees = await _employeeRepository.GetEmployees();
            var homeViewModel = MapToHomeViewModel(employees);
            homeViewModel.EmployeeFormViewModel = employeeFormViewModel;

            return View("Index", homeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeRepository.GetEmployee(id);
            if (employee == null)
            {
                return NotFound();
            }

            _employeeRepository.DeleteEmployee(employee);
            await  _employeeRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }


        private HomeViewModel MapToHomeViewModel(List<Employee> employees, Employee focusedEmployee = null)
        {
            List<EmployeeTableRowViewModel> employeeTableRowViewModels = new List<EmployeeTableRowViewModel>();

            foreach (Employee emp in employees)
            {
                employeeTableRowViewModels.Add(new EmployeeTableRowViewModel { Id = emp.Id, FullName = emp.FullName, Email = emp.Email });
            }

            EmployeeTableViewModel employeeTableViewModel = new EmployeeTableViewModel
            {
                Employees = employeeTableRowViewModels
            };

            EmployeeFormViewModel employeeFormViewModel = null;
            if (focusedEmployee != null)
            {
                employeeFormViewModel = new EmployeeFormViewModel
                {
                    Id = focusedEmployee.Id,
                    FullName = focusedEmployee.FullName,
                    Email = focusedEmployee.Email,
                    SkillsString = string.Join(",", focusedEmployee.EmployeeSkills.Select(es => es.SkillId))
                };
            }


            return new HomeViewModel { EmployeeFormViewModel = employeeFormViewModel, EmployeeTableViewModel = employeeTableViewModel };

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
