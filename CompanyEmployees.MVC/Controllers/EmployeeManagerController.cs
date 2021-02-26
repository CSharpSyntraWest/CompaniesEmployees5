using Contracts;
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
    }
}
