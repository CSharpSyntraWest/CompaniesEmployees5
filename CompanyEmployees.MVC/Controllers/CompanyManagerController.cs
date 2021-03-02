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
    public class CompanyManagerController : Controller
    {

        private IRepositoryManager _repositoryManager;

        public CompanyManagerController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public IActionResult Index()
        {
            var companies = _repositoryManager.Company.GetAllCompanies(false);
            return View(companies);
        }
        // [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Insert(Company model)
        {
            if (ModelState.IsValid)
            {
                _repositoryManager.Company.CreateCompany(model);
                _repositoryManager.Save();
                ViewBag.Message = "Company inserted";
            }
            return View(model);
        }
        public IActionResult Update(Guid id)
        {
            Company model = _repositoryManager.Company.GetCompany(id, false);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Company model)
        {

            if (ModelState.IsValid)
            {
                Company companyToUpdate = _repositoryManager.Company.GetCompany(model.Id, true);
                companyToUpdate.Name = model.Name;
                companyToUpdate.Address = model.Address;
                companyToUpdate.Country = model.Country;

                _repositoryManager.Save();
                ViewBag.Message = "Company updated successfully";
            }
            return View(model);
        }
        private void FillEmployeesForCompany(Guid companyId)
        {
            List<SelectListItem> employees = (from c in _repositoryManager.Employee.GetAllEmployees(false)
                                              where c.CompanyId == companyId
                                              orderby c.Name
                                              select new SelectListItem() { Text = c.Name, Value = c.Id.ToString() }).ToList();
            ViewBag.Employees = employees;
        }

        [ActionName("Delete")]
        public IActionResult ConfirmDelete(Guid id)
        {
            //int aantalEmployees = _repositoryManager.Employee.GetAllEmployees(false).Where(e => e.CompanyId == id).Count();
            //if (aantalEmployees > 0)
            //{
            //    ViewBag.Message = "Cannot remove Company, " + aantalEmployees + " employee(s) work here";
            //    return View(new Company());
            //}
            //else {
                ViewBag.Message = "Warning : You are about to delete this company";
                Company company = _repositoryManager.Company.GetCompany(id, false);
                FillEmployeesForCompany(id);
                return View(company);
           // }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            //int aantalEmployees = _repositoryManager.Employee.GetAllEmployees(false).Where(e => e.CompanyId == id).Count();
            //if (aantalEmployees > 0)
            //{
            //    ViewBag.Message = "Cannot remove Company, " + aantalEmployees + " employee(s) work here";
            //    return View(new Company());
            //}
            //else
            //{
                Company companyToDelete = _repositoryManager.Company.GetCompany(id, false);
                _repositoryManager.Company.DeleteCompany(companyToDelete);
                _repositoryManager.Save();

                TempData["Message"] = "Company deleted successfully";
                return RedirectToAction(nameof(Index));
            //}
        }
    }
}

