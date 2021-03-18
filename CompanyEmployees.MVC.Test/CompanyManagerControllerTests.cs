﻿using CompanyEmployees.MVC.Controllers;
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
    public class CompanyManagerControllerTests
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
        public void Index_ReturnsAViewResult_WithListOfCompanies()
        {
            //Oefening: maak aan het voorbeeld van de EmployeeMangerControllerTest
            //de test methode voor Index van CompanyManagerController
            //oplossing:
            // Arrange

            var controller = new CompanyManagerController(mockRepo.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsAssignableFrom<List<Company>>(
                 viewResult.ViewData.Model);
            var model = viewResult.ViewData.Model as List<Company>;
            Assert.AreEqual(2, model.Count());
            //bv eerste  van de testCompanies testen op Id:
            Assert.IsTrue(model.Where(comp => comp.Id == SeedTestData.GetTestCompany().Id).Any());
        }
        [Test]
        public void Details_ForCompanyId_ReturnsCompany()
        {
            //Arrange
            Company testCompany = SeedTestData.GetTestCompany();
            Guid testCompanyId = testCompany.Id;
            mockRepo.Setup(r => r.Company.GetCompany(testCompanyId, It.IsAny<bool>())).Returns(testCompany);
            CompanyManagerController controller = new CompanyManagerController(mockRepo.Object); //controller is sut(System Under Test)

            //Act
            var result = controller.Details(testCompanyId);
            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<Company>());
            Company company = viewResult.Model as Company; //of Company company = (Company)viewResult.Model;
            Assert.AreEqual(testCompanyId, company.Id);
            //Assert.AreEqual(testCompany.Name, company.Name);
            //Assert.AreEqual(testCompany.LaunchDate, company.LaunchDate);
            //Assert.AreEqual(testCompany.Size, company.Size);
            //Assert.AreEqual(testCompany.Description, company.Description);
            //Assert.AreEqual(testCompany.Country, company.Country);
            //Assert.AreEqual(testCompany.Address, company.Address);
            //Assert.AreEqual(testCompany.LaunchDate, company.LaunchDate);
        }

        [Test]
        public void Insert_InsertsCompanyAndReturnsAViewResult_WithAnCompany()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}
