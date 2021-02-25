using Contracts;
using Microsoft.AspNetCore.Mvc;
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
    }
}
