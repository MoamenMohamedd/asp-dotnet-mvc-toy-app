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

namespace AspDotnetMvcToyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ToyAppContext _context;

        public HomeController(ToyAppContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Employees = await _context.Employees.ToListAsync(),
                FocusedEmployee = null

            };

            ViewData["FormAction"] = "Register";

            return View(homeViewModel);
        }

        public async Task<Array> Skills()
        {
            var query = from skill in _context.Skills
                        select new { skill.Id, skill.Name };

            return await query.ToArrayAsync();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.Include(e => e.EmployeeSkills).SingleAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            var skillsIds = employee.EmployeeSkills.Select(e => e.SkillId);
            employee.Skills = String.Join(',', skillsIds);

            var homeViewModel = new HomeViewModel
            {
                Employees = await _context.Employees.ToListAsync(),
                FocusedEmployee = employee
            };

            ViewData["FormAction"] = "Edit";

            return View("Index", homeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,FullName,Email,Skills", Prefix = "FocusedEmployee")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var homeViewModel = new HomeViewModel
            {
                Employees = await _context.Employees.ToListAsync(),
                FocusedEmployee = employee
            };

            ViewData["FormAction"] = "Register";

            return View("Index", homeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email", Prefix = "FocusedEmployee")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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

            var homeViewModel = new HomeViewModel
            {
                Employees = await _context.Employees.ToListAsync(),
                FocusedEmployee = employee
            };

            ViewData["FormAction"] = "Edit";

            return View("Index", homeViewModel);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
