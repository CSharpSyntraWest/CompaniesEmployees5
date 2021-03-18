using CompanyEmployees.MVC.Controllers;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.MVC.Test
{
    public class EmployeeManagerControllerTests
    {
        private Mock<IRepositoryManager> mockRepo;
        [SetUp]
        public void Initialize()
        {
            mockRepo = new Mock<IRepositoryManager>();
            mockRepo.Setup(repo => repo.Company.GetAllCompanies(false))
                .Returns(SeedTestData.GetTestCompanies());
        }
        [Test]
        public void Index_ReturnsAViewResult_WithListOfEmployees()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Employee.GetAllEmployees(false))
                .Returns(SeedTestData.GetTestEmployees());
            var controller = new EmployeeManagerController(mockRepo.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
            ViewResult viewResult = result as ViewResult; //casting, kan ook via (ViewResult)result
            Assert.IsAssignableFrom<List<Employee>>(viewResult.ViewData.Model);
            List<Employee> model = viewResult.ViewData.Model as List<Employee>;
            Assert.AreEqual(2, model.Count());
        }
    }
}
