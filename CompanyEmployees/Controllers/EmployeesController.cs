﻿using AutoMapper;
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
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IRepositoryManager _repositoryManager;
        private IMapper _mapper;
        public EmployeesController(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _repositoryManager.Employee.GetAllEmployees(false);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            //var employeesDto = employees.Select(c => new EmployeeDto
            //{
            //    Id = c.Id,
            //    Name = c.Name,
            //    Age = c.Age
            //});
            return Ok(employeesDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = _repositoryManager.Employee.GetEmployee(id, trackChanges: false);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                var employeeDto = _mapper.Map<EmployeeDto>(employee);
                return Ok(employeeDto);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            //eerst employee opzoeken aan de hand van id
            var employee = _repositoryManager.Employee.GetEmployee(id, false);
            if (employee == null)
            {
                NotFound(); // status 404 - not found zonder gegevens teruggeven
            }
            _repositoryManager.Employee.DeleteEmployee(employee);
            _repositoryManager.Save();
            return NoContent();
        }
    }
}
