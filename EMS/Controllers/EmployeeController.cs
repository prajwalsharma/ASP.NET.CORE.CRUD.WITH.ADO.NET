using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.DataAccessLayer;
using EMS.Models;
using EMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EMS.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeRepository employeeRepository = new EmployeeADO();

        [HttpGet]
        public IActionResult Index()
        {
            var employees = employeeRepository.GetEmployees();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {

            if (!ModelState.IsValid || employee == null)
            {
                return View();
            }

            Result result = employeeRepository.SaveEmployee(employee);
            ViewBag.CreateMessage = result;

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Employee employee = employeeRepository.GetEmployee(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (employee == null)
            {
                return View();
            }

            Result result = employeeRepository.UpdateEmployee(employee);
            ViewBag.EditMessage = result;

            return View(employee);
        }

        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Result result = employeeRepository.DeleteEmployee(id);

            if (result.Success)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
