using CompanyEmployees.MVC.Controllers;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public void Index_ReturnsAViewResult_WithAListOfEmployees()
        {
            // Arrange
            
            mockRepo.Setup(repo => repo.Employee.GetAllEmployees(false))
                .Returns(SeedTestData.GetTestEmployees());
            var controller = new EmployeeManagerController(mockRepo.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsAssignableFrom<List<Employee>>(
                 viewResult.ViewData.Model);
            var model = viewResult.ViewData.Model as List<Employee>;
            Assert.AreEqual(2, model.Count());
        }


        [Test]
        public void Insert_InsertsEmployeeAndReturnsAViewResult_WithAnEmployee()
        {
            // Arrange

            mockRepo.Setup(repo => repo.Employee.CreateEmployeeForCompany(It.IsAny<Guid>(), It.IsAny<Employee>()))
                .Verifiable();
            var controller = new EmployeeManagerController(mockRepo.Object);
            var newEmployee = SeedTestData.GetTestEmployee();

            // Act
            var result = controller.Insert(newEmployee);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.AreEqual(viewResult.Model, newEmployee);
            mockRepo.Verify();
        }
        [Test]
        public void Delete_DeletesEmployeeAndReturnsRedirectToActionResult()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["Message"] = "Werknemer verwijderd";
            var testDeleteEmployee = SeedTestData.GetTestEmployee();
            mockRepo.Setup(repo => repo.Employee.GetEmployee(testDeleteEmployee.Id, false))
                .Returns(testDeleteEmployee);

            var controller = new EmployeeManagerController(mockRepo.Object) { TempData = tempData};

            // Act
            var result = controller.Delete(testDeleteEmployee.Id);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }
        [Test]
        public void Employee_Details_ReturnsEmployee()
        {
            // arrange
            Guid testEmployeeId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a");
            var testEmployees = SeedTestData.GetTestEmployees();
            var firstEmpl = SeedTestData.GetTestEmployee();
            mockRepo.Setup(x => x.Employee.GetEmployee(testEmployeeId, It.IsAny<bool>())).Returns(firstEmpl);

            var controller = new EmployeeManagerController(mockRepo.Object);

            // act
            var result = controller.Details(testEmployeeId);
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;        
            Assert.That(viewResult.Model, Is.TypeOf<Employee>());
            var employee = viewResult.Model as Employee;
            // assert
            Assert.AreEqual(testEmployeeId, employee.Id);
        }


    }
}