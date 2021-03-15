using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NUnit.Framework;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.MVC.Test
{
    public class RepositoryManagerTests
    {
        [Test]
        public void GetAllEmployees_ShouldReturnAllEmployeesFromContext()
        {
            using (var factory = new TestRepositoryContextFactory())
            {
                using (var context = factory.CreateContext())
                {
                    var countEmployeesInDb = context.Employees.Count();
                    
                    var repository = new RepositoryManager(context);
                    var emp = repository.Employee.GetAllEmployees(false);
                    Assert.IsNotNull(emp);
                    Assert.AreEqual(countEmployeesInDb, emp.Count());
                }
            }
        }
        [Test]
        public void GetAllCompanies_ShouldReturnAllCompaniesFromContext()
        {
            using (var factory = new TestRepositoryContextFactory())
            {
                using (var context = factory.CreateContext())
                {
                    var countCompaniesInDb = context.Companies.Count();

                    var repository = new RepositoryManager(context);
                    var comp = repository.Company.GetAllCompanies(false);
                    Assert.IsNotNull(comp);
                    Assert.AreEqual(countCompaniesInDb, comp.Count());
                }
            }
        }
        [Test]
        public void GetEmployee_ShouldReturnEmployee()
        {
            //Arrange
            Guid testEmployeeId;
            using (var factory = new TestRepositoryContextFactory())
            {
                using (var context = factory.CreateContext())
                {
                    var testEmployee = context.Employees.FirstOrDefault();
                    testEmployeeId = testEmployee.Id;
                    var repository = new RepositoryManager(context);
                    //Act
                    var empl = repository.Employee.GetEmployee(testEmployeeId,false);
                    Assert.IsNotNull(empl);
                    Assert.AreEqual(testEmployeeId, empl.Id);
                }
            }
        }
        [Test]
        public void CreateEmployeeForExistingCompany_ShouldAddNewEmployeeToContextForCompany()
        {
            using (var factory = new TestRepositoryContextFactory())
            {
                //Arrange
                int count = 0;

                Company testCompany = null;
                Guid testEmployeeId = Guid.NewGuid();
                Employee testEmployee = new Employee()
                {
                    Id = testEmployeeId,
                    Name = "Jos",
                    Description = "Test employee",
                    Age = 45,
                    Gender = GeslachtType.Man,
                    Position = "Developer"
                };
                using (var context = factory.CreateContext())
                {
                    count = context.Employees.Count();
                    testCompany = context.Companies.FirstOrDefault();
                    testEmployee.CompanyId = testCompany.Id;
                    var repository = new RepositoryManager(context);
                    //Act
                    repository.Employee.CreateEmployeeForCompany(testCompany.Id, testEmployee);
                    repository.Save();
                }
                //Assert
                using (var context = factory.CreateContext())
                {
                    Assert.AreEqual(count + 1, context.Employees.Count());
                    var addedEmployee = context.Employees.Find(testEmployeeId);
                    Assert.IsNotNull(addedEmployee);
                    Assert.AreEqual(testEmployeeId, addedEmployee.Id);
                }
            }
        }
        [Test]
        public void CreateCompany_ShouldAddNewCompanyToContext()
        {
            using (var factory = new TestRepositoryContextFactory())
            {
                //Arrange
                int count = 0;
                Guid testCompanyId;
                using (var context = factory.CreateContext())
                {
                    testCompanyId = Guid.NewGuid();
                    Company testCompany = new Company()
                    {
                        Id = testCompanyId,
                        Name = "Test bedrijf",
                        Country = "Test land",
                        Description = "Test beschrijving",
                        Size = CompanySize.Small,
                        LaunchDate = DateTime.Today,
                        Address = "Test adres"
                    };
                    count = context.Companies.Count();
                    var repository = new RepositoryManager(context);
                    //Act
                    repository.Company.CreateCompany(testCompany);
                    repository.Save();
                }
                //Assert
                using (var context = factory.CreateContext())
                {                  
                    Assert.AreEqual(count +1 , context.Companies.Count());
                    var addedCompany = context.Companies.FirstOrDefault(e => e.Id == testCompanyId);
                    Assert.IsNotNull(addedCompany);
                    Assert.AreEqual(testCompanyId, addedCompany.Id);
                }
            }
        }
    }
}
