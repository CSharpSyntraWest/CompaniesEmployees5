using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private IRepositoryManager _repositoryManager;
        private IMapper _mapper;
        public CompaniesController(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _repositoryManager.Company.GetAllCompanies(false);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            //var companiesDto = companies.Select(c => new CompanyDto
            //{ 
            //    Id = c.Id,
            //    Name = c.Name,
            //    FullAddress = c.Address + " " + c.Country               
            //});
            return Ok(companiesDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _repositoryManager.Company.GetCompany(id, false);
            if (company == null)
            {
                return NotFound();//404 not found status boodschap (geen gegevens)
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto); //200 OK status boodschap met de gegevens
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(Guid id)
        {
            var company = _repositoryManager.Company.GetCompany(id, trackChanges: false);
            if (company == null)
            {
                //_logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repositoryManager.Company.DeleteCompany(company);
            _repositoryManager.Save();
            return NoContent();
        }
        [HttpPost]
        public IActionResult CreateCompany([FromBody] Company company)
        {
            if (company == null)
            {
                BadRequest("Company object is empty");
            }
            _repositoryManager.Company.CreateCompany(company);
            _repositoryManager.Save();
            return Ok(company);    //200 met data van nieuw gecreëerde company
        }
    }
}