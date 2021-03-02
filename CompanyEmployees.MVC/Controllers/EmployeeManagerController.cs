﻿using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees.MVC.Controllers
{
    public class EmployeeManagerController : Controller
    {
        private IRepositoryManager _repositoryManager;

        public EmployeeManagerController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public IActionResult Index()
        {
            var employees = _repositoryManager.Employee.GetAllEmployees(false);
            return View(employees);
        }
        private void FillCompanies()
        {
            List<SelectListItem> companies = (from c in _repositoryManager.Company.GetAllCompanies(false)
                                              orderby c.Name
                                              select new SelectListItem() { Text = c.Name, Value = c.Id.ToString() }).ToList();
            ViewBag.Companies = companies;
        }
        public IActionResult Insert()
        {
            FillCompanies();
            return View();
        }
        [HttpPost]
        public IActionResult Insert(Employee model)
        {
            if (ModelState.IsValid)
            {
                _repositoryManager.Employee.CreateEmployeeForCompany(model.CompanyId, model);
                _repositoryManager.Save();
                ViewBag.Message = "Employee inserted";
            }
            FillCompanies();
            return View(model);
        }

        public IActionResult Update(Guid id)
        {
            FillCompanies();

            Employee model = _repositoryManager.Employee.GetEmployee(id, false);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Employee model)
        {
            FillCompanies();

            if (ModelState.IsValid)
            {
                Employee employeeToUpdate = _repositoryManager.Employee.GetEmployee(model.Id, true);
                employeeToUpdate.Name = model.Name;
                employeeToUpdate.Position = model.Position;
                employeeToUpdate.Age = model.Age;
                employeeToUpdate.CompanyId = model.CompanyId;
                _repositoryManager.Save();
                ViewBag.Message = "Employee updated successfully";
            }
            return View(model);
        }

        [ActionName("Delete")]
        public IActionResult ConfirmDelete(Guid id)
        {
            Employee model = _repositoryManager.Employee.GetEmployee(id, false);
            Company company = _repositoryManager.Company.GetCompany(model.CompanyId, false);
            ViewBag.Company = company;
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            Employee employeeToDelete = _repositoryManager.Employee.GetEmployee(id, false);
            _repositoryManager.Employee.DeleteEmployee(employeeToDelete);
            _repositoryManager.Save();
            TempData["Message"] = "Employee deleted successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
